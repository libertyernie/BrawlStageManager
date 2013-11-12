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
		public ResourceNode RootNode {
			set {
				if (miscdata80 != null) {
					miscdata80.Dispose();
					miscdata80 = null;
				}
				ResourceNode p1icon = value.FindChild("MenSelmapCursorPly.1", true);
				if (p1icon != null) miscdata80 = p1icon.Parent.Parent as BRESNode;
				Icon = Icon;
			}
		}

		private StagePair _pair;
		private TextureContainer textures;
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
		public byte Stage {
			get {
				return Pair.stage;
			}
			set {
				if (Pair != null) {
					Pair.stage = value;
					ddlStagePacs.SelectedValue = value;
					lblStageID.Text = value.ToString("X2");
				}
			}
		}
		public byte Icon {
			get {
				return Pair.icon;
			}
			set {
				if (Pair != null) {
					Pair.icon = value;
					if (miscdata80 == null) {
						pictureBox1.Image = null;
					} else {
						textures = new TextureContainer(miscdata80, Icon);
						pictureBox1.Image = (textures.icon_tex0 == null) ? null : textures.icon_tex0.GetImage(0);
					}
					nudIconID.Value = value;
					lblIconID.Text = value.ToString("X2");
				}
			}
		}

		public StagePairControl() {
			InitializeComponent();

			ddlStagePacs.DisplayMember = "Value";
			ddlStagePacs.ValueMember = "Key";
			ddlStagePacs.DataSource = Static.StagesByID;
		}

		private void ddlStagePacs_SelectedIndexChanged(object sender, EventArgs e) {
			if (ddlStagePacs.SelectedValue != null) Stage = (byte)ddlStagePacs.SelectedValue;
		}

		private void nudIconID_ValueChanged(object sender, EventArgs e) {
			Icon = (byte)nudIconID.Value;
		}

		private void btnUp_Click(object sender, EventArgs e) {
			var C = Parent.Controls;
			int index = C.IndexOf(this);
			if (index == 0) return;
			Control controlAbove = C[index - 1];
			C.SetChildIndex(this, index - 1);
			C.SetChildIndex(controlAbove, index);
		}

		private void btnDown_Click(object sender, EventArgs e) {
			var C = Parent.Controls;
			int index = C.IndexOf(this);
			if (index == C.Count - 1) return;
			Control controlBelow = C[index + 1];
			C.SetChildIndex(this, index + 1);
			C.SetChildIndex(controlBelow, index);
		}
	}
}
