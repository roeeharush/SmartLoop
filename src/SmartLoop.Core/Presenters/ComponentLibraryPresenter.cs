using SmartLoop.Core.Exceptions;
using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Views;
using SmartLoop.Core.Models;

namespace SmartLoop.Core.Presenters;

public class ComponentLibraryPresenter
{
    private readonly IComponentLibraryView _view;
    private readonly IComponentRepository _repository;

    public ComponentLibraryPresenter(IComponentLibraryView view, IComponentRepository repository)
    {
        _view = view;
        _repository = repository;

        _view.ViewLoaded += OnViewLoaded;
        _view.DeleteRequested += OnDeleteRequested;
        _view.AddRequested += OnAddRequested;
        _view.EditRequested += OnEditRequested;
        _view.UpdateRequested += OnUpdateRequested;
    }

    private async void OnViewLoaded(object? sender, EventArgs e)
    {
        try
        {
            var components = await _repository.GetAllAsync();
            _view.ShowComponents(components);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to load components: {ex.Message}");
        }
    }

    private async void OnDeleteRequested(object? sender, EventArgs e)
    {
        var id = _view.SelectedComponentId;

        if (id == null)
        {
            _view.ShowError("Please select a component to delete.");
            return;
        }

        try
        {
            await _repository.DeleteAsync(id.Value);

            var components = await _repository.GetAllAsync();
            _view.ShowComponents(components);
        }
        catch (ComponentInUseException ex)
        {
            _view.ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to delete component: {ex.Message}");
        }
    }

    private async void OnAddRequested(object? sender, EventArgs e)
    {
        var component = ReadComponentFromInputs();
        if (component == null)
        {
            return;
        }

        try
        {
            await _repository.AddAsync(component);
            _view.ClearInputs();

            var components = await _repository.GetAllAsync();
            _view.ShowComponents(components);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to add component: {ex.Message}");
        }
    }

    private async void OnEditRequested(object? sender, EventArgs e)
    {
        var id = _view.SelectedComponentId;

        if (id == null)
        {
            _view.ShowError("Please select a component to edit.");
            return;
        }

        try
        {
            var component = await _repository.GetByIdAsync(id.Value);

            if (component == null)
            {
                _view.ShowError("The selected component no longer exists.");
                return;
            }

            _view.ShowInputs(component);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to load component: {ex.Message}");
        }
    }

    private async void OnUpdateRequested(object? sender, EventArgs e)
    {
        var id = _view.SelectedComponentId;

        if (id == null)
        {
            _view.ShowError("Please select a component to update.");
            return;
        }

        var component = ReadComponentFromInputs();
        if (component == null)
        {
            return;
        }

        component.Id = id.Value;

        try
        {
            await _repository.UpdateAsync(component);
            _view.ClearInputs();

            var components = await _repository.GetAllAsync();
            _view.ShowComponents(components);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Failed to update component: {ex.Message}");
        }
    }

    private Component? ReadComponentFromInputs()
    {
        if (string.IsNullOrWhiteSpace(_view.ComponentName))
        {
            _view.ShowError("Name is required.");
            return null;
        }

        if (string.IsNullOrWhiteSpace(_view.ComponentUnit))
        {
            _view.ShowError("Unit is required.");
            return null;
        }

        if (!decimal.TryParse(_view.MinValueText, out var minValue) ||
            !decimal.TryParse(_view.MaxValueText, out var maxValue))
        {
            _view.ShowError("Min and Max must be valid numbers.");
            return null;
        }

        if (minValue >= maxValue)
        {
            _view.ShowError("Min must be smaller than Max.");
            return null;
        }

        return new Component
        {
            Name = _view.ComponentName.Trim(),
            Type = _view.SelectedType,
            Unit = _view.ComponentUnit.Trim(),
            MinValue = minValue,
            MaxValue = maxValue,
            Description = string.IsNullOrWhiteSpace(_view.ComponentDescription) ? null : _view.ComponentDescription.Trim()
        };
    }
}
