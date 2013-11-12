namespace SSSEditor {
	partial class SSSEditor {
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
			this.button1 = new System.Windows.Forms.Button();
			this.tblStageDefinitions = new System.Windows.Forms.TableLayoutPanel();
			this.tblColorCodeKeys = new System.Windows.Forms.TableLayoutPanel();
			this.lblGreen = new System.Windows.Forms.Label();
			this.lblYellow = new System.Windows.Forms.Label();
			this.lblBlue = new System.Windows.Forms.Label();
			this.tblColorCodeKeys.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.button1.Location = new System.Drawing.Point(0, 439);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(304, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tblStageDefinitions
			// 
			this.tblStageDefinitions.AutoScroll = true;
			this.tblStageDefinitions.ColumnCount = 1;
			this.tblStageDefinitions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblStageDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblStageDefinitions.Location = new System.Drawing.Point(0, 64);
			this.tblStageDefinitions.Name = "tblStageDefinitions";
			this.tblStageDefinitions.RowCount = 1;
			this.tblStageDefinitions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblStageDefinitions.Size = new System.Drawing.Size(304, 375);
			this.tblStageDefinitions.TabIndex = 1;
			// 
			// tblColorCodeKeys
			// 
			this.tblColorCodeKeys.ColumnCount = 1;
			this.tblColorCodeKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblColorCodeKeys.Controls.Add(this.lblGreen, 0, 2);
			this.tblColorCodeKeys.Controls.Add(this.lblYellow, 0, 1);
			this.tblColorCodeKeys.Controls.Add(this.lblBlue, 0, 0);
			this.tblColorCodeKeys.Dock = System.Windows.Forms.DockStyle.Top;
			this.tblColorCodeKeys.Location = new System.Drawing.Point(0, 0);
			this.tblColorCodeKeys.Name = "tblColorCodeKeys";
			this.tblColorCodeKeys.RowCount = 3;
			this.tblColorCodeKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tblColorCodeKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tblColorCodeKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tblColorCodeKeys.Size = new System.Drawing.Size(304, 64);
			this.tblColorCodeKeys.TabIndex = 2;
			// 
			// lblGreen
			// 
			this.lblGreen.BackColor = System.Drawing.Color.Green;
			this.lblGreen.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblGreen.ForeColor = System.Drawing.Color.White;
			this.lblGreen.Location = new System.Drawing.Point(3, 42);
			this.lblGreen.Name = "lblGreen";
			this.lblGreen.Size = new System.Drawing.Size(298, 22);
			this.lblGreen.TabIndex = 2;
			this.lblGreen.Text = "Green: both on My Music AND chosen on random";
			this.lblGreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblYellow
			// 
			this.lblYellow.BackColor = System.Drawing.Color.Yellow;
			this.lblYellow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblYellow.ForeColor = System.Drawing.Color.Black;
			this.lblYellow.Location = new System.Drawing.Point(3, 21);
			this.lblYellow.Name = "lblYellow";
			this.lblYellow.Size = new System.Drawing.Size(298, 21);
			this.lblYellow.TabIndex = 1;
			this.lblYellow.Text = "Yellow: missing from My Music (Hanenbow)";
			this.lblYellow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblBlue
			// 
			this.lblBlue.BackColor = System.Drawing.Color.Blue;
			this.lblBlue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblBlue.ForeColor = System.Drawing.Color.White;
			this.lblBlue.Location = new System.Drawing.Point(3, 0);
			this.lblBlue.Name = "lblBlue";
			this.lblBlue.Size = new System.Drawing.Size(298, 21);
			this.lblBlue.TabIndex = 0;
			this.lblBlue.Text = "Blue: never gets chosen on random";
			this.lblBlue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SSSEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 462);
			this.Controls.Add(this.tblStageDefinitions);
			this.Controls.Add(this.tblColorCodeKeys);
			this.Controls.Add(this.button1);
			this.Name = "SSSEditor";
			this.Text = "SSS Editor";
			this.tblColorCodeKeys.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TableLayoutPanel tblStageDefinitions;
        private System.Windows.Forms.TableLayoutPanel tblColorCodeKeys;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.Label lblYellow;
	}
}

