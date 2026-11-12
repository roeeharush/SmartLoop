using System.Net;
using System.Text;
using System.Text.Json;
using SmartLoop.Core.Models;
using SmartLoop.Infrastructure;

namespace SmartLoop.Tests;

public class AiServiceTests
{
    private static List<Component> CreateLibrary()
    {
        return new List<Component>
        {
            new Component { Id = 1, Name = "Temperature Sensor", Type = ComponentType.Sensor, Unit = "°C", MinValue = -50, MaxValue = 150 },
            new Component { Id = 5, Name = "PID Controller", Type = ComponentType.Controller, Unit = "%", MinValue = 0, MaxValue = 100 },
            new Component { Id = 9, Name = "Heater Actuator", Type = ComponentType.Actuator, Unit = "W", MinValue = 0, MaxValue = 2000 }
        };
    }

    private static string TextResponse(string text)
    {
        return JsonSerializer.Serialize(new
        {
            content = new[] { new { type = "text", text } },
            stop_reason = "end_turn"
        });
    }

    private static AiService CreateService(StubHandler handler)
    {
        return new AiService(new HttpClient(handler), "test-key");
    }

    [Fact]
    public async Task Suggest_ValidResponse_ReturnsIds()
    {
        var handler = new StubHandler(TextResponse("{\"componentIds\": [1, 5, 9]}"));
        var service = CreateService(handler);

        var ids = await service.SuggestComponentIdsAsync("greenhouse", CreateLibrary());

        Assert.Equal(new[] { 1, 5, 9 }, ids);
    }

    [Fact]
    public async Task Suggest_JsonWrappedInTextAndCodeFence_ReturnsIds()
    {
        var text = "Here is my suggestion:\n```json\n{\"componentIds\": [1, 5, 9]}\n```\nHope it helps.";
        var service = CreateService(new StubHandler(TextResponse(text)));

        var ids = await service.SuggestComponentIdsAsync("greenhouse", CreateLibrary());

        Assert.Equal(new[] { 1, 5, 9 }, ids);
    }

    [Fact]
    public async Task Suggest_ThinkingBlockBeforeTextBlock_ReturnsIds()
    {
        var body = JsonSerializer.Serialize(new
        {
            content = new object[]
            {
                new { type = "thinking", thinking = "" },
                new { type = "text", text = "{\"componentIds\": [1, 5, 9]}" }
            },
            stop_reason = "end_turn"
        });
        var service = CreateService(new StubHandler(body));

        var ids = await service.SuggestComponentIdsAsync("greenhouse", CreateLibrary());

        Assert.Equal(new[] { 1, 5, 9 }, ids);
    }

    [Fact]
    public async Task Suggest_NoTextBlock_ThrowsFormatException()
    {
        var body = JsonSerializer.Serialize(new
        {
            content = new object[] { new { type = "thinking", thinking = "" } },
            stop_reason = "max_tokens"
        });
        var service = CreateService(new StubHandler(body));

        await Assert.ThrowsAsync<FormatException>(
            () => service.SuggestComponentIdsAsync("greenhouse", CreateLibrary()));
    }

    [Fact]
    public async Task Suggest_TextWithoutJson_ThrowsFormatException()
    {
        var service = CreateService(new StubHandler(TextResponse("I cannot help with that.")));

        await Assert.ThrowsAsync<FormatException>(
            () => service.SuggestComponentIdsAsync("greenhouse", CreateLibrary()));
    }

    [Fact]
    public async Task Suggest_JsonWithoutComponentIds_ThrowsFormatException()
    {
        var service = CreateService(new StubHandler(TextResponse("{\"ids\": [1, 2]}")));

        await Assert.ThrowsAsync<FormatException>(
            () => service.SuggestComponentIdsAsync("greenhouse", CreateLibrary()));
    }

    [Fact]
    public async Task Suggest_ComponentIdsNotNumbers_ThrowsFormatException()
    {
        var service = CreateService(new StubHandler(TextResponse("{\"componentIds\": [\"a\", \"b\"]}")));

        await Assert.ThrowsAsync<FormatException>(
            () => service.SuggestComponentIdsAsync("greenhouse", CreateLibrary()));
    }

    [Fact]
    public async Task Suggest_UnauthorizedStatus_ThrowsHttpRequestException()
    {
        var service = CreateService(new StubHandler("{}", HttpStatusCode.Unauthorized));

        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            () => service.SuggestComponentIdsAsync("greenhouse", CreateLibrary()));

        Assert.Contains("401", exception.Message);
    }

    [Fact]
    public async Task Suggest_SendsKeyVersionDescriptionAndLibrary()
    {
        var handler = new StubHandler(TextResponse("{\"componentIds\": [1, 5, 9]}"));
        var service = CreateService(handler);

        await service.SuggestComponentIdsAsync("pressure in a gas tank", CreateLibrary());

        Assert.Equal("https://api.anthropic.com/v1/messages", handler.RequestUri);
        Assert.Equal("test-key", handler.ApiKeyHeader);
        Assert.False(string.IsNullOrEmpty(handler.VersionHeader));
        Assert.Contains("pressure in a gas tank", handler.RequestBody);
        Assert.Contains("Temperature Sensor", handler.RequestBody);
    }

    private class StubHandler : HttpMessageHandler
    {
        private readonly string _responseBody;
        private readonly HttpStatusCode _statusCode;

        public string? RequestUri { get; private set; }
        public string? ApiKeyHeader { get; private set; }
        public string? VersionHeader { get; private set; }
        public string RequestBody { get; private set; } = string.Empty;

        public StubHandler(string responseBody, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            _responseBody = responseBody;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestUri = request.RequestUri?.ToString();
            ApiKeyHeader = request.Headers.TryGetValues("x-api-key", out var keys) ? keys.FirstOrDefault() : null;
            VersionHeader = request.Headers.TryGetValues("anthropic-version", out var versions) ? versions.FirstOrDefault() : null;
            RequestBody = request.Content == null ? string.Empty : await request.Content.ReadAsStringAsync(cancellationToken);

            return new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_responseBody, Encoding.UTF8, "application/json")
            };
        }
    }
}
