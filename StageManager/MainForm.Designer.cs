namespace BrawlStageManager {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.stageInfoControl1 = new BrawlStageManager.StageInfoControl();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.rightPanel = new System.Windows.Forms.Panel();
			this.modelPanel1 = new System.Windows.Forms.ModelPanel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moduleFileDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.verifyrelStageIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.useFullrelNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renderModels = new System.Windows.Forms.ToolStripMenuItem();
			this.selmapMarkPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.useAFixedStageListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.prevbaseSize = new System.Windows.Forms.ToolStripMenuItem();
			this.warningResizingIsQuiteUglyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.prevbaseOriginalSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x128ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x88ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.frontstnameSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.frontstnameOriginalSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x56ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selmapMarkSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selmapMarkOriginalSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x56ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.texturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.portraitViewer1 = new BrawlStageManager.PortraitViewer();
			this.addmissingPAT0EntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(451, 462);
			this.splitContainer1.SplitterDistance = 135;
			this.splitContainer1.TabIndex = 0;
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(135, 462);
			this.listBox1.TabIndex = 0;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.stageInfoControl1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer2.Size = new System.Drawing.Size(312, 462);
			this.splitContainer2.SplitterDistance = 72;
			this.splitContainer2.TabIndex = 0;
			// 
			// stageInfoControl1
			// 
			this.stageInfoControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stageInfoControl1.Location = new System.Drawing.Point(0, 0);
			this.stageInfoControl1.Name = "stageInfoControl1";
			this.stageInfoControl1.RelFile = null;
			this.stageInfoControl1.ShouldVerifyIDs = false;
			this.stageInfoControl1.Size = new System.Drawing.Size(308, 68);
			this.stageInfoControl1.TabIndex = 0;
			this.stageInfoControl1.UseRelDescription = false;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.rightPanel);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.modelPanel1);
			this.splitContainer3.Size = new System.Drawing.Size(308, 382);
			this.splitContainer3.SplitterDistance = 129;
			this.splitContainer3.TabIndex = 4;
			// 
			// rightPanel
			// 
			this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rightPanel.Location = new System.Drawing.Point(0, 0);
			this.rightPanel.Name = "rightPanel";
			this.rightPanel.Size = new System.Drawing.Size(308, 129);
			this.rightPanel.TabIndex = 3;
			// 
			// modelPanel1
			// 
			this.modelPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.modelPanel1.InitialYFactor = 100;
			this.modelPanel1.InitialZoomFactor = 5;
			this.modelPanel1.Location = new System.Drawing.Point(0, 0);
			this.modelPanel1.Name = "modelPanel1";
			this.modelPanel1.RotationScale = 0.1F;
			this.modelPanel1.Size = new System.Drawing.Size(308, 249);
			this.modelPanel1.TabIndex = 2;
			this.modelPanel1.TranslationScale = 0.05F;
			this.modelPanel1.ZoomScale = 2.5F;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.resizeToolStripMenuItem,
            this.texturesToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(684, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDirectoryToolStripMenuItem,
            this.exportStageToolStripMenuItem,
            this.exportAllToolStripMenuItem,
            this.addmissingPAT0EntriesToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// changeDirectoryToolStripMenuItem
			// 
			this.changeDirectoryToolStripMenuItem.Name = "changeDirectoryToolStripMenuItem";
			this.changeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.changeDirectoryToolStripMenuItem.Text = "Change directory...";
			this.changeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.changeDirectoryToolStripMenuItem_Click);
			// 
			// exportStageToolStripMenuItem
			// 
			this.exportStageToolStripMenuItem.Name = "exportStageToolStripMenuItem";
			this.exportStageToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.exportStageToolStripMenuItem.Text = "Export stage";
			this.exportStageToolStripMenuItem.Click += new System.EventHandler(this.exportStageToolStripMenuItem_Click);
			// 
			// exportAllToolStripMenuItem
			// 
			this.exportAllToolStripMenuItem.Name = "exportAllToolStripMenuItem";
			this.exportAllToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.exportAllToolStripMenuItem.Text = "Export all stages";
			this.exportAllToolStripMenuItem.Click += new System.EventHandler(this.exportAllToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moduleFileDirectoryToolStripMenuItem,
            this.verifyrelStageIDsToolStripMenuItem,
            this.useFullrelNamesToolStripMenuItem,
            this.renderModels,
            this.selmapMarkPreviewToolStripMenuItem,
            this.useAFixedStageListToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// moduleFileDirectoryToolStripMenuItem
			// 
			this.moduleFileDirectoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sameToolStripMenuItem,
            this.moduleToolStripMenuItem});
			this.moduleFileDirectoryToolStripMenuItem.Name = "moduleFileDirectoryToolStripMenuItem";
			this.moduleFileDirectoryToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.moduleFileDirectoryToolStripMenuItem.Text = "Module file directory";
			// 
			// sameToolStripMenuItem
			// 
			this.sameToolStripMenuItem.Name = "sameToolStripMenuItem";
			this.sameToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.sameToolStripMenuItem.Text = "Same";
			this.sameToolStripMenuItem.Click += new System.EventHandler(this.sameToolStripMenuItem_Click);
			// 
			// moduleToolStripMenuItem
			// 
			this.moduleToolStripMenuItem.Checked = true;
			this.moduleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.moduleToolStripMenuItem.Name = "moduleToolStripMenuItem";
			this.moduleToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.moduleToolStripMenuItem.Text = "..\\..\\module";
			this.moduleToolStripMenuItem.Click += new System.EventHandler(this.moduleToolStripMenuItem_Click);
			// 
			// verifyrelStageIDsToolStripMenuItem
			// 
			this.verifyrelStageIDsToolStripMenuItem.Checked = true;
			this.verifyrelStageIDsToolStripMenuItem.CheckOnClick = true;
			this.verifyrelStageIDsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.verifyrelStageIDsToolStripMenuItem.Name = "verifyrelStageIDsToolStripMenuItem";
			this.verifyrelStageIDsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.verifyrelStageIDsToolStripMenuItem.Text = "Verify .rel stage IDs";
			this.verifyrelStageIDsToolStripMenuItem.Click += new System.EventHandler(this.verifyrelStageIDsToolStripMenuItem_Click);
			// 
			// useFullrelNamesToolStripMenuItem
			// 
			this.useFullrelNamesToolStripMenuItem.Checked = true;
			this.useFullrelNamesToolStripMenuItem.CheckOnClick = true;
			this.useFullrelNamesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.useFullrelNamesToolStripMenuItem.Name = "useFullrelNamesToolStripMenuItem";
			this.useFullrelNamesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.useFullrelNamesToolStripMenuItem.Text = "Use full .rel names";
			this.useFullrelNamesToolStripMenuItem.Click += new System.EventHandler(this.useFullrelNamesToolStripMenuItem_Click);
			// 
			// renderModels
			// 
			this.renderModels.Checked = true;
			this.renderModels.CheckOnClick = true;
			this.renderModels.CheckState = System.Windows.Forms.CheckState.Checked;
			this.renderModels.Name = "renderModels";
			this.renderModels.Size = new System.Drawing.Size(184, 22);
			this.renderModels.Text = "Render models";
			// 
			// selmapMarkPreviewToolStripMenuItem
			// 
			this.selmapMarkPreviewToolStripMenuItem.Checked = true;
			this.selmapMarkPreviewToolStripMenuItem.CheckOnClick = true;
			this.selmapMarkPreviewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.selmapMarkPreviewToolStripMenuItem.Name = "selmapMarkPreviewToolStripMenuItem";
			this.selmapMarkPreviewToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.selmapMarkPreviewToolStripMenuItem.Text = "SelmapMark preview";
			this.selmapMarkPreviewToolStripMenuItem.Click += new System.EventHandler(this.selmapMarkPreviewToolStripMenuItem_Click);
			// 
			// useAFixedStageListToolStripMenuItem
			// 
			this.useAFixedStageListToolStripMenuItem.CheckOnClick = true;
			this.useAFixedStageListToolStripMenuItem.Name = "useAFixedStageListToolStripMenuItem";
			this.useAFixedStageListToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.useAFixedStageListToolStripMenuItem.Text = "Use a fixed stage list";
			this.useAFixedStageListToolStripMenuItem.Click += new System.EventHandler(this.useAFixedStageListToolStripMenuItem_Click);
			// 
			// resizeToolStripMenuItem
			// 
			this.resizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prevbaseSize,
            this.frontstnameSizeToolStripMenuItem,
            this.selmapMarkSizeToolStripMenuItem});
			this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
			this.resizeToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
			this.resizeToolStripMenuItem.Text = "Auto-resize";
			// 
			// prevbaseSize
			// 
			this.prevbaseSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.warningResizingIsQuiteUglyToolStripMenuItem,
            this.prevbaseOriginalSizeToolStripMenuItem,
            this.x128ToolStripMenuItem,
            this.x88ToolStripMenuItem});
			this.prevbaseSize.Name = "prevbaseSize";
			this.prevbaseSize.Size = new System.Drawing.Size(168, 22);
			this.prevbaseSize.Text = "Prevbase size:";
			// 
			// warningResizingIsQuiteUglyToolStripMenuItem
			// 
			this.warningResizingIsQuiteUglyToolStripMenuItem.Enabled = false;
			this.warningResizingIsQuiteUglyToolStripMenuItem.Name = "warningResizingIsQuiteUglyToolStripMenuItem";
			this.warningResizingIsQuiteUglyToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.warningResizingIsQuiteUglyToolStripMenuItem.Text = "(Warning: resizing is quite ugly)";
			// 
			// prevbaseOriginalSizeToolStripMenuItem
			// 
			this.prevbaseOriginalSizeToolStripMenuItem.Checked = true;
			this.prevbaseOriginalSizeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.prevbaseOriginalSizeToolStripMenuItem.Name = "prevbaseOriginalSizeToolStripMenuItem";
			this.prevbaseOriginalSizeToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.prevbaseOriginalSizeToolStripMenuItem.Text = "Off";
			this.prevbaseOriginalSizeToolStripMenuItem.Click += new System.EventHandler(this.prevbaseSizeToolStripMenuItem_Click);
			// 
			// x128ToolStripMenuItem
			// 
			this.x128ToolStripMenuItem.Name = "x128ToolStripMenuItem";
			this.x128ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.x128ToolStripMenuItem.Text = "128x128";
			this.x128ToolStripMenuItem.Click += new System.EventHandler(this.prevbaseSizeToolStripMenuItem_Click);
			// 
			// x88ToolStripMenuItem
			// 
			this.x88ToolStripMenuItem.Name = "x88ToolStripMenuItem";
			this.x88ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.x88ToolStripMenuItem.Text = "88x88";
			this.x88ToolStripMenuItem.Click += new System.EventHandler(this.prevbaseSizeToolStripMenuItem_Click);
			// 
			// frontstnameSizeToolStripMenuItem
			// 
			this.frontstnameSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frontstnameOriginalSizeToolStripMenuItem,
            this.x56ToolStripMenuItem});
			this.frontstnameSizeToolStripMenuItem.Name = "frontstnameSizeToolStripMenuItem";
			this.frontstnameSizeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.frontstnameSizeToolStripMenuItem.Text = "Frontstname size:";
			// 
			// frontstnameOriginalSizeToolStripMenuItem
			// 
			this.frontstnameOriginalSizeToolStripMenuItem.Checked = true;
			this.frontstnameOriginalSizeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.frontstnameOriginalSizeToolStripMenuItem.Name = "frontstnameOriginalSizeToolStripMenuItem";
			this.frontstnameOriginalSizeToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.frontstnameOriginalSizeToolStripMenuItem.Text = "Off";
			this.frontstnameOriginalSizeToolStripMenuItem.Click += new System.EventHandler(this.frontstnameSizeToolStripMenuItem_Click);
			// 
			// x56ToolStripMenuItem
			// 
			this.x56ToolStripMenuItem.Name = "x56ToolStripMenuItem";
			this.x56ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.x56ToolStripMenuItem.Text = "104x56";
			this.x56ToolStripMenuItem.Click += new System.EventHandler(this.frontstnameSizeToolStripMenuItem_Click);
			// 
			// selmapMarkSizeToolStripMenuItem
			// 
			this.selmapMarkSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selmapMarkOriginalSizeToolStripMenuItem,
            this.x56ToolStripMenuItem1});
			this.selmapMarkSizeToolStripMenuItem.Name = "selmapMarkSizeToolStripMenuItem";
			this.selmapMarkSizeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.selmapMarkSizeToolStripMenuItem.Text = "Selmap mark size:";
			// 
			// selmapMarkOriginalSizeToolStripMenuItem
			// 
			this.selmapMarkOriginalSizeToolStripMenuItem.Checked = true;
			this.selmapMarkOriginalSizeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.selmapMarkOriginalSizeToolStripMenuItem.Name = "selmapMarkOriginalSizeToolStripMenuItem";
			this.selmapMarkOriginalSizeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.selmapMarkOriginalSizeToolStripMenuItem.Text = "Off";
			// 
			// x56ToolStripMenuItem1
			// 
			this.x56ToolStripMenuItem1.Name = "x56ToolStripMenuItem1";
			this.x56ToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
			this.x56ToolStripMenuItem1.Text = "60x56";
			// 
			// texturesToolStripMenuItem
			// 
			this.texturesToolStripMenuItem.Name = "texturesToolStripMenuItem";
			this.texturesToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
			this.texturesToolStripMenuItem.Text = "Model Textures";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// portraitViewer1
			// 
			this.portraitViewer1.AutoSize = true;
			this.portraitViewer1.Dock = System.Windows.Forms.DockStyle.Right;
			this.portraitViewer1.Location = new System.Drawing.Point(451, 24);
			this.portraitViewer1.Name = "portraitViewer1";
			this.portraitViewer1.Size = new System.Drawing.Size(233, 462);
			this.portraitViewer1.TabIndex = 2;
			// 
			// addmissingPAT0EntriesToolStripMenuItem
			// 
			this.addmissingPAT0EntriesToolStripMenuItem.Name = "addmissingPAT0EntriesToolStripMenuItem";
			this.addmissingPAT0EntriesToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.addmissingPAT0EntriesToolStripMenuItem.Text = "Add \"missing\" PAT0 entries";
			this.addmissingPAT0EntriesToolStripMenuItem.Click += new System.EventHandler(this.addmissingPAT0EntriesToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 486);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.portraitViewer1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Brawl Stage Manager";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private StageInfoControl stageInfoControl1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moduleFileDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moduleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem verifyrelStageIDsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem useFullrelNamesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ModelPanel modelPanel1;
		private System.Windows.Forms.ToolStripMenuItem texturesToolStripMenuItem;
		private System.Windows.Forms.Panel rightPanel;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private PortraitViewer portraitViewer1;
		private System.Windows.Forms.ToolStripMenuItem prevbaseSize;
		private System.Windows.Forms.ToolStripMenuItem prevbaseOriginalSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x128ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x88ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem warningResizingIsQuiteUglyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem frontstnameSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem frontstnameOriginalSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x56ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renderModels;
		private System.Windows.Forms.ToolStripMenuItem exportAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportStageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selmapMarkSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selmapMarkOriginalSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x56ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selmapMarkPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem useAFixedStageListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addmissingPAT0EntriesToolStripMenuItem;



	}
}

