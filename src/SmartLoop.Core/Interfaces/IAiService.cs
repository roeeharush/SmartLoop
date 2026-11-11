using SmartLoop.Core.Models;

namespace SmartLoop.Core.Interfaces;

public interface IAiService
{
    Task<IReadOnlyList<int>> SuggestComponentIdsAsync(
        string description,
        IReadOnlyList<Component> libraryComponents);
}
