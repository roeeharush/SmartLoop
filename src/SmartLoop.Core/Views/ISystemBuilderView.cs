using SmartLoop.Core.Models;

namespace SmartLoop.Core.Views;

public interface ISystemBuilderView
{
    event EventHandler? ViewLoaded;
    event EventHandler? SaveRequested;
    event EventHandler? AiSuggestRequested;

    string SystemName { get; }
    string SystemDescription { get; }
    IReadOnlyList<int> SelectedComponentIds { get; }

    void ShowComponents(IReadOnlyList<Component> components);
    void SelectComponents(IReadOnlyList<int> componentIds);
    void ShowError(string message);
    void ShowMessage(string message);
    void CloseView();
}