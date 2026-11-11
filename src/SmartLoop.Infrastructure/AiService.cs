using System.Text;
using System.Text.Json;
using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;

namespace SmartLoop.Infrastructure;

public class AiService : IAiService
{
    private const string ApiUrl = "https://api.anthropic.com/v1/messages";
    private const string Model = "claude-haiku-5-5";

    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AiService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<IReadOnlyList<int>> SuggestComponentIdsAsync(
        string description,
        IReadOnlyList<Component> libraryComponents)
    {
        var requestBody = new
        {
            model = Model,
            max_tokens = 1024,
            thinking = new { type = "disabled" },
            messages = new[]
            {
                new { role = "user", content = BuildPrompt(description, libraryComponents) }
            }
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
        request.Headers.Add("x-api-key", _apiKey);
        request.Headers.Add("anthropic-version", "2023-06-01");
        request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"The AI request failed with status {(int)response.StatusCode}.");
        }

        var responseText = await response.Content.ReadAsStringAsync();

        return ParseIds(ExtractText(responseText));
    }

    private static string BuildPrompt(string description, IReadOnlyList<Component> libraryComponents)
    {
        var libraryJson = JsonSerializer.Serialize(libraryComponents.Select(c => new
        {
            id = c.Id,
            name = c.Name,
            type = c.Type.ToString(),
            unit = c.Unit,
            min = c.MinValue,
            max = c.MaxValue
        }));

        return $$"""
            You help compose a control system from a component library.
            Pick components ONLY from this library (JSON): {{libraryJson}}
            The user describes the system: "{{description}}"
            Include at least one Sensor, one Controller and one Actuator.
            Reply with ONLY a JSON object in exactly this form, with no other text: {"componentIds": [1, 2, 3]}
            """;
    }

    private static string ExtractText(string responseText)
    {
        try
        {
            using var document = JsonDocument.Parse(responseText);
            var root = document.RootElement;

            foreach (var block in root.GetProperty("content").EnumerateArray())
            {
                if (block.TryGetProperty("type", out var type)
                    && type.GetString() == "text"
                    && block.TryGetProperty("text", out var text))
                {
                    return text.GetString() ?? string.Empty;
                }
            }

            var stopReason = root.TryGetProperty("stop_reason", out var stop) ? stop.GetString() : "unknown";
            throw new FormatException($"The AI response contained no text block (stop_reason: {stopReason}).");
        }
        catch (Exception ex) when (ex is JsonException or KeyNotFoundException or InvalidOperationException)
        {
            throw new FormatException("The AI response had an unexpected format.", ex);
        }
    }

    private static IReadOnlyList<int> ParseIds(string text)
    {
        var start = text.IndexOf('{');
        var end = text.LastIndexOf('}');

        if (start < 0 || end <= start)
        {
            throw new FormatException("The AI response did not contain a JSON object.");
        }

        var json = text.Substring(start, end - start + 1);

        try
        {
            using var document = JsonDocument.Parse(json);

            return document.RootElement
                .GetProperty("componentIds")
                .EnumerateArray()
                .Select(item => item.GetInt32())
                .ToList();
        }
        catch (Exception ex) when (ex is JsonException or KeyNotFoundException or InvalidOperationException or FormatException)
        {
            throw new FormatException("The AI response was not valid JSON with componentIds.", ex);
        }
    }
}