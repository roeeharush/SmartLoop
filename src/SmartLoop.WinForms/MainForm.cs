using SmartLoop.Core.Models;
using SmartLoop.Core.Views;

namespace SmartLoop.WinForms;

public partial class MainForm : Form, IComponentLibraryView
{
    public event EventHandler? ViewLoaded;
    public event EventHandler? DeleteRequested;
    public event EventHandler? AddRequested;
    public event EventHandler? EditRequested;
    public event EventHandler? UpdateRequested;

    public int? SelectedComponentId
    {
        get
        {
            if (componentsGrid.CurrentRow?.DataBoundItem is Component component)
            {
                return component.Id;
            }

            return null;
        }
    }

    public string ComponentName => nameTextBox.Text;

    public ComponentType SelectedType
    {
        get
        {
            if (typeComboBox.SelectedItem is ComponentType type)
            {
                return type;
            }

            return ComponentType.Sensor;
        }
    }

    public string ComponentUnit => unitTextBox.Text;

    public string MinValueText => minValueTextBox.Text;

    public string MaxValueText => maxValueTextBox.Text;

    public string ComponentDescription => descriptionTextBox.Text;

    public MainForm()
    {
        InitializeComponent();
           
        typeComboBox.DataSource = Enum.GetValues<ComponentType>();

        Load += (sender, e) => ViewLoaded?.Invoke(this, EventArgs.Empty);
        deleteButton.Click += (sender, e) => DeleteRequested?.Invoke(this, EventArgs.Empty);
        addButton.Click += (sender, e) => AddRequested?.Invoke(this, EventArgs.Empty);
        editButton.Click += (sender, e) => EditRequested?.Invoke(this, EventArgs.Empty);
        updateButton.Click += (sender, e) => UpdateRequested?.Invoke(this, EventArgs.Empty);
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

    public void ClearInputs()
    {
        nameTextBox.Clear();
        typeComboBox.SelectedIndex = 0;
        unitTextBox.Clear();
        minValueTextBox.Clear();
        maxValueTextBox.Clear();
        descriptionTextBox.Clear();
    }

    public void ShowInputs(Component component)
    {
        nameTextBox.Text = component.Name;
        typeComboBox.SelectedItem = component.Type;
        unitTextBox.Text = component.Unit;
        minValueTextBox.Text = component.MinValue.ToString();
        maxValueTextBox.Text = component.MaxValue.ToString();
        descriptionTextBox.Text = component.Description ?? string.Empty;
    }
}