using Microsoft.Extensions.DependencyInjection;
using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Presenters;
using SmartLoop.Core.Validation;
using SmartLoop.Core.Views;
using SmartLoop.Infrastructure;

namespace SmartLoop.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var connectionString = @"Server=localhost\SQLEXPRESS;Database=SmartLoop;Trusted_Connection=True;TrustServerCertificate=True;";

        var services = new ServiceCollection();

        services.AddSingleton<IComponentRepository>(_ => new SqlComponentRepository(connectionString));
        services.AddSingleton<ISystemRepository>(_ => new SqlSystemRepository(connectionString));
        services.AddSingleton<IValidationService, ValidationService>();
        services.AddSingleton<MainForm>();
        services.AddSingleton<IComponentLibraryView>(provider => provider.GetRequiredService<MainForm>());
        services.AddSingleton<ComponentLibraryPresenter>();

        using var serviceProvider = services.BuildServiceProvider();

        serviceProvider.GetRequiredService<ComponentLibraryPresenter>();
        var form = serviceProvider.GetRequiredService<MainForm>();

        form.BuildSystemRequested += (sender, e) =>
        {
            using var builderForm = new SystemBuilderForm();
            ActivatorUtilities.CreateInstance<SystemBuilderPresenter>(serviceProvider, builderForm);
            builderForm.ShowDialog(form);
        };

        Application.Run(form);
    }
}