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
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();

			MutableSSS m = new MutableSSS(System.IO.File.ReadAllBytes("F:\\codes\\RSBE01.gct"));
			ResourceNode node = NodeFactory.FromFile(null, @"F:\private\wii\app\RSBE\pf\system\common5.pac");

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
	}
}
