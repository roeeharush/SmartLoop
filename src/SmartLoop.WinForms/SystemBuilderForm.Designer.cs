namespace SmartLoop.WinForms
{
    partial class SystemBuilderForm
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
            componentsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            topPanel = new System.Windows.Forms.Panel();
            descriptionTextBox = new System.Windows.Forms.TextBox();
            descriptionLabel = new System.Windows.Forms.Label();
            nameTextBox = new System.Windows.Forms.TextBox();
            nameLabel = new System.Windows.Forms.Label();
            bottomPanel = new System.Windows.Forms.Panel();
            cancelButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            aiSuggestButton = new System.Windows.Forms.Button();
            topPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            //
            // componentsCheckedListBox
            //
            componentsCheckedListBox.CheckOnClick = true;
            componentsCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            componentsCheckedListBox.FormattingEnabled = true;
            componentsCheckedListBox.Location = new System.Drawing.Point(0, 90);
            componentsCheckedListBox.Name = "componentsCheckedListBox";
            componentsCheckedListBox.Size = new System.Drawing.Size(484, 321);
            componentsCheckedListBox.TabIndex = 2;
            //
            // topPanel
            //
            topPanel.Controls.Add(descriptionTextBox);
            topPanel.Controls.Add(descriptionLabel);
            topPanel.Controls.Add(nameTextBox);
            topPanel.Controls.Add(nameLabel);
            topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            topPanel.Location = new System.Drawing.Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new System.Drawing.Size(484, 90);
            topPanel.TabIndex = 0;
            //
            // descriptionTextBox
            //
            descriptionTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            descriptionTextBox.Location = new System.Drawing.Point(100, 47);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.Size = new System.Drawing.Size(370, 27);
            descriptionTextBox.TabIndex = 3;
            //
            // descriptionLabel
            //
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(12, 50);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(82, 20);
            descriptionLabel.TabIndex = 2;
            descriptionLabel.Text = "Description:";
            //
            // nameTextBox
            //
            nameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            nameTextBox.Location = new System.Drawing.Point(100, 12);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new System.Drawing.Size(370, 27);
            nameTextBox.TabIndex = 1;
            //
            // nameLabel
            //
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(12, 15);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(52, 20);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name:";
            //
            // bottomPanel
            //
            bottomPanel.Controls.Add(cancelButton);
            bottomPanel.Controls.Add(saveButton);
            bottomPanel.Controls.Add(aiSuggestButton);
            bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            bottomPanel.Location = new System.Drawing.Point(0, 411);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new System.Drawing.Size(484, 50);
            bottomPanel.TabIndex = 1;
            //
            // cancelButton
            //
            cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(390, 10);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(80, 30);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            //
            // saveButton
            //
            saveButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            saveButton.Location = new System.Drawing.Point(300, 10);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(80, 30);
            saveButton.TabIndex = 0;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            //
            // aiSuggestButton
            //
            aiSuggestButton.Location = new System.Drawing.Point(12, 10);
            aiSuggestButton.Name = "aiSuggestButton";
            aiSuggestButton.Size = new System.Drawing.Size(110, 30);
            aiSuggestButton.TabIndex = 2;
            aiSuggestButton.Text = "AI Suggest";
            aiSuggestButton.UseVisualStyleBackColor = true;
            //
            // SystemBuilderForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(484, 461);
            Controls.Add(componentsCheckedListBox);
            Controls.Add(topPanel);
            Controls.Add(bottomPanel);
            Name = "SystemBuilderForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Build Control System";
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.CheckedListBox componentsCheckedListBox;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button aiSuggestButton;
    }
}
