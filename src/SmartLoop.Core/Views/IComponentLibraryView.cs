using SmartLoop.Core.Models;

namespace SmartLoop.Core.Views;

public interface IComponentLibraryView
{
    event EventHandler? ViewLoaded;
    event EventHandler? DeleteRequested;
    event EventHandler? AddRequested;
    event EventHandler? EditRequested;
    event EventHandler? UpdateRequested;

    int? SelectedComponentId { get; }

    string ComponentName { get; }
    ComponentType SelectedType { get; }
    string ComponentUnit { get; }
    string MinValueText { get; }
    string MaxValueText { get; }
    string ComponentDescription { get; }

    void ClearInputs();

    void ShowInputs(Component component);

    void ShowComponents(IReadOnlyList<Component> components);

    void ShowError(string message);
}