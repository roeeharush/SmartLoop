using SmartLoop.Core.Models;

namespace SmartLoop.Core.Interfaces;

public interface IXmlService
{
    string Export(ControlSystem system, IReadOnlyList<Component> libraryComponents);
    ControlSystem Import(string xml);
}