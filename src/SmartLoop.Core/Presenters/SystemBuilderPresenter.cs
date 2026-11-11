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

    private IReadOnlyList<Component> _components = new List<Component>();

    public SystemBuilderPresenter(
        ISystemBuilderView view,
        IComponentRepository componentRepository,
        ISystemRepository systemRepository,
        IValidationService validationService)
    {   
        _view = view;
        _componentRepository = componentRepository;
        _systemRepository = systemRepository;
        _validationService = validationService;

        _view.ViewLoaded += OnViewLoaded;
        _view.SaveRequested += OnSaveRequested;
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
            _view.ShowError($"Failed to save system: {ex.Message}");
        }
    }

    private ControlSystem BuildSystemFromView()
    {
        var system = new ControlSystem
        {
            Name = _view.SystemName.Trim(),
            Description = string.IsNullOrWhiteSpace(_view.SystemDescription) ? null : _view.SystemDescription.Trim()
        };

        var position = 1;

        foreach (var id in _view.SelectedComponentIds)
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

        return system;
    }
}