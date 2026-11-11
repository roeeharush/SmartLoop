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
}
