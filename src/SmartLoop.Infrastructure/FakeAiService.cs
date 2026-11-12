using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;

namespace SmartLoop.Infrastructure;

public class FakeAiService : IAiService
{
    public Task<IReadOnlyList<int>> SuggestComponentIdsAsync(string description, IReadOnlyList<Component> libraryComponents)
    {
        var suggestion = new List<int>();

        var types = new[] { ComponentType.Sensor, ComponentType.Controller, ComponentType.Actuator };

        foreach (var type in types)
        {
            var component = libraryComponents
                .Where(c => c.Type == type)
                .OrderBy(c => c.Id)
                .FirstOrDefault();

            if (component != null)
            {
                suggestion.Add(component.Id);
            }
        }

        return Task.FromResult<IReadOnlyList<int>>(suggestion);
    }
}