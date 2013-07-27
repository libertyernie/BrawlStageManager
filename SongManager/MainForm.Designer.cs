﻿namespace BrawlSongManager {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveInfopacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadNamesFromInfopacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadBRSTMPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupSongsByStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.defaultSongsListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
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
			this.splitContainer1.Size = new System.Drawing.Size(592, 290);
			this.splitContainer1.SplitterDistance = 180;
			this.splitContainer1.TabIndex = 0;
			// 
			// listBox1
			// 
			this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(180, 290);
			this.listBox1.TabIndex = 0;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.statusToolStripMenuItem});
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
            this.saveInfopacToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// changeDirectoryToolStripMenuItem
			// 
			this.changeDirectoryToolStripMenuItem.Name = "changeDirectoryToolStripMenuItem";
			this.changeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.changeDirectoryToolStripMenuItem.Text = "Change directory...";
			this.changeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.changeDirectoryToolStripMenuItem_Click);
			// 
			// saveInfopacToolStripMenuItem
			// 
			this.saveInfopacToolStripMenuItem.Name = "saveInfopacToolStripMenuItem";
			this.saveInfopacToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.saveInfopacToolStripMenuItem.Text = "Save info.pac";
			this.saveInfopacToolStripMenuItem.Click += new System.EventHandler(this.saveInfopacToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNamesFromInfopacToolStripMenuItem,
            this.loadBRSTMPlayerToolStripMenuItem,
            this.groupSongsByStageToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// loadNamesFromInfopacToolStripMenuItem
			// 
			this.loadNamesFromInfopacToolStripMenuItem.Checked = true;
			this.loadNamesFromInfopacToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.loadNamesFromInfopacToolStripMenuItem.Name = "loadNamesFromInfopacToolStripMenuItem";
			this.loadNamesFromInfopacToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.loadNamesFromInfopacToolStripMenuItem.Text = "Load names from info.pac";
			this.loadNamesFromInfopacToolStripMenuItem.Click += new System.EventHandler(this.loadNamesFromInfopacToolStripMenuItem_Click);
			// 
			// loadBRSTMPlayerToolStripMenuItem
			// 
			this.loadBRSTMPlayerToolStripMenuItem.Checked = true;
			this.loadBRSTMPlayerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.loadBRSTMPlayerToolStripMenuItem.Name = "loadBRSTMPlayerToolStripMenuItem";
			this.loadBRSTMPlayerToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.loadBRSTMPlayerToolStripMenuItem.Text = "Load BRSTM player";
			this.loadBRSTMPlayerToolStripMenuItem.Click += new System.EventHandler(this.loadBRSTMPlayerToolStripMenuItem_Click);
			// 
			// groupSongsByStageToolStripMenuItem
			// 
			this.groupSongsByStageToolStripMenuItem.Name = "groupSongsByStageToolStripMenuItem";
			this.groupSongsByStageToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.groupSongsByStageToolStripMenuItem.Text = "Group songs by stage (SSBB)";
			this.groupSongsByStageToolStripMenuItem.Click += new System.EventHandler(this.groupSongsByStageToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultSongsListToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// defaultSongsListToolStripMenuItem
			// 
			this.defaultSongsListToolStripMenuItem.Name = "defaultSongsListToolStripMenuItem";
			this.defaultSongsListToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.defaultSongsListToolStripMenuItem.Text = "Default Songs List";
			this.defaultSongsListToolStripMenuItem.Click += new System.EventHandler(this.defaultSongsListToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// statusToolStripMenuItem
			// 
			this.statusToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.statusToolStripMenuItem.Enabled = false;
			this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
			this.statusToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
			this.statusToolStripMenuItem.Text = "test";
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exportToolStripMenuItem.Text = "Export";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 314);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Brawl Song Manager";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadNamesFromInfopacToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadBRSTMPlayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveInfopacToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem groupSongsByStageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem defaultSongsListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;



	}
}

