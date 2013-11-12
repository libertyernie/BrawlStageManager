using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSEditor {
	public class FixedStagePairControl : StagePairControl {
		public FixedStagePairControl() : base() {
			ddlStagePacs.Enabled = false;
			nudIconID.Enabled = false;
			colorCode.BackColor = Color.Gray;
		}

		public override void UpdateColor(int? index = null) {
			return;
		}
	}
}
