using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSSEditor {
	public class FixedStagePairControl : StagePairControl {
		private Control definitionsContainer;

		public FixedStagePairControl(Control definitionsContainer) : base() {
			this.definitionsContainer = definitionsContainer;
			ddlStagePacs.Enabled = false;
			nudIconID.Visible = false;
			colorCode.Visible = false;
			nudDefIndex.Visible = true;

			nudDefIndex.ValueChanged += nudDefIndex_ValueChanged;
			this.PairChanged += delegate(object o, EventArgs e) {
				for (int i = 0; i < definitionsContainer.Controls.Count; i++) {
					Control c = definitionsContainer.Controls[i];
					if (c is StagePairControl && ((StagePairControl)c).Pair == this.Pair) {
						nudDefIndex.Value = i;
						break;
					}
				}
			};
		}

		void nudDefIndex_ValueChanged(object sender, EventArgs e) {
			if (nudDefIndex.Value >= definitionsContainer.Controls.Count) {
				nudDefIndex.Value = definitionsContainer.Controls.Count - 1;
			}
			Control c = definitionsContainer.Controls[(int)nudDefIndex.Value];
			if (c is StagePairControl) {
				this.Pair = ((StagePairControl)c).Pair;
			}
		}
	}
}
