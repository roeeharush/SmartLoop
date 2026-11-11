using SmartLoop.Core.Models;
using SmartLoop.Core.Validation;

namespace SmartLoop.Tests;

public class ValidationServiceTests
{
    private readonly ValidationService _service = new();

    private static Component CreateValidComponent()
    {
        return new Component
        {
            Name = "Temperature Sensor",
            Type = ComponentType.Sensor,
            Unit = "°C",
            MinValue = -50,
            MaxValue = 200
        };
    }

    [Fact]
    public void ValidateComponent_ValidComponent_ReturnsNoErrors()
    {
        var component = CreateValidComponent();

        var errors = _service.ValidateComponent(component);

        Assert.Empty(errors);
    }

    [Fact]
    public void ValidateComponent_EmptyName_ReturnsNameError()
    {
        var component = CreateValidComponent();
        component.Name = "";

        var errors = _service.ValidateComponent(component);

        Assert.Single(errors);
        Assert.Contains("Name is required.", errors);
    }

    [Fact]
    public void ValidateComponent_MinGreaterThanMax_ReturnsRangeError()
    {
        var component = CreateValidComponent();
        component.MinValue = 100;
        component.MaxValue = 10;

        var errors = _service.ValidateComponent(component);

        Assert.Single(errors);
        Assert.Contains("Min must be smaller than Max.", errors);
    }
}
