using SmartLoop.Core.Presenters;
using SmartLoop.Infrastructure;


namespace SmartLoop.WinForms;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var connectionString = @"Server=localhost\SQLEXPRESS;Database=SmartLoop;Trusted_Connection=True;TrustServerCertificate=True;";

        var form = new MainForm();
        var repository = new SqlComponentRepository(connectionString);
        var presenter = new ComponentLibraryPresenter(form, repository);

        Application.Run(form);
    }    
}