using SmartLoop.Core.Models;
using SmartLoop.Core.Views;

namespace SmartLoop.WinForms;

public partial class SystemBuilderForm : Form, ISystemBuilderView
{
    public event EventHandler? ViewLoaded;
    public event EventHandler? SaveRequested;
    public event EventHandler? AiSuggestRequested;

    public SystemBuilderForm()
    {
        InitializeComponent();

        aiSuggestButton.Click += (sender, e) => AiSuggestRequested?.Invoke(this, EventArgs.Empty);

        Load += (sender, e) => ViewLoaded?.Invoke(this, EventArgs.Empty);
        saveButton.Click += (sender, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
    }

    public string SystemName => nameTextBox.Text;

    public string SystemDescription => descriptionTextBox.Text;

    public IReadOnlyList<int> SelectedComponentIds =>
        componentsCheckedListBox.CheckedItems.Cast<Component>().Select(c => c.Id).ToList();

    public void ShowComponents(IReadOnlyList<Component> components)
    {
        componentsCheckedListBox.DisplayMember = nameof(Component.Name);
        componentsCheckedListBox.Items.Clear();

        foreach (var component in components)
        {
            componentsCheckedListBox.Items.Add(component);
        }
    }

    public void SelectComponents(IReadOnlyList<int> componentIds)
    {
        for (var i = 0; i < componentsCheckedListBox.Items.Count; i++)
        {
            var component = (Component)componentsCheckedListBox.Items[i];
            componentsCheckedListBox.SetItemChecked(i, componentIds.Contains(component.Id));
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

    public void CloseView()
    {
        Close();
    }
}
