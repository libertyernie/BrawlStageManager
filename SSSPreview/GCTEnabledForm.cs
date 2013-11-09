using BrawlLib.SSBB.ResourceNodes;
using BrawlStageManager;
using System.IO;
using System.Windows.Forms;

namespace SSSPreview {
	public partial class GCTEnabledForm : Form {
		private SSSPrev[] previews;
		private string common5, gctbinary;

		public GCTEnabledForm(string common5, string gctbinary) {
			InitializeComponent();

			this.common5 = common5;
			this.gctbinary = gctbinary;
			this.Load += GCTEnabledForm_Load;
		}

		private void GCTEnabledForm_Load(object sender, System.EventArgs e) {
			common5 = common5 ?? askFile("Brawl data files (*.pac, *.brres)|*.pac;*.brres");
			gctbinary = gctbinary ?? askFile("GCT codesets (*.gct)|*.gct");

			ResourceNode rootNode = NodeFactory.FromFile(null, common5);
			CustomSSS sss = new CustomSSS(File.ReadAllBytes(gctbinary));

			previews = new SSSPrev[4];
			for (int i = 0; i < 4; i++) {
				previews[i] = new SSSPrev() {
					Dock = DockStyle.Fill,
					RootNode = rootNode,
				};
				tabControl1.TabPages[i].Controls.Add(previews[i]);
			}

			var icons = sss.IconsInGroups;
			previews[0].IconOrder = icons.Item1;
			previews[0].NumIcons = icons.Item1.Length;
			previews[1].IconOrder = icons.Item2;
			previews[1].NumIcons = icons.Item2.Length;
		}

		private string askFile(string filter) {
			using (OpenFileDialog ofd = new OpenFileDialog()) {
				ofd.Multiselect = false;
				ofd.Filter = filter;
				if (ofd.ShowDialog(this) == DialogResult.OK) {
					return ofd.FileName;
				}
			}
			return null;
		}
	}
}
