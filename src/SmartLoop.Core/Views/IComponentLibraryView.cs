using SmartLoop.Core.Models;

namespace SmartLoop.Core.Views;

public interface IComponentLibraryView
{
    event EventHandler? ViewLoaded;
    event EventHandler? DeleteRequested;

    int? SelectedComponentId { get; }

    void ShowComponents(IReadOnlyList<Component> components);

    void ShowError(string message);
}