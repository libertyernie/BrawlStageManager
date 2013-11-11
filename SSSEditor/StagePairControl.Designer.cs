namespace SSSEditor {
	partial class StagePairControl {
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.ddlStagePacs = new System.Windows.Forms.ComboBox();
			this.lblStageID = new System.Windows.Forms.Label();
			this.lblIconID = new System.Windows.Forms.Label();
			this.nudIconID = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIconID)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 56);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// ddlStagePacs
			// 
			this.ddlStagePacs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlStagePacs.FormattingEnabled = true;
			this.ddlStagePacs.Location = new System.Drawing.Point(70, 0);
			this.ddlStagePacs.Name = "ddlStagePacs";
			this.ddlStagePacs.Size = new System.Drawing.Size(186, 21);
			this.ddlStagePacs.TabIndex = 1;
			this.ddlStagePacs.SelectedIndexChanged += new System.EventHandler(this.ddlStagePacs_SelectedIndexChanged);
			// 
			// lblStageID
			// 
			this.lblStageID.AutoSize = true;
			this.lblStageID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStageID.Location = new System.Drawing.Point(127, 27);
			this.lblStageID.Name = "lblStageID";
			this.lblStageID.Size = new System.Drawing.Size(24, 16);
			this.lblStageID.TabIndex = 3;
			this.lblStageID.Text = "01";
			// 
			// lblIconID
			// 
			this.lblIconID.AutoSize = true;
			this.lblIconID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIconID.Location = new System.Drawing.Point(144, 27);
			this.lblIconID.Name = "lblIconID";
			this.lblIconID.Size = new System.Drawing.Size(24, 16);
			this.lblIconID.TabIndex = 4;
			this.lblIconID.Text = "01";
			// 
			// nudIconID
			// 
			this.nudIconID.Location = new System.Drawing.Point(70, 27);
			this.nudIconID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudIconID.Name = "nudIconID";
			this.nudIconID.Size = new System.Drawing.Size(51, 20);
			this.nudIconID.TabIndex = 5;
			this.nudIconID.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudIconID.ValueChanged += new System.EventHandler(this.nudIconID_ValueChanged);
			// 
			// StagePairControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nudIconID);
			this.Controls.Add(this.lblIconID);
			this.Controls.Add(this.lblStageID);
			this.Controls.Add(this.ddlStagePacs);
			this.Controls.Add(this.pictureBox1);
			this.Name = "StagePairControl";
			this.Size = new System.Drawing.Size(256, 56);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIconID)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ComboBox ddlStagePacs;
		private System.Windows.Forms.Label lblStageID;
		private System.Windows.Forms.Label lblIconID;
		private System.Windows.Forms.NumericUpDown nudIconID;
	}
}
