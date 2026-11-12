using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;
using SmartLoop.Core.Views;

namespace SmartLoop.Core.Presenters;

public class SystemsPresenter
{
    private readonly ISystemsView _view;
    private readonly ISystemRepository _systemRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly IXmlService _xmlService;
    private readonly IValidationService _validationService;

    private IReadOnlyList<ControlSystem> _systems = new List<ControlSystem>();

    public SystemsPresenter(
        ISystemsView view,
        ISystemRepository systemRepository,
        IComponentRepository componentRepository,
        IXmlService xmlService,
        IValidationService validationService)
    {
        _view = view;
        _systemRepository = systemRepository;
        _componentRepository = componentRepository;
        _xmlService = xmlService;
        _validationService = validationService;

        _view.ViewLoaded += OnViewLoaded;
        _view.ExportRequested += OnExportRequested;
        _view.ImportRequested += OnImportRequested;
        _view.SearchRequested += OnSearchRequested;
    }

    private async void OnSearchRequested(object? sender, EventArgs e)
    {
        var xpath = _view.XPathQuery.Trim();
        if (xpath.Length == 0)
        {
            _view.ShowError("Please enter an XPath expression.");
            return;
        }

        try
        {
            var library = await _componentRepository.GetAllAsync();
            var lines = new List<string>();

            foreach (var system in _systems)
            {
                var xml = _xmlService.Export(system, library);

                foreach (var result in _xmlService.Search(xml, xpath))
                {
                    lines.Add($"[{system.Name}] {result}");
                }
            }

            if (lines.Count == 0)
            {
                lines.Add("No matches.");
            }

            _view.ShowSearchResults(lines);
        }
        catch (FormatException ex)
        {
            _view.ShowError($"Invalid XPath: {ex.Message}");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Search failed: {ex.Message}");
        }
    }

    private async void OnViewLoaded(object? sender, EventArgs e)
    {
        await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        try
        {
            _systems = await _systemRepository.GetAllAsync();
            _view.ShowSystems(_systems);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to load systems: {ex.Message}");
        }
    }

    private async void OnExportRequested(object? sender, EventArgs e)
    {
        var id = _view.SelectedSystemId;
        if (id == null)
        {
            _view.ShowError("Please select a system to export.");
            return;
        }

        var system = _systems.FirstOrDefault(s => s.Id == id.Value);
        if (system == null)
        {
            _view.ShowError("The selected system was not found.");
            return;
        }

        var safeName = string.Concat(system.Name.Split(Path.GetInvalidFileNameChars()));
        var path = _view.ChooseSaveFilePath($"{safeName}.xml");
        if (path == null)
        {
            return;
        }

        try
        {
            var library = await _componentRepository.GetAllAsync();
            var xml = _xmlService.Export(system, library);
            await File.WriteAllTextAsync(path, xml);
            _view.ShowMessage("System exported successfully.");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to export: {ex.Message}");
        }
    }

    private async void OnImportRequested(object? sender, EventArgs e)
    {
        var path = _view.ChooseOpenFilePath();
        if (path == null)
        {
            return;
        }

        try
        {
            var xml = await File.ReadAllTextAsync(path);
            var system = _xmlService.Import(xml);

            var errors = _validationService.ValidateSystem(system).ToList();
            var library = await _componentRepository.GetAllAsync();

            foreach (var item in system.Components)
            {
                var component = library.FirstOrDefault(c => c.Id == item.ComponentId);

                if (component == null)
                {
                    errors.Add($"Component with id {item.ComponentId} does not exist in the library.");
                }
                else if (component.Type != item.Role)
                {
                    errors.Add($"Component '{component.Name}' is a {component.Type}, but the file lists it as {item.Role}.");
                }
            }

            if (errors.Count > 0)
            {
                _view.ShowError(string.Join(Environment.NewLine, errors));
                return;
            }

            await _systemRepository.AddAsync(system);
            await RefreshAsync();
            _view.ShowMessage("System imported successfully.");
        }
        catch (FormatException ex)
        {
            _view.ShowError($"Invalid file: {ex.Message}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            _view.ShowError("Failed to import the system. Check the file and the database connection, then try again.");
        }
    }
}