using SmartLoop.Core.Models;

namespace SmartLoop.Core.Interfaces;

public interface IValidationService
{
    IReadOnlyList<string> ValidateComponent(Component component);
    IReadOnlyList<string> ValidateSystem(ControlSystem system);
}
