namespace BrawlStageManager {
	partial class PortraitViewer {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.prevbase = new BrawlStageManager.ImagePreviewPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.icon = new BrawlStageManager.ImagePreviewPanel();
			this.frontstname = new BrawlStageManager.ImagePreviewPanel();
			this.fileSizeBar = new System.Windows.Forms.ProgressBar();
			this.button1 = new System.Windows.Forms.Button();
			this.fileSizeLabel = new System.Windows.Forms.Label();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// prevbase
			// 
			this.prevbase.AllowDrop = true;
			this.prevbase.Location = new System.Drawing.Point(0, 0);
			this.prevbase.Margin = new System.Windows.Forms.Padding(0);
			this.prevbase.Name = "prevbase";
			this.prevbase.Size = new System.Drawing.Size(176, 176);
			this.prevbase.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 288);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 40);
			this.label1.TabIndex = 1;
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(0, 328);
			this.saveButton.Margin = new System.Windows.Forms.Padding(0);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(208, 23);
			this.saveButton.TabIndex = 2;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.prevbase);
			this.flowLayoutPanel1.Controls.Add(this.icon);
			this.flowLayoutPanel1.Controls.Add(this.frontstname);
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.saveButton);
			this.flowLayoutPanel1.Controls.Add(this.fileSizeBar);
			this.flowLayoutPanel1.Controls.Add(this.fileSizeLabel);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(210, 400);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// icon
			// 
			this.icon.AllowDrop = true;
			this.icon.Location = new System.Drawing.Point(0, 176);
			this.icon.Margin = new System.Windows.Forms.Padding(0);
			this.icon.Name = "icon";
			this.icon.Size = new System.Drawing.Size(64, 56);
			this.icon.TabIndex = 3;
			// 
			// frontstname
			// 
			this.frontstname.AllowDrop = true;
			this.frontstname.Location = new System.Drawing.Point(0, 232);
			this.frontstname.Margin = new System.Windows.Forms.Padding(0);
			this.frontstname.Name = "frontstname";
			this.frontstname.Size = new System.Drawing.Size(208, 56);
			this.frontstname.TabIndex = 4;
			// 
			// fileSizeBar
			// 
			this.fileSizeBar.Location = new System.Drawing.Point(0, 351);
			this.fileSizeBar.Margin = new System.Windows.Forms.Padding(0);
			this.fileSizeBar.Name = "fileSizeBar";
			this.fileSizeBar.Size = new System.Drawing.Size(208, 23);
			this.fileSizeBar.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Left;
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(20, 400);
			this.button1.TabIndex = 4;
			this.button1.Text = ">";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// fileSizeLabel
			// 
			this.fileSizeLabel.AutoSize = true;
			this.fileSizeLabel.Location = new System.Drawing.Point(3, 374);
			this.fileSizeLabel.Name = "fileSizeLabel";
			this.fileSizeLabel.Size = new System.Drawing.Size(66, 13);
			this.fileSizeLabel.TabIndex = 6;
			this.fileSizeLabel.Text = "fileSizeLabel";
			// 
			// PortraitViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.button1);
			this.Name = "PortraitViewer";
			this.Size = new System.Drawing.Size(230, 400);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		protected BrawlStageManager.ImagePreviewPanel prevbase;
		protected System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private BrawlStageManager.ImagePreviewPanel icon;
		private BrawlStageManager.ImagePreviewPanel frontstname;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ProgressBar fileSizeBar;
		private System.Windows.Forms.Label fileSizeLabel;
	}
}
