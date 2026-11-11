using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;
using System.Xml;
using System.Xml.Linq;

namespace SmartLoop.Infrastructure;

public class XmlService : IXmlService
{
    public string Export(ControlSystem system, IReadOnlyList<Component> libraryComponents)
    {
        var componentElements = system.Components.Select(item =>
        {
            var element = new XElement("Component",
                new XAttribute("id", item.ComponentId),
                new XAttribute("role", item.Role),
                new XAttribute("position", item.Position));

            var component = libraryComponents.FirstOrDefault(c => c.Id == item.ComponentId);
            if (component != null)
            {
                element.Add(
                    new XAttribute("name", component.Name),
                    new XAttribute("unit", component.Unit),
                    new XAttribute("min", component.MinValue),
                    new XAttribute("max", component.MaxValue));
            }

            return element;
        });

        var document = new XDocument(
            new XElement("ControlSystem",
                new XAttribute("name", system.Name),
                new XElement("Description", system.Description),
                new XElement("Components", componentElements)));

        return document.ToString();
    }

    public ControlSystem Import(string xml)
    {
        XDocument document;

        try
        {
            document = XDocument.Parse(xml);
        }
        catch (XmlException ex)
        {
            throw new FormatException("The text is not valid XML.", ex);
        }

        var root = document.Root;
        if (root == null || root.Name != "ControlSystem")
        {
            throw new FormatException("The root element must be <ControlSystem>.");
        }

        var description = root.Element("Description")?.Value;

        var system = new ControlSystem
        {
            Name = (string?)root.Attribute("name") ?? string.Empty,
            Description = string.IsNullOrWhiteSpace(description) ? null : description
        };

        var componentsElement = root.Element("Components");
        if (componentsElement != null)
        {
            foreach (var element in componentsElement.Elements("Component"))
            {
                system.Components.Add(new SystemComponent
                {
                    ComponentId = ReadInt(element, "id"),
                    Role = ReadRole(element),
                    Position = ReadInt(element, "position")
                });
            }
        }

        return system;
    }

    private static int ReadInt(XElement element, string attributeName)
    {
        var text = (string?)element.Attribute(attributeName);

        if (!int.TryParse(text, out var value))
        {
            throw new FormatException($"Attribute '{attributeName}' must be a whole number.");
        }

        return value;
    }

    private static ComponentType ReadRole(XElement element)
    {
        var text = (string?)element.Attribute("role");

        if (!Enum.TryParse<ComponentType>(text, out var role) || !Enum.IsDefined(role))
        {
            throw new FormatException("Attribute 'role' must be Sensor, Controller or Actuator.");
        }

        return role;
    }
}