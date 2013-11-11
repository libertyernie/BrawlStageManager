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

		public byte _stage, _icon;
		public byte Stage {
			get {
				return _stage;
			}
			set {
				_stage = value;
				ddlStagePacs.SelectedValue = value;
				lblStageID.Text = value.ToString("X2");
			}
		}
		public byte Icon {
			get {
				return _icon;
			}
			set {
				_icon = value;
				pictureBox1.Image = (miscdata80 == null) ? null : new TextureContainer(miscdata80, _icon).icon_tex0.GetImage(0);
				nudIconID.Value = value;
				lblIconID.Text = value.ToString("X2");
			}
		}

		public StagePairControl() {
			InitializeComponent();

			ddlStagePacs.DisplayMember = "PacBasename";
			ddlStagePacs.ValueMember = "ID";
			ddlStagePacs.DataSource = StageIDMap.Stages;
		}
	}
}
