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
			this.ddlStagePacs = new System.Windows.Forms.ComboBox();
			this.lblStageID = new System.Windows.Forms.Label();
			this.lblIconID = new System.Windows.Forms.Label();
			this.nudIconID = new System.Windows.Forms.NumericUpDown();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.nudIconID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
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
			this.lblIconID.Location = new System.Drawing.Point(146, 27);
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
			// btnDown
			// 
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDown.Image = global::SSSEditor.Properties.Resources.downarr;
			this.btnDown.Location = new System.Drawing.Point(236, 27);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(20, 20);
			this.btnDown.TabIndex = 7;
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnUp
			// 
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUp.Image = global::SSSEditor.Properties.Resources.uparr;
			this.btnUp.Location = new System.Drawing.Point(210, 27);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(20, 20);
			this.btnUp.TabIndex = 6;
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 56);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(190, 27);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(14, 13);
			this.radioButton1.TabIndex = 8;
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// StagePairControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.nudIconID);
			this.Controls.Add(this.lblIconID);
			this.Controls.Add(this.lblStageID);
			this.Controls.Add(this.ddlStagePacs);
			this.Controls.Add(this.pictureBox1);
			this.Name = "StagePairControl";
			this.Size = new System.Drawing.Size(256, 56);
			((System.ComponentModel.ISupportInitialize)(this.nudIconID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ComboBox ddlStagePacs;
		private System.Windows.Forms.Label lblStageID;
		private System.Windows.Forms.Label lblIconID;
		private System.Windows.Forms.NumericUpDown nudIconID;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.RadioButton radioButton1;
	}
}
