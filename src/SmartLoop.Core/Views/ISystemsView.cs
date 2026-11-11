using SmartLoop.Core.Models;

namespace SmartLoop.Core.Views;

public interface ISystemsView
{
    event EventHandler? ViewLoaded;
    event EventHandler? ExportRequested;
    event EventHandler? ImportRequested;
    event EventHandler? SearchRequested;

    int? SelectedSystemId { get; }
    string XPathQuery { get; }

    void ShowSystems(IReadOnlyList<ControlSystem> systems);
    void ShowSearchResults(IReadOnlyList<string> lines);
    void ShowError(string message);
    void ShowMessage(string message);

    string? ChooseSaveFilePath(string suggestedFileName);
    string? ChooseOpenFilePath();
}