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
        if (string.IsNullOrWhiteSpace(_view.ComponentName))
        {
            _view.ShowError("Name is required.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_view.ComponentUnit))
        {
            _view.ShowError("Unit is required.");
            return;
        }

        if (!decimal.TryParse(_view.MinValueText, out var minValue) ||!decimal.TryParse(_view.MaxValueText, out var maxValue))
        {
            _view.ShowError("Min and Max must be valid numbers.");
            return;
        }

        if (minValue >= maxValue)
        {
            _view.ShowError("Min must be smaller than Max.");
            return;
        }
        
        var component = new Component
        {
            Name = _view.ComponentName.Trim(),
            Type = _view.SelectedType,
            Unit = _view.ComponentUnit.Trim(),
            MinValue = minValue,
            MaxValue = maxValue,
            Description = string.IsNullOrWhiteSpace(_view.ComponentDescription) ? null : _view.ComponentDescription.Trim()
        };

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
}