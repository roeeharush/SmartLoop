using SmartLoop.Core.Models;
using SmartLoop.Core.Views;

namespace SmartLoop.WinForms;

public partial class MainForm : Form, IComponentLibraryView
{
    public event EventHandler? ViewLoaded;

    public MainForm()
    {
        InitializeComponent();
        Load += (sender, e) => ViewLoaded?.Invoke(this, EventArgs.Empty);
    }

    public void ShowComponents(IReadOnlyList<Component> components)
    {
        componentsGrid.DataSource = components.ToList();

        var parametersColumn = componentsGrid.Columns["Parameters"];
        if (parametersColumn != null)
        {
            parametersColumn.Visible = false;
        }
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}