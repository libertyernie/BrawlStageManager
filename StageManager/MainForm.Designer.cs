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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moduleFileDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.verifyrelStageIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.useFullrelNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.modelPanel1 = new System.Windows.Forms.ModelPanel();
			this.texturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rightPanel = new System.Windows.Forms.Panel();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.stageInfoControl1 = new BrawlStageManager.StageInfoControl();
			this.portraitViewer1 = new BrawlStageManager.PortraitViewer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
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
			this.splitContainer1.Size = new System.Drawing.Size(414, 290);
			this.splitContainer1.SplitterDistance = 125;
			this.splitContainer1.TabIndex = 0;
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(125, 290);
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
			this.splitContainer2.Size = new System.Drawing.Size(285, 290);
			this.splitContainer2.SplitterDistance = 43;
			this.splitContainer2.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.texturesToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(592, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDirectoryToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// changeDirectoryToolStripMenuItem
			// 
			this.changeDirectoryToolStripMenuItem.Name = "changeDirectoryToolStripMenuItem";
			this.changeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.changeDirectoryToolStripMenuItem.Text = "Change directory...";
			this.changeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.changeDirectoryToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moduleFileDirectoryToolStripMenuItem,
            this.verifyrelStageIDsToolStripMenuItem,
            this.useFullrelNamesToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// moduleFileDirectoryToolStripMenuItem
			// 
			this.moduleFileDirectoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sameToolStripMenuItem,
            this.moduleToolStripMenuItem});
			this.moduleFileDirectoryToolStripMenuItem.Name = "moduleFileDirectoryToolStripMenuItem";
			this.moduleFileDirectoryToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.moduleFileDirectoryToolStripMenuItem.Text = "Module file directory";
			// 
			// sameToolStripMenuItem
			// 
			this.sameToolStripMenuItem.Name = "sameToolStripMenuItem";
			this.sameToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.sameToolStripMenuItem.Text = "Same";
			this.sameToolStripMenuItem.Click += new System.EventHandler(this.sameToolStripMenuItem_Click);
			// 
			// moduleToolStripMenuItem
			// 
			this.moduleToolStripMenuItem.Checked = true;
			this.moduleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.moduleToolStripMenuItem.Name = "moduleToolStripMenuItem";
			this.moduleToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.moduleToolStripMenuItem.Text = "..\\..\\module";
			this.moduleToolStripMenuItem.Click += new System.EventHandler(this.moduleToolStripMenuItem_Click);
			// 
			// verifyrelStageIDsToolStripMenuItem
			// 
			this.verifyrelStageIDsToolStripMenuItem.Checked = true;
			this.verifyrelStageIDsToolStripMenuItem.CheckOnClick = true;
			this.verifyrelStageIDsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.verifyrelStageIDsToolStripMenuItem.Name = "verifyrelStageIDsToolStripMenuItem";
			this.verifyrelStageIDsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.verifyrelStageIDsToolStripMenuItem.Text = "Verify .rel stage IDs";
			this.verifyrelStageIDsToolStripMenuItem.Click += new System.EventHandler(this.verifyrelStageIDsToolStripMenuItem_Click);
			// 
			// useFullrelNamesToolStripMenuItem
			// 
			this.useFullrelNamesToolStripMenuItem.Checked = true;
			this.useFullrelNamesToolStripMenuItem.CheckOnClick = true;
			this.useFullrelNamesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.useFullrelNamesToolStripMenuItem.Name = "useFullrelNamesToolStripMenuItem";
			this.useFullrelNamesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.useFullrelNamesToolStripMenuItem.Text = "Use full .rel names";
			this.useFullrelNamesToolStripMenuItem.Click += new System.EventHandler(this.useFullrelNamesToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// modelPanel1
			// 
			this.modelPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.modelPanel1.InitialYFactor = 100;
			this.modelPanel1.InitialZoomFactor = 5;
			this.modelPanel1.Location = new System.Drawing.Point(0, 0);
			this.modelPanel1.Name = "modelPanel1";
			this.modelPanel1.RotationScale = 0.1F;
			this.modelPanel1.Size = new System.Drawing.Size(281, 160);
			this.modelPanel1.TabIndex = 2;
			this.modelPanel1.TranslationScale = 0.05F;
			this.modelPanel1.ZoomScale = 2.5F;
			// 
			// texturesToolStripMenuItem
			// 
			this.texturesToolStripMenuItem.Name = "texturesToolStripMenuItem";
			this.texturesToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
			this.texturesToolStripMenuItem.Text = "Textures";
			// 
			// rightPanel
			// 
			this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rightPanel.Location = new System.Drawing.Point(0, 0);
			this.rightPanel.Name = "rightPanel";
			this.rightPanel.Size = new System.Drawing.Size(281, 75);
			this.rightPanel.TabIndex = 3;
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
			this.splitContainer3.Size = new System.Drawing.Size(281, 239);
			this.splitContainer3.SplitterDistance = 75;
			this.splitContainer3.TabIndex = 4;
			// 
			// stageInfoControl1
			// 
			this.stageInfoControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stageInfoControl1.Location = new System.Drawing.Point(0, 0);
			this.stageInfoControl1.Name = "stageInfoControl1";
			this.stageInfoControl1.RelFile = null;
			this.stageInfoControl1.ShouldVerifyIDs = false;
			this.stageInfoControl1.Size = new System.Drawing.Size(281, 39);
			this.stageInfoControl1.TabIndex = 0;
			this.stageInfoControl1.UseRelDescription = false;
			// 
			// portraitViewer1
			// 
			this.portraitViewer1.Dock = System.Windows.Forms.DockStyle.Right;
			this.portraitViewer1.Location = new System.Drawing.Point(414, 24);
			this.portraitViewer1.Name = "portraitViewer1";
			this.portraitViewer1.Size = new System.Drawing.Size(178, 290);
			this.portraitViewer1.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 314);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.portraitViewer1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Brawl Stage Manager";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
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



	}
}

