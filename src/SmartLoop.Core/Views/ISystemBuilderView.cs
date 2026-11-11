using SmartLoop.Core.Models;

namespace SmartLoop.Core.Views;

public interface ISystemBuilderView
{
    event EventHandler? ViewLoaded;
    event EventHandler? SaveRequested;

    string SystemName { get; }
    string SystemDescription { get; }
    IReadOnlyList<int> SelectedComponentIds { get; }

    void ShowComponents(IReadOnlyList<Component> components);
    void ShowError(string message);
    void ShowMessage(string message);
    void CloseView();
}