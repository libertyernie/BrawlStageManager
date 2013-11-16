using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrawlStageManager;
using BrawlLib.SSBB.ResourceNodes;

namespace SSSEditor {
	public partial class StagePairControl : UserControl {
		private BRESNode miscdata80;
		/// <summary>
		/// The MiscData[80] file to read icons from.
		/// </summary>
		public BRESNode MiscData80 {
			set {
				if (miscdata80 != null) {
					miscdata80.Dispose();
				}
				miscdata80 = value;
				Icon = Icon;
			}
		}

        public bool SetNUDToOwnIndex;

		/// <summary>
		/// Checks the radio button, and if 'true', focuses it as well.
		/// </summary>
		public bool Checked {
			get {
				return radioButton1.Checked;
			}
			set {
				radioButton1.Checked = value;
				if (value) radioButton1.Focus();
			}
		}

		/// <summary>
		/// Gets or sets the value of the leftmost number spinner, which may toggle update events.
		/// </summary>
        public decimal NUDDefValue {
            get {
                return nudDefIndex.Value;
            }
            set {
                nudDefIndex.Value = value;
            }
        }

		private StagePair _pair;
		private TextureContainer textures;
		/// <summary>
		/// Gets/sets the pair object being pointed to by this control.
		/// </summary>
		public StagePair Pair {
			get {
				return _pair;
			}
			set {
				_pair = value;
				Stage = Stage;
				Icon = Icon;
			}
		}
		/// <summary>
		/// Gets/sets the stage ID of the pair and updates the dropdown list.
		/// </summary>
		public byte Stage {
			get {
				return Pair == null ? (byte)0xff : Pair.stage;
			}
			set {
				if (Pair != null) {
					Pair.stage = value;
					ddlStagePacs.SelectedValue = value;
					lblStageID.Text = value.ToString("X2");
				}
			}
		}
		/// <summary>
		/// Gets/sets the icon ID of the pair and updates the icon ID and images.
		/// </summary>
		public byte Icon {
			get {
				return Pair == null ? (byte)0xff : Pair.icon;
			}
			set {
				if (Pair != null) {
					Pair.icon = value;
					if (miscdata80 == null) {
						radioButton1.Image = null;
                        pictureBox1.Image = null;
					} else {
                        textures = new TextureContainer(miscdata80, Icon);
                        radioButton1.Image = (textures.icon_tex0 == null) ? null : textures.icon_tex0.GetImage(0);
                        pictureBox1.Image = (textures.frontstname_tex0 == null) ? null : textures.frontstname_tex0.GetImage(0);
					}
					nudIconID.Value = value;
					lblIconID.Text = value.ToString("X2");
				}
			}
		}

		public StagePairControl() {
			InitializeComponent();
            SetNUDToOwnIndex = true;

            radioButton1.KeyDown += keyHandler;
            foreach (Control c in new Control[] { panel1, colorCode, radioButton1, pictureBox1, lblIconID, lblStageID }) {
                c.Click += CheckRadioButton;
                c.MouseUp += ShowMenuOnRightClick;
            }

            ddlStagePacs.DisplayMember = "Value";
            ddlStagePacs.ValueMember = "Key";
            ddlStagePacs.DataSource = Static.StagesByID;
            ddlStagePacs.Resize += (o, e) =>
            {
                if (!ddlStagePacs.Focused) ddlStagePacs.SelectionLength = 0;
            };

            this.ParentChanged += (o, e) => {
                Recolor();
            };

            nudDefIndex.ValueChanged += (o, e) => {
                colorCode.BackColor =
                  nudDefIndex.Value == 0x1E ? Color.Yellow
                : nudDefIndex.Value < 0x29 ? Color.Green
                : Color.Red;
            };

            nudIconID.ValueChanged += (o, e) => {
                Icon = (byte)nudIconID.Value;
            };
		}

		private void Recolor() {
			int i = Parent.Controls.GetChildIndex(this);
            lblIndex.Text = i.ToString("X2");
            if (SetNUDToOwnIndex) nudDefIndex.Value = i;
            Invalidate();
		}

        private void CheckRadioButton(object sender, EventArgs e) {
            radioButton1.Focus();
			radioButton1.Checked = true;
        }

		void ShowMenuOnRightClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) contextMenuStrip1.Show(Cursor.Position);
		}

		private void ddlStagePacs_SelectedIndexChanged(object sender, EventArgs e) {
			if (ddlStagePacs.SelectedValue != null) Stage = (byte)ddlStagePacs.SelectedValue;
		}

		private void btnUp_Click(object sender, EventArgs e) {
			var C = Parent.Controls;
			int index = C.IndexOf(this);
			if (index == 0) return;

			Control control = C[index - 1];
            C.SetChildIndex(this, index - 1);
            C.SetChildIndex(control, index);

            Recolor();
            if (control is StagePairControl) ((StagePairControl)control).Recolor();
			/*controlAbove.Invalidate();
			this.Invalidate();*/
		}

		private void btnDown_Click(object sender, EventArgs e) {
			var C = Parent.Controls;
			int index = C.IndexOf(this);
			if (index == C.Count - 1) return;

			Control control = C[index + 1];
			C.SetChildIndex(this, index + 1);
            C.SetChildIndex(control, index);

            Recolor();
            if (control is StagePairControl) ((StagePairControl)control).Recolor();
			/*controlBelow.Invalidate();
			this.Invalidate();*/
		}

		void keyHandler(object sender, KeyEventArgs e) {
			if (!radioButton1.Checked) return;
			if (e.KeyCode == Keys.PageUp) {
				e.Handled = true;
				btnUp.PerformClick();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                e.Handled = true;
                btnDown.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                deleteToolStripMenuItem.PerformClick();
            }
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e) {
			BackColor = radioButton1.Checked ? SystemColors.Highlight : SystemColors.Control;
			if (radioButton1.Checked) {
				foreach (Control c in Parent.Controls) {
					if (c is StagePairControl && c != this) {
						((StagePairControl)c).Checked = false;
					}
				}
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
			var C = Parent.Controls;
			int index = C.IndexOf(this);
			Control control = (index == C.Count - 1)
				? C[index - 1]
				: C[index + 1];
			if (control is StagePairControl) {
				((StagePairControl)control).Checked = true;
			}

			Parent.Controls.Remove(this);
			this.Dispose();
		}
	}
}
