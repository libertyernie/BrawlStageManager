namespace SSSPreview {
	partial class GCTEnabledForm {
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabBrawl = new System.Windows.Forms.TabPage();
			this.tabMelee = new System.Windows.Forms.TabPage();
			this.tabMM1 = new System.Windows.Forms.TabPage();
			this.tabMM2 = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabBrawl);
			this.tabControl1.Controls.Add(this.tabMelee);
			this.tabControl1.Controls.Add(this.tabMM1);
			this.tabControl1.Controls.Add(this.tabMM2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(384, 261);
			this.tabControl1.TabIndex = 0;
			// 
			// tabBrawl
			// 
			this.tabBrawl.Location = new System.Drawing.Point(4, 22);
			this.tabBrawl.Name = "tabBrawl";
			this.tabBrawl.Padding = new System.Windows.Forms.Padding(3);
			this.tabBrawl.Size = new System.Drawing.Size(376, 235);
			this.tabBrawl.TabIndex = 0;
			this.tabBrawl.Text = "Brawl Stages";
			this.tabBrawl.UseVisualStyleBackColor = true;
			// 
			// tabMelee
			// 
			this.tabMelee.Location = new System.Drawing.Point(4, 22);
			this.tabMelee.Name = "tabMelee";
			this.tabMelee.Padding = new System.Windows.Forms.Padding(3);
			this.tabMelee.Size = new System.Drawing.Size(276, 235);
			this.tabMelee.TabIndex = 1;
			this.tabMelee.Text = "Melee Stages";
			this.tabMelee.UseVisualStyleBackColor = true;
			// 
			// tabMM1
			// 
			this.tabMM1.Location = new System.Drawing.Point(4, 22);
			this.tabMM1.Name = "tabMM1";
			this.tabMM1.Size = new System.Drawing.Size(276, 235);
			this.tabMM1.TabIndex = 2;
			this.tabMM1.Text = "My Music #1";
			this.tabMM1.UseVisualStyleBackColor = true;
			// 
			// tabMM2
			// 
			this.tabMM2.Location = new System.Drawing.Point(4, 22);
			this.tabMM2.Name = "tabMM2";
			this.tabMM2.Size = new System.Drawing.Size(276, 235);
			this.tabMM2.TabIndex = 3;
			this.tabMM2.Text = "My Music #2";
			this.tabMM2.UseVisualStyleBackColor = true;
			// 
			// GCTEnabledForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 261);
			this.Controls.Add(this.tabControl1);
			this.Name = "GCTEnabledForm";
			this.Text = "GCTEnabledForm";
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabBrawl;
		private System.Windows.Forms.TabPage tabMelee;
		private System.Windows.Forms.TabPage tabMM1;
		private System.Windows.Forms.TabPage tabMM2;
	}
}