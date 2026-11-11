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
            resultsTextBox = new System.Windows.Forms.TextBox();
            topPanel = new System.Windows.Forms.Panel();
            searchButton = new System.Windows.Forms.Button();
            xpathTextBox = new System.Windows.Forms.TextBox();
            bottomPanel = new System.Windows.Forms.Panel();
            importButton = new System.Windows.Forms.Button();
            exportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)systemsGrid).BeginInit();
            topPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            //
            // systemsGrid
            //
            systemsGrid.AllowUserToAddRows = false;
            systemsGrid.AllowUserToDeleteRows = false;
            systemsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            systemsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            systemsGrid.Location = new System.Drawing.Point(0, 40);
            systemsGrid.MultiSelect = false;
            systemsGrid.Name = "systemsGrid";
            systemsGrid.ReadOnly = true;
            systemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            systemsGrid.Size = new System.Drawing.Size(640, 280);
            systemsGrid.TabIndex = 0;
            //
            // resultsTextBox
            //
            resultsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            resultsTextBox.Location = new System.Drawing.Point(0, 320);
            resultsTextBox.Multiline = true;
            resultsTextBox.Name = "resultsTextBox";
            resultsTextBox.ReadOnly = true;
            resultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            resultsTextBox.Size = new System.Drawing.Size(640, 150);
            resultsTextBox.TabIndex = 1;
            resultsTextBox.WordWrap = false;
            //
            // topPanel
            //
            topPanel.Controls.Add(searchButton);
            topPanel.Controls.Add(xpathTextBox);
            topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            topPanel.Location = new System.Drawing.Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new System.Drawing.Size(640, 40);
            topPanel.TabIndex = 2;
            //
            // searchButton
            //
            searchButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            searchButton.Location = new System.Drawing.Point(530, 8);
            searchButton.Name = "searchButton";
            searchButton.Size = new System.Drawing.Size(100, 25);
            searchButton.TabIndex = 1;
            searchButton.Text = "XPath Search";
            searchButton.UseVisualStyleBackColor = true;
            //
            // xpathTextBox
            //
            xpathTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            xpathTextBox.Location = new System.Drawing.Point(10, 9);
            xpathTextBox.Name = "xpathTextBox";
            xpathTextBox.Size = new System.Drawing.Size(510, 23);
            xpathTextBox.TabIndex = 0;
            xpathTextBox.Text = "//Component[@role='Sensor']";
            //
            // bottomPanel
            //
            bottomPanel.Controls.Add(importButton);
            bottomPanel.Controls.Add(exportButton);
            bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            bottomPanel.Location = new System.Drawing.Point(0, 470);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new System.Drawing.Size(640, 50);
            bottomPanel.TabIndex = 3;
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
            ClientSize = new System.Drawing.Size(640, 520);
            Controls.Add(systemsGrid);
            Controls.Add(resultsTextBox);
            Controls.Add(topPanel);
            Controls.Add(bottomPanel);
            Name = "SystemsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Saved Systems";
            ((System.ComponentModel.ISupportInitialize)systemsGrid).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView systemsGrid;
        private System.Windows.Forms.TextBox resultsTextBox;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox xpathTextBox;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;
    }
}
