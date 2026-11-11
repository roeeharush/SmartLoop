using SmartLoop.Core.Models;
using SmartLoop.Infrastructure;

namespace SmartLoop.Tests;

public class XmlServiceTests
{
    private readonly XmlService _service = new();

    private static List<Component> CreateLibrary()
    {
        return new List<Component>
        {
            new Component { Id = 1, Name = "Temperature Sensor", Type = ComponentType.Sensor, Unit = "°C", MinValue = -50, MaxValue = 150 },
            new Component { Id = 5, Name = "PID Controller", Type = ComponentType.Controller, Unit = "%", MinValue = 0, MaxValue = 100 },
            new Component { Id = 9, Name = "Heater Actuator", Type = ComponentType.Actuator, Unit = "W", MinValue = 0, MaxValue = 2000 }
        };
    }

    private static ControlSystem CreateSystem()
    {
        var system = new ControlSystem
        {
            Name = "Greenhouse Temperature Control",
            Description = "Keeps greenhouse temperature stable"
        };

        system.Components.Add(new SystemComponent { ComponentId = 1, Role = ComponentType.Sensor, Position = 1 });
        system.Components.Add(new SystemComponent { ComponentId = 5, Role = ComponentType.Controller, Position = 2 });
        system.Components.Add(new SystemComponent { ComponentId = 9, Role = ComponentType.Actuator, Position = 3 });

        return system;
    }

    [Fact]
    public void ExportThenImport_ReturnsSameSystem()
    {
        var original = CreateSystem();

        var xml = _service.Export(original, CreateLibrary());
        var imported = _service.Import(xml);

        Assert.Equal(original.Name, imported.Name);
        Assert.Equal(original.Description, imported.Description);
        Assert.Equal(original.Components.Count, imported.Components.Count);

        for (var i = 0; i < original.Components.Count; i++)
        {
            Assert.Equal(original.Components[i].ComponentId, imported.Components[i].ComponentId);
            Assert.Equal(original.Components[i].Role, imported.Components[i].Role);
            Assert.Equal(original.Components[i].Position, imported.Components[i].Position);
        }
    }

    [Fact]
    public void ExportThenImport_NullDescription_StaysNull()
    {
        var original = CreateSystem();
        original.Description = null;

        var imported = _service.Import(_service.Export(original, CreateLibrary()));

        Assert.Null(imported.Description);
    }

    [Fact]
    public void Export_IncludesComponentDetailsFromLibrary()
    {
        var xml = _service.Export(CreateSystem(), CreateLibrary());

        Assert.Contains("name=\"Temperature Sensor\"", xml);
        Assert.Contains("min=\"-50\"", xml);
        Assert.Contains("max=\"150\"", xml);
    }

    [Fact]
    public void Export_ComponentMissingFromLibrary_StillExportsIdRolePosition()
    {
        var xml = _service.Export(CreateSystem(), new List<Component>());
        var imported = _service.Import(xml);

        Assert.Equal(3, imported.Components.Count);
        Assert.DoesNotContain("name=\"Temperature Sensor\"", xml);
    }

    [Fact]
    public void Import_NotXml_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => _service.Import("this is not xml"));
    }

    [Fact]
    public void Import_WrongRootElement_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => _service.Import("<Something />"));
    }

    [Fact]
    public void Import_InvalidRole_ThrowsFormatException()
    {
        var xml = "<ControlSystem name=\"X\"><Components><Component id=\"1\" role=\"Banana\" position=\"1\" /></Components></ControlSystem>";

        Assert.Throws<FormatException>(() => _service.Import(xml));
    }

    [Fact]
    public void Import_NonNumericId_ThrowsFormatException()
    {
        var xml = "<ControlSystem name=\"X\"><Components><Component id=\"abc\" role=\"Sensor\" position=\"1\" /></Components></ControlSystem>";

        Assert.Throws<FormatException>(() => _service.Import(xml));
    }
}
