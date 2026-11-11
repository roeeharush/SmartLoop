using SmartLoop.Core.Models;

namespace SmartLoop.Core.Views;

public interface ISystemsView
{
    event EventHandler? ViewLoaded;
    event EventHandler? ExportRequested;
    event EventHandler? ImportRequested;

    int? SelectedSystemId { get; }

    void ShowSystems(IReadOnlyList<ControlSystem> systems);
    void ShowError(string message);
    void ShowMessage(string message);

    string? ChooseSaveFilePath(string suggestedFileName);
    string? ChooseOpenFilePath();
}