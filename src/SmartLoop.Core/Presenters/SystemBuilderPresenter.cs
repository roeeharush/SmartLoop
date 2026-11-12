using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;
using SmartLoop.Core.Views;

namespace SmartLoop.Core.Presenters;

public class SystemBuilderPresenter
{
    private readonly ISystemBuilderView _view;
    private readonly IComponentRepository _componentRepository;
    private readonly ISystemRepository _systemRepository;
    private readonly IValidationService _validationService;
    private readonly IAiService _aiService;

    private IReadOnlyList<Component> _components = new List<Component>();

    public SystemBuilderPresenter(
        ISystemBuilderView view,
        IComponentRepository componentRepository,
        ISystemRepository systemRepository,
        IValidationService validationService,
        IAiService aiService)
    {
        _view = view;
        _componentRepository = componentRepository;
        _systemRepository = systemRepository;
        _validationService = validationService;
        _aiService = aiService;

        _view.ViewLoaded += OnViewLoaded;
        _view.SaveRequested += OnSaveRequested;
        _view.AiSuggestRequested += OnAiSuggestRequested;
    }

    private async void OnAiSuggestRequested(object? sender, EventArgs e)
    {
        var description = _view.SystemDescription.Trim();
        if (description.Length == 0)
        {
            _view.ShowError("Please describe the system in the Description box first.");
            return;
        }

        try
        {
            var suggestedIds = await _aiService.SuggestComponentIdsAsync(description, _components);

            var knownIds = suggestedIds
                .Where(id => _components.Any(c => c.Id == id))
                .Distinct()
                .ToList();

            var draft = new ControlSystem { Name = "AI suggestion" };
            AddComponents(draft, knownIds);

            var errors = _validationService.ValidateSystem(draft);
            if (errors.Count > 0)
            {
                _view.ShowError("The AI suggestion is not valid:" + Environment.NewLine + string.Join(Environment.NewLine, errors));
                return;
            }

            _view.SelectComponents(knownIds);

            var ignored = suggestedIds.Distinct().Count() - knownIds.Count;
            var note = ignored > 0 ? $" ({ignored} unknown component(s) were ignored.)" : string.Empty;
            _view.ShowMessage("Suggestion applied." + note + " Review it and press Save to confirm.");
        }
        catch (Exception ex)
        {
            _view.ShowError($"AI suggestion failed: {ex.Message}");
        }
    }

    private async void OnViewLoaded(object? sender, EventArgs e)
    {
        try
        {
            _components = await _componentRepository.GetAllAsync();
            _view.ShowComponents(_components);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to load components: {ex.Message}");
        }
    }

    private async void OnSaveRequested(object? sender, EventArgs e)
    {
        var system = BuildSystemFromView();

        var errors = _validationService.ValidateSystem(system);
        if (errors.Count > 0)
        {
            _view.ShowError(string.Join(Environment.NewLine, errors));
            return;
        }

        try
        {
            await _systemRepository.AddAsync(system);
            _view.ShowMessage("System saved successfully.");
            _view.CloseView();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            _view.ShowError("Failed to save the system. Check the database connection and try again.");
        }
    }

    private ControlSystem BuildSystemFromView()
    {
        var system = new ControlSystem
        {
            Name = _view.SystemName.Trim(),
            Description = string.IsNullOrWhiteSpace(_view.SystemDescription) ? null : _view.SystemDescription.Trim()
        };

        AddComponents(system, _view.SelectedComponentIds);

        return system;
    }

    private void AddComponents(ControlSystem system, IEnumerable<int> componentIds)
    {
        var position = 1;

        foreach (var id in componentIds)
        {
            var component = _components.FirstOrDefault(c => c.Id == id);
            if (component == null)
            {
                continue;
            }

            system.Components.Add(new SystemComponent
            {
                ComponentId = component.Id,
                Role = component.Type,
                Position = position
            });

            position++;
        }
    }
}