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

		private MutableSSS.StagePair _pair;
		public MutableSSS.StagePair Pair {
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
					pictureBox1.Image = (miscdata80 == null) ? null : new TextureContainer(miscdata80, Icon).icon_tex0.GetImage(0);
					nudIconID.Value = value;
					lblIconID.Text = value.ToString("X2");
				}
			}
		}

		public StagePairControl() {
			InitializeComponent();

			ddlStagePacs.DisplayMember = "PacBasename";
			ddlStagePacs.ValueMember = "ID";
			ddlStagePacs.DataSource = StageIDMap.Stages;
		}

		private void ddlStagePacs_SelectedIndexChanged(object sender, EventArgs e) {
			if (ddlStagePacs.SelectedValue != null) Stage = (byte)ddlStagePacs.SelectedValue;
		}

		private void nudIconID_ValueChanged(object sender, EventArgs e) {
			Icon = (byte)nudIconID.Value;
		}
	}
}
