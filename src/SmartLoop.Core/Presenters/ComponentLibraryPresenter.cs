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
}