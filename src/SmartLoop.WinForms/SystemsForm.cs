using SmartLoop.Core.Models;
using SmartLoop.Core.Views;

namespace SmartLoop.WinForms;

public partial class SystemsForm : Form, ISystemsView
{
    public event EventHandler? ViewLoaded;
    public event EventHandler? ExportRequested;
    public event EventHandler? ImportRequested;

    public SystemsForm()
    {
        InitializeComponent();

        Load += (sender, e) => ViewLoaded?.Invoke(this, EventArgs.Empty);
        exportButton.Click += (sender, e) => ExportRequested?.Invoke(this, EventArgs.Empty);
        importButton.Click += (sender, e) => ImportRequested?.Invoke(this, EventArgs.Empty);
    }

    public int? SelectedSystemId
    {
        get
        {
            if (systemsGrid.CurrentRow?.DataBoundItem is ControlSystem system)
            {
                return system.Id;
            }

            return null;
        }
    }

    public void ShowSystems(IReadOnlyList<ControlSystem> systems)
    {
        systemsGrid.DataSource = systems.ToList();

        var componentsColumn = systemsGrid.Columns["Components"];
        if (componentsColumn != null)
        {
            componentsColumn.Visible = false;
        }
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message, "SmartLoop", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public string? ChooseSaveFilePath(string suggestedFileName)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "XML files (*.xml)|*.xml",
            DefaultExt = "xml",
            FileName = suggestedFileName
        };

        return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : null;
    }

    public string? ChooseOpenFilePath()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "XML files (*.xml)|*.xml"
        };

        return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : null;
    }
}
