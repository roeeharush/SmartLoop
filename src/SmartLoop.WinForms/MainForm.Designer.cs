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
        ((System.ComponentModel.ISupportInitialize)componentsGrid).BeginInit();
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
        componentsGrid.Size = new Size(800, 450);
        componentsGrid.TabIndex = 0;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(componentsGrid);
        Name = "MainForm";
        Text = "SmartLoop - Component Library";
        ((System.ComponentModel.ISupportInitialize)componentsGrid).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView componentsGrid;
}
