using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;

namespace SmartLoop.Core.Validation;

public class ValidationService : IValidationService
{
    public IReadOnlyList<string> ValidateComponent(Component component)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(component.Name))
        {
            errors.Add("Name is required.");
        }

        if (string.IsNullOrWhiteSpace(component.Unit))
        {
            errors.Add("Unit is required.");
        }

        if (component.MinValue >= component.MaxValue)
        {
            errors.Add("Min must be smaller than Max.");
        }

        return errors;
    }

    public IReadOnlyList<string> ValidateSystem(ControlSystem system)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(system.Name))
        {
            errors.Add("System name is required.");
        }

        if (!system.Components.Any(c => c.Role == ComponentType.Sensor))
        {
            errors.Add("The system must contain at least one sensor.");
        }

        if (!system.Components.Any(c => c.Role == ComponentType.Controller))
        {
            errors.Add("The system must contain at least one controller.");
        }

        if (!system.Components.Any(c => c.Role == ComponentType.Actuator))
        {
            errors.Add("The system must contain at least one actuator.");
        }

        var hasDuplicates = system.Components
            .GroupBy(c => new { c.ComponentId, c.Role })
            .Any(group => group.Count() > 1);

        if (hasDuplicates)
        {
            errors.Add("The same component cannot appear twice in the same role.");
        }

        return errors;
    }
}
