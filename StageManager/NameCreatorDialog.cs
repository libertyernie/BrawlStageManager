using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrawlStageManager {
	public partial class NameCreatorDialog : Form {
		public Font SelectedFont;
		public int VerticalOffset { get; private set; }

		public NameCreatorDialog() {
			InitializeComponent();
		}

		private void btnImpact_Click(object sender, EventArgs e) {
			SelectedFont = new Font("Impact", 22.5f);
			VerticalOffset = -1;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnEdo_Click(object sender, EventArgs e) {
			SelectedFont = new Font("Edo SZ", 22f, FontStyle.Bold);
			VerticalOffset = 2;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCustom_Click(object sender, EventArgs e) {
			using (FontDialog d = new FontDialog()) {
				d.Font = SelectedFont;
				DialogResult = d.ShowDialog();
				SelectedFont = d.Font;
				VerticalOffset = (int)nudOffset.Value;
			}
			Close();
		}
	}
}
