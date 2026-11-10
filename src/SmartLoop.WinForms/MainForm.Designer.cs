namespace SmartLoop.WinForms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        componentsGrid = new DataGridView();
        bottomPanel = new Panel();
        nameLabel = new Label();
        nameTextBox = new TextBox();
        typeLabel = new Label();
        typeComboBox = new ComboBox();
        unitLabel = new Label();
        unitTextBox = new TextBox();
        minValueLabel = new Label();
        minValueTextBox = new TextBox();
        maxValueLabel = new Label();
        maxValueTextBox = new TextBox();
        descriptionLabel = new Label();
        descriptionTextBox = new TextBox();
        addButton = new Button();
        deleteButton = new Button();
        ((System.ComponentModel.ISupportInitialize)componentsGrid).BeginInit();
        bottomPanel.SuspendLayout();
        SuspendLayout();
        //
        // componentsGrid
        //
        componentsGrid.AllowUserToAddRows = false;
        componentsGrid.AllowUserToDeleteRows = false;
        componentsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        componentsGrid.Dock = DockStyle.Fill;
        componentsGrid.Location = new Point(0, 0);
        componentsGrid.Name = "componentsGrid";
        componentsGrid.ReadOnly = true;
        componentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        componentsGrid.Size = new Size(800, 320);
        componentsGrid.TabIndex = 0;
        //
        // bottomPanel
        //
        bottomPanel.Controls.Add(nameLabel);
        bottomPanel.Controls.Add(nameTextBox);
        bottomPanel.Controls.Add(typeLabel);
        bottomPanel.Controls.Add(typeComboBox);
        bottomPanel.Controls.Add(unitLabel);
        bottomPanel.Controls.Add(unitTextBox);
        bottomPanel.Controls.Add(minValueLabel);
        bottomPanel.Controls.Add(minValueTextBox);
        bottomPanel.Controls.Add(maxValueLabel);
        bottomPanel.Controls.Add(maxValueTextBox);
        bottomPanel.Controls.Add(descriptionLabel);
        bottomPanel.Controls.Add(descriptionTextBox);
        bottomPanel.Controls.Add(addButton);
        bottomPanel.Controls.Add(deleteButton);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new Point(0, 320);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Size = new Size(800, 130);
        bottomPanel.TabIndex = 2;
        //
        // nameLabel
        //
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(10, 10);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(41, 15);
        nameLabel.TabIndex = 0;
        nameLabel.Text = "Name";
        //
        // nameTextBox
        //
        nameTextBox.Location = new Point(10, 28);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(150, 23);
        nameTextBox.TabIndex = 1;
        //
        // typeLabel
        //
        typeLabel.AutoSize = true;
        typeLabel.Location = new Point(170, 10);
        typeLabel.Name = "typeLabel";
        typeLabel.Size = new Size(32, 15);
        typeLabel.TabIndex = 2;
        typeLabel.Text = "Type";
        //
        // typeComboBox
        //
        typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        typeComboBox.FormattingEnabled = true;
        typeComboBox.Location = new Point(170, 28);
        typeComboBox.Name = "typeComboBox";
        typeComboBox.Size = new Size(110, 23);
        typeComboBox.TabIndex = 3;
        //
        // unitLabel
        //
        unitLabel.AutoSize = true;
        unitLabel.Location = new Point(290, 10);
        unitLabel.Name = "unitLabel";
        unitLabel.Size = new Size(29, 15);
        unitLabel.TabIndex = 4;
        unitLabel.Text = "Unit";
        //
        // unitTextBox
        //
        unitTextBox.Location = new Point(290, 28);
        unitTextBox.Name = "unitTextBox";
        unitTextBox.Size = new Size(70, 23);
        unitTextBox.TabIndex = 5;
        //
        // minValueLabel
        //
        minValueLabel.AutoSize = true;
        minValueLabel.Location = new Point(370, 10);
        minValueLabel.Name = "minValueLabel";
        minValueLabel.Size = new Size(28, 15);
        minValueLabel.TabIndex = 6;
        minValueLabel.Text = "Min";
        //
        // minValueTextBox
        //
        minValueTextBox.Location = new Point(370, 28);
        minValueTextBox.Name = "minValueTextBox";
        minValueTextBox.Size = new Size(70, 23);
        minValueTextBox.TabIndex = 7;
        //
        // maxValueLabel
        //
        maxValueLabel.AutoSize = true;
        maxValueLabel.Location = new Point(450, 10);
        maxValueLabel.Name = "maxValueLabel";
        maxValueLabel.Size = new Size(30, 15);
        maxValueLabel.TabIndex = 8;
        maxValueLabel.Text = "Max";
        //
        // maxValueTextBox
        //
        maxValueTextBox.Location = new Point(450, 28);
        maxValueTextBox.Name = "maxValueTextBox";
        maxValueTextBox.Size = new Size(70, 23);
        maxValueTextBox.TabIndex = 9;
        //
        // descriptionLabel
        //
        descriptionLabel.AutoSize = true;
        descriptionLabel.Location = new Point(10, 62);
        descriptionLabel.Name = "descriptionLabel";
        descriptionLabel.Size = new Size(69, 15);
        descriptionLabel.TabIndex = 10;
        descriptionLabel.Text = "Description";
        //
        // descriptionTextBox
        //
        descriptionTextBox.Location = new Point(10, 80);
        descriptionTextBox.Name = "descriptionTextBox";
        descriptionTextBox.Size = new Size(510, 23);
        descriptionTextBox.TabIndex = 11;
        //
        // addButton
        //
        addButton.Location = new Point(550, 27);
        addButton.Name = "addButton";
        addButton.Size = new Size(90, 25);
        addButton.TabIndex = 12;
        addButton.Text = "Add";
        addButton.UseVisualStyleBackColor = true;
        //
        // deleteButton
        //
        deleteButton.Location = new Point(550, 79);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(90, 25);
        deleteButton.TabIndex = 13;
        deleteButton.Text = "Delete";
        deleteButton.UseVisualStyleBackColor = true;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(bottomPanel);
        Controls.Add(componentsGrid);
        Name = "MainForm";
        Text = "SmartLoop - Component Library";
        ((System.ComponentModel.ISupportInitialize)componentsGrid).EndInit();
        bottomPanel.ResumeLayout(false);
        bottomPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView componentsGrid;
    private Panel bottomPanel;
    private Label nameLabel;
    private TextBox nameTextBox;
    private Label typeLabel;
    private ComboBox typeComboBox;
    private Label unitLabel;
    private TextBox unitTextBox;
    private Label minValueLabel;
    private TextBox minValueTextBox;
    private Label maxValueLabel;
    private TextBox maxValueTextBox;
    private Label descriptionLabel;
    private TextBox descriptionTextBox;
    private Button addButton;
    private Button deleteButton;
}
