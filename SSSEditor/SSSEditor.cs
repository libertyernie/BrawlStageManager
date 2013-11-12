using BrawlLib.SSBB.ResourceNodes;
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

			MutableSSS m = new MutableSSS(System.IO.File.ReadAllBytes("F:\\codes\\RSBE01.gct"));
			ResourceNode node = NodeFactory.FromFile(null, @"F:\private\wii\app\RSBE\pf\system\common5.pac");

			this.FormClosing += delegate(object o, FormClosingEventArgs ea) {
				MessageBox.Show(m.ToCode());
			};

			foreach (MutableSSS.StagePair pair in m.screen1) {
				flowLayoutPanel1.Controls.Add(new StagePairControl {
					Pair = pair,
					RootNode = node,
				});
			}
			flowLayoutPanel1.Controls.Add(new Label() { Text = "---------------------" });
			foreach (MutableSSS.StagePair pair in m.screen2) {
				flowLayoutPanel1.Controls.Add(new StagePairControl {
					Pair = pair,
					RootNode = node,
				});
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			int i = 0;
			foreach (Control c in flowLayoutPanel1.Controls) {
				if (c is StagePairControl) {
					var control = c as StagePairControl;
					Console.Write(control.Pair.ToUshort().ToString("X4"));
					if (++i % 8 == 0) Console.WriteLine();
				}
			}
		}
	}
}
