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
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudIconID)).BeginInit();
			this.SuspendLayout();
			// 
			// ddlStagePacs
			// 
			this.ddlStagePacs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlStagePacs.FormattingEnabled = true;
			this.ddlStagePacs.Location = new System.Drawing.Point(90, 3);
			this.ddlStagePacs.Name = "ddlStagePacs";
			this.ddlStagePacs.Size = new System.Drawing.Size(169, 21);
			this.ddlStagePacs.TabIndex = 1;
			this.ddlStagePacs.SelectedIndexChanged += new System.EventHandler(this.ddlStagePacs_SelectedIndexChanged);
			// 
			// lblStageID
			// 
			this.lblStageID.AutoSize = true;
			this.lblStageID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStageID.Location = new System.Drawing.Point(147, 44);
			this.lblStageID.Name = "lblStageID";
			this.lblStageID.Size = new System.Drawing.Size(24, 16);
			this.lblStageID.TabIndex = 3;
			this.lblStageID.Text = "01";
			// 
			// lblIconID
			// 
			this.lblIconID.AutoSize = true;
			this.lblIconID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIconID.Location = new System.Drawing.Point(166, 44);
			this.lblIconID.Name = "lblIconID";
			this.lblIconID.Size = new System.Drawing.Size(24, 16);
			this.lblIconID.TabIndex = 4;
			this.lblIconID.Text = "01";
			// 
			// nudIconID
			// 
			this.nudIconID.Location = new System.Drawing.Point(90, 40);
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
			// radioButton1
			// 
			this.radioButton1.Image = global::SSSEditor.Properties.Resources.stageicon;
			this.radioButton1.Location = new System.Drawing.Point(0, 0);
			this.radioButton1.Margin = new System.Windows.Forms.Padding(0);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(84, 62);
			this.radioButton1.TabIndex = 8;
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// btnDown
			// 
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDown.Image = global::SSSEditor.Properties.Resources.downarr;
			this.btnDown.Location = new System.Drawing.Point(236, 40);
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
			this.btnUp.Location = new System.Drawing.Point(210, 40);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(20, 20);
			this.btnUp.TabIndex = 6;
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
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
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "StagePairControl";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(262, 62);
			((System.ComponentModel.ISupportInitialize)(this.nudIconID)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox ddlStagePacs;
		private System.Windows.Forms.Label lblStageID;
		private System.Windows.Forms.Label lblIconID;
		private System.Windows.Forms.NumericUpDown nudIconID;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.RadioButton radioButton1;
	}
}
