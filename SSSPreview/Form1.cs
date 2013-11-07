using System.Windows.Forms;

namespace SSSPreview {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
			numericUpDown1.Value = sssPreview1.NumIcons;
		}

		private void numericUpDown1_ValueChanged(object sender, System.EventArgs e) {
			sssPreview1.NumIcons = (int)numericUpDown1.Value;
		}
	}
}
