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

    private static ControlSystem CreateValidSystem()
    {
        return new ControlSystem
        {
            Name = "Greenhouse",
            Components = new List<SystemComponent>
            {
                new SystemComponent { ComponentId = 1, Role = ComponentType.Sensor, Position = 1 },
                new SystemComponent { ComponentId = 5, Role = ComponentType.Controller, Position = 2 },
                new SystemComponent { ComponentId = 9, Role = ComponentType.Actuator, Position = 3 }
            }
        };
    }

    [Fact]
    public void ValidateSystem_ValidSystem_ReturnsNoErrors()
    {
        var system = CreateValidSystem();

        var errors = _service.ValidateSystem(system);

        Assert.Empty(errors);
    }

    [Fact]
    public void ValidateSystem_EmptyName_ReturnsNameError()
    {
        var system = CreateValidSystem();
        system.Name = "  ";

        var errors = _service.ValidateSystem(system);

        Assert.Single(errors);
        Assert.Contains("System name is required.", errors);
    }

    [Fact]
    public void ValidateSystem_NoSensor_ReturnsSensorError()
    {
        var system = CreateValidSystem();
        system.Components.RemoveAll(c => c.Role == ComponentType.Sensor);

        var errors = _service.ValidateSystem(system);

        Assert.Single(errors);
        Assert.Contains("The system must contain at least one sensor.", errors);
    }

    [Fact]
    public void ValidateSystem_NoController_ReturnsControllerError()
    {
        var system = CreateValidSystem();
        system.Components.RemoveAll(c => c.Role == ComponentType.Controller);

        var errors = _service.ValidateSystem(system);

        Assert.Single(errors);
        Assert.Contains("The system must contain at least one controller.", errors);
    }

    [Fact]
    public void ValidateSystem_NoActuator_ReturnsActuatorError()
    {
        var system = CreateValidSystem();
        system.Components.RemoveAll(c => c.Role == ComponentType.Actuator);

        var errors = _service.ValidateSystem(system);

        Assert.Single(errors);
        Assert.Contains("The system must contain at least one actuator.", errors);
    }

    [Fact]
    public void ValidateSystem_SameComponentTwiceInSameRole_ReturnsDuplicateError()
    {
        var system = CreateValidSystem();
        system.Components.Add(new SystemComponent { ComponentId = 1, Role = ComponentType.Sensor, Position = 4 });

        var errors = _service.ValidateSystem(system);

        Assert.Single(errors);
        Assert.Contains("The same component cannot appear twice in the same role.", errors);
    }

    [Fact]
    public void ValidateSystem_EmptySystem_ReturnsAllErrors()
    {
        var system = new ControlSystem();

        var errors = _service.ValidateSystem(system);

        Assert.Equal(4, errors.Count);
    }
}
