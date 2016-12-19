namespace NDataflashEditor.Windows
{
	partial class DeviceConfiguratorWindow
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
            this.components = new System.ComponentModel.Container();
            this.MainContainer = new NDataflashEditor.UI.MultiPanel();
            this.WelcomePage = new NDataflashEditor.UI.MultiPanelPage();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.WorkspacePage = new NDataflashEditor.UI.MultiPanelPage();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.ConfigurationTab = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.DeveloperTabPage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ResetAndRestartButton = new System.Windows.Forms.Button();
            this.RestartButton = new System.Windows.Forms.Button();
            this.DefinitionTabPage = new System.Windows.Forms.TabPage();
            this.DataflashDefinitionEditorControl = new NDataflashEditor.Windows.DataflashDefinitionEditor.DataflashDefinitionEditorControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.DeviceNameLabel = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.MainStatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.MainContainer.SuspendLayout();
            this.WelcomePage.SuspendLayout();
            this.WorkspacePage.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.ConfigurationTab.SuspendLayout();
            this.DeveloperTabPage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.DefinitionTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MainStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Controls.Add(this.WelcomePage);
            this.MainContainer.Controls.Add(this.WorkspacePage);
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.SelectedPage = this.WorkspacePage;
            this.MainContainer.Size = new System.Drawing.Size(841, 546);
            this.MainContainer.TabIndex = 0;
            // 
            // WelcomePage
            // 
            this.WelcomePage.BackColor = System.Drawing.Color.White;
            this.WelcomePage.Controls.Add(this.WelcomeLabel);
            this.WelcomePage.Description = null;
            this.WelcomePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WelcomePage.Location = new System.Drawing.Point(0, 0);
            this.WelcomePage.Name = "WelcomePage";
            this.WelcomePage.Size = new System.Drawing.Size(841, 546);
            this.WelcomePage.TabIndex = 0;
            this.WelcomePage.Text = "WelcomePage";
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WelcomeLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WelcomeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WelcomeLabel.Location = new System.Drawing.Point(0, 0);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(841, 546);
            this.WelcomeLabel.TabIndex = 0;
            this.WelcomeLabel.Text = "Waiting for device...";
            this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WorkspacePage
            // 
            this.WorkspacePage.Controls.Add(this.MainTabControl);
            this.WorkspacePage.Controls.Add(this.panel1);
            this.WorkspacePage.Controls.Add(this.MainStatusBar);
            this.WorkspacePage.Description = null;
            this.WorkspacePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkspacePage.Location = new System.Drawing.Point(0, 0);
            this.WorkspacePage.Name = "WorkspacePage";
            this.WorkspacePage.Size = new System.Drawing.Size(841, 546);
            this.WorkspacePage.TabIndex = 1;
            this.WorkspacePage.Text = "WorkspacePage";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.ConfigurationTab);
            this.MainTabControl.Controls.Add(this.DeveloperTabPage);
            this.MainTabControl.Controls.Add(this.DefinitionTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 71);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(841, 453);
            this.MainTabControl.TabIndex = 0;
            // 
            // ConfigurationTab
            // 
            this.ConfigurationTab.Controls.Add(this.propertyGrid1);
            this.ConfigurationTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigurationTab.Name = "ConfigurationTab";
            this.ConfigurationTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigurationTab.Size = new System.Drawing.Size(833, 427);
            this.ConfigurationTab.TabIndex = 5;
            this.ConfigurationTab.Text = "Configuration";
            this.ConfigurationTab.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(827, 421);
            this.propertyGrid1.TabIndex = 0;
            // 
            // DeveloperTabPage
            // 
            this.DeveloperTabPage.Controls.Add(this.groupBox4);
            this.DeveloperTabPage.Location = new System.Drawing.Point(4, 22);
            this.DeveloperTabPage.Name = "DeveloperTabPage";
            this.DeveloperTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DeveloperTabPage.Size = new System.Drawing.Size(833, 427);
            this.DeveloperTabPage.TabIndex = 2;
            this.DeveloperTabPage.Text = "Developer";
            this.DeveloperTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ResetAndRestartButton);
            this.groupBox4.Controls.Add(this.RestartButton);
            this.groupBox4.Location = new System.Drawing.Point(10, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(318, 48);
            this.groupBox4.TabIndex = 71;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Device manipulations";
            // 
            // ResetAndRestartButton
            // 
            this.ResetAndRestartButton.Location = new System.Drawing.Point(86, 16);
            this.ResetAndRestartButton.Name = "ResetAndRestartButton";
            this.ResetAndRestartButton.Size = new System.Drawing.Size(227, 25);
            this.ResetAndRestartButton.TabIndex = 41;
            this.ResetAndRestartButton.Text = "Reset dafaflash and restart";
            this.ResetAndRestartButton.UseVisualStyleBackColor = true;
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(7, 16);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(75, 25);
            this.RestartButton.TabIndex = 40;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            // 
            // DefinitionTabPage
            // 
            this.DefinitionTabPage.Controls.Add(this.DataflashDefinitionEditorControl);
            this.DefinitionTabPage.Location = new System.Drawing.Point(4, 22);
            this.DefinitionTabPage.Name = "DefinitionTabPage";
            this.DefinitionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DefinitionTabPage.Size = new System.Drawing.Size(833, 427);
            this.DefinitionTabPage.TabIndex = 3;
            this.DefinitionTabPage.Text = "Dataflash Definition";
            this.DefinitionTabPage.UseVisualStyleBackColor = true;
            // 
            // DataflashDefinitionEditorControl
            // 
            this.DataflashDefinitionEditorControl.DataflashManager = null;
            this.DataflashDefinitionEditorControl.Definition = null;
            this.DataflashDefinitionEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataflashDefinitionEditorControl.Location = new System.Drawing.Point(3, 3);
            this.DataflashDefinitionEditorControl.Name = "DataflashDefinitionEditorControl";
            this.DataflashDefinitionEditorControl.Size = new System.Drawing.Size(827, 421);
            this.DataflashDefinitionEditorControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.DownloadButton);
            this.panel1.Controls.Add(this.DeviceNameLabel);
            this.panel1.Controls.Add(this.UploadButton);
            this.panel1.Controls.Add(this.SaveAsButton);
            this.panel1.Controls.Add(this.ResetButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 71);
            this.panel1.TabIndex = 37;
            // 
            // button1
            // 
            this.button1.Image = global::NDataflashEditor.Properties.Resources.save_as;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(520, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 25);
            this.button1.TabIndex = 37;
            this.button1.Text = "Old Editor";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DownloadButton
            // 
            this.DownloadButton.Image = global::NDataflashEditor.Properties.Resources.download_settings;
            this.DownloadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DownloadButton.Location = new System.Drawing.Point(11, 12);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(124, 25);
            this.DownloadButton.TabIndex = 1;
            this.DownloadButton.Text = "Refresh settings";
            this.DownloadButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DownloadButton.UseVisualStyleBackColor = true;
            // 
            // DeviceNameLabel
            // 
            this.DeviceNameLabel.AutoSize = true;
            this.DeviceNameLabel.BackColor = System.Drawing.Color.White;
            this.DeviceNameLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeviceNameLabel.Location = new System.Drawing.Point(14, 43);
            this.DeviceNameLabel.Name = "DeviceNameLabel";
            this.DeviceNameLabel.Size = new System.Drawing.Size(99, 19);
            this.DeviceNameLabel.TabIndex = 0;
            this.DeviceNameLabel.Text = "Device name";
            // 
            // UploadButton
            // 
            this.UploadButton.Image = global::NDataflashEditor.Properties.Resources.upload_settings;
            this.UploadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UploadButton.Location = new System.Drawing.Point(141, 12);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(110, 25);
            this.UploadButton.TabIndex = 2;
            this.UploadButton.Text = "Upload settings";
            this.UploadButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UploadButton.UseVisualStyleBackColor = true;
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Image = global::NDataflashEditor.Properties.Resources.save_as;
            this.SaveAsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveAsButton.Location = new System.Drawing.Point(393, 12);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(105, 25);
            this.SaveAsButton.TabIndex = 36;
            this.SaveAsButton.Text = "Save settings";
            this.SaveAsButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveAsButton.UseVisualStyleBackColor = true;
            // 
            // ResetButton
            // 
            this.ResetButton.Image = global::NDataflashEditor.Properties.Resources.reset_settings;
            this.ResetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetButton.Location = new System.Drawing.Point(257, 12);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(107, 25);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset settings";
            this.ResetButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResetButton.UseVisualStyleBackColor = true;
            // 
            // MainStatusBar
            // 
            this.MainStatusBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainStatusBar.GripMargin = new System.Windows.Forms.Padding(0);
            this.MainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressLabel});
            this.MainStatusBar.Location = new System.Drawing.Point(0, 524);
            this.MainStatusBar.Name = "MainStatusBar";
            this.MainStatusBar.Size = new System.Drawing.Size(841, 22);
            this.MainStatusBar.SizingGrip = false;
            this.MainStatusBar.TabIndex = 35;
            this.MainStatusBar.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(788, 17);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "StatusLabel";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(38, 17);
            this.ProgressLabel.Text = "Ready";
            // 
            // MainErrorProvider
            // 
            this.MainErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.MainErrorProvider.ContainerControl = this;
            // 
            // DeviceConfiguratorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(841, 546);
            this.Controls.Add(this.MainContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DeviceConfiguratorWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Device Configuration";
            this.MainContainer.ResumeLayout(false);
            this.WelcomePage.ResumeLayout(false);
            this.WorkspacePage.ResumeLayout(false);
            this.WorkspacePage.PerformLayout();
            this.MainTabControl.ResumeLayout(false);
            this.ConfigurationTab.ResumeLayout(false);
            this.DeveloperTabPage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.DefinitionTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.MainStatusBar.ResumeLayout(false);
            this.MainStatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainErrorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private UI.MultiPanel MainContainer;
		private UI.MultiPanelPage WelcomePage;
		private UI.MultiPanelPage WorkspacePage;
		private System.Windows.Forms.Label WelcomeLabel;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.Label DeviceNameLabel;
		private System.Windows.Forms.Button DownloadButton;
		private System.Windows.Forms.Button UploadButton;
		private System.Windows.Forms.Button ResetButton;
		private System.Windows.Forms.TabPage DeveloperTabPage;
		private System.Windows.Forms.ErrorProvider MainErrorProvider;
		private System.Windows.Forms.StatusStrip MainStatusBar;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel ProgressLabel;
		private System.Windows.Forms.Button RestartButton;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button ResetAndRestartButton;
        private System.Windows.Forms.TabPage DefinitionTabPage;
        private DataflashDefinitionEditor.DataflashDefinitionEditorControl DataflashDefinitionEditorControl;
        private System.Windows.Forms.TabPage ConfigurationTab;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button SaveAsButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}