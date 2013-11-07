using System.Windows.Forms;

namespace SSSPreview {
	public partial class Form1 : Form {
		OpenFileDialog ofd;

		public string RootFile {
			set {
				sssPreview1.RootNode = BrawlLib.SSBB.ResourceNodes.NodeFactory.FromFile(null, value);
			}
		}

		public Form1() {
			InitializeComponent();
			numericUpDown1.Value = sssPreview1.NumIcons;
		}

		private void numericUpDown1_ValueChanged(object sender, System.EventArgs e) {
			sssPreview1.NumIcons = (int)numericUpDown1.Value;
		}

		private void openToolStripMenuItem_Click(object sender, System.EventArgs e) {
			if (ofd == null) ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "Brawl data files (*.pac, *.brres)|*.pac;*.brres*";
			if (ofd.ShowDialog(this) == DialogResult.OK) {
				RootFile = ofd.FileName;
			}
		}

		private void exitToolStripMenuItem_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void rdo__1_CheckedChanged(object sender, System.EventArgs e) {
			sssPreview1.MyMusic = rdo__1.Checked;
		}
	}
}
