using SmartLoop.Core.Models;

namespace SmartLoop.Core.Views;

public interface IComponentLibraryView
{
    event EventHandler ViewLoaded;

    void ShowComponents(IReadOnlyList<Component> components);

    void ShowError(string message);
}