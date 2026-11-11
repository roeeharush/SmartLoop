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
        services.AddSingleton<IXmlService, XmlService>();
        var apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            services.AddSingleton<IAiService, FakeAiService>();
        }
        else
        {
            services.AddSingleton<IAiService>(_ => new AiService(new HttpClient(), apiKey));
        }
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

        form.SystemsRequested += (sender, e) =>
        {
            using var systemsForm = new SystemsForm();
            ActivatorUtilities.CreateInstance<SystemsPresenter>(serviceProvider, systemsForm);
            systemsForm.ShowDialog(form);
        };

        Application.Run(form);
    }
}