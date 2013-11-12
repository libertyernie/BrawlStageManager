using BrawlLib.SSBB.ResourceNodes;
using BrawlStageManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSSEditor {
	public partial class SSSEditor : Form {
		public SSSEditor() {
			InitializeComponent();

			CustomSSS sss = new CustomSSS(System.IO.File.ReadAllBytes("F:\\codes\\RSBE01.gct"));
			ResourceNode node = NodeFactory.FromFile(null, @"F:\private\wii\app\RSBE\pf\system\common5.pac");

			var screen1 = new List<StagePair>();
			var screen2 = new List<StagePair>();
			var definitions = new List<StagePair>();
			for (int i = 0; i < sss.sss3.Length; i += 2) {
				definitions.Add(new StagePair {
					stage = sss.sss3[i],
					icon = sss.sss3[i + 1],
				});
			}
			foreach (byte b in sss.sss1) {
				screen1.Add(definitions[b]);
			}
			foreach (byte b in sss.sss2) {
				screen2.Add(definitions[b]);
			}

			foreach (StagePair pair in screen1) {
				tableLayoutPanel1.Controls.Add(new StagePairControl {
					Pair = pair,
					RootNode = node,
				});
			}
			tableLayoutPanel1.Controls.Add(new Label() { Text = "---------------------" });
			foreach (StagePair pair in screen2) {
				tableLayoutPanel1.Controls.Add(new StagePairControl {
					Pair = pair,
					RootNode = node,
				});
			}
		}

		private void button1_Click(object sender, EventArgs e) {

		}
	}
}
