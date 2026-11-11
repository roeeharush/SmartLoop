namespace SmartLoop.WinForms
{
    partial class SystemsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            systemsGrid = new System.Windows.Forms.DataGridView();
            bottomPanel = new System.Windows.Forms.Panel();
            importButton = new System.Windows.Forms.Button();
            exportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)systemsGrid).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            //
            // systemsGrid
            //
            systemsGrid.AllowUserToAddRows = false;
            systemsGrid.AllowUserToDeleteRows = false;
            systemsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            systemsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            systemsGrid.Location = new System.Drawing.Point(0, 0);
            systemsGrid.MultiSelect = false;
            systemsGrid.Name = "systemsGrid";
            systemsGrid.ReadOnly = true;
            systemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            systemsGrid.Size = new System.Drawing.Size(584, 311);
            systemsGrid.TabIndex = 0;
            //
            // bottomPanel
            //
            bottomPanel.Controls.Add(importButton);
            bottomPanel.Controls.Add(exportButton);
            bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            bottomPanel.Location = new System.Drawing.Point(0, 311);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new System.Drawing.Size(584, 50);
            bottomPanel.TabIndex = 1;
            //
            // importButton
            //
            importButton.Location = new System.Drawing.Point(130, 10);
            importButton.Name = "importButton";
            importButton.Size = new System.Drawing.Size(110, 30);
            importButton.TabIndex = 1;
            importButton.Text = "Import XML...";
            importButton.UseVisualStyleBackColor = true;
            //
            // exportButton
            //
            exportButton.Location = new System.Drawing.Point(10, 10);
            exportButton.Name = "exportButton";
            exportButton.Size = new System.Drawing.Size(110, 30);
            exportButton.TabIndex = 0;
            exportButton.Text = "Export XML...";
            exportButton.UseVisualStyleBackColor = true;
            //
            // SystemsForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 361);
            Controls.Add(systemsGrid);
            Controls.Add(bottomPanel);
            Name = "SystemsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Saved Systems";
            ((System.ComponentModel.ISupportInitialize)systemsGrid).EndInit();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView systemsGrid;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;
    }
}
