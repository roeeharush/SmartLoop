using SmartLoop.Core.Exceptions;
using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Views;

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
}