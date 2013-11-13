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

			ResourceNode p1icon = node.FindChild("MenSelmapCursorPly.1", true);
			BRESNode md80 = (p1icon != null) ? p1icon.Parent.Parent as BRESNode : null;

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

			foreach (StagePair pair in definitions) {
				var spc = new StagePairControl {
					Pair = pair,
					MiscData80 = md80,
					Dock = DockStyle.Fill,
				};
				tblStageDefinitions.Controls.Add(spc);
			}

			foreach (StagePair pair in screen1) {
				var spc = new FixedStagePairControl {
					Pair = pair,
					MiscData80 = md80,
					Dock = DockStyle.Fill,
				};
				tblSSS1.Controls.Add(spc);
			}

			foreach (StagePair pair in screen2) {
				var spc = new FixedStagePairControl {
					Pair = pair,
					MiscData80 = md80,
					Dock = DockStyle.Fill,
				};
				tblSSS2.Controls.Add(spc);
			}
		}

		#region Conversion to code text
		private string ToCodeLines(List<StagePair> list, List<StagePair> definitions) {
			StringBuilder sb = new StringBuilder();
			string[] s = (from sp in list
						  select definitions.Contains(sp)
						  ? definitions.IndexOf(sp).ToString("X2")
						  : "__").ToArray();
			for (int i = 0; i < s.Length; i += 8) {
				sb.Append("* ");
				for (int j = i; j < i + 4; j++) {
					sb.Append(j < s.Length ? s[j] : "00");
				}
				sb.Append(" ");
				for (int j = i + 4; j < i + 8; j++) {
					sb.Append(j < s.Length ? s[j] : "00");
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		private string ToCodeLines(List<StagePair> definitions) {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < definitions.Count; i += 4) {
				sb.Append("* ");
				for (int j = i; j < i + 2; j++) {
					sb.Append(j < definitions.Count ? definitions[j].ToUshort().ToString("X4") : "0000");
				}
				sb.Append(" ");
				for (int j = i + 2; j < i + 4; j++) {
					sb.Append(j < definitions.Count ? definitions[j].ToUshort().ToString("X4") : "0000");
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		public string ToCode() {
			List<StagePair> definitions = new List<StagePair>();
			foreach (Control c in tblStageDefinitions.Controls) {
				if (c is StagePairControl) {
					definitions.Add(((StagePairControl)c).Pair);
				}
			}
			List<StagePair> screen1 = new List<StagePair>();
			foreach (Control c in tblSSS1.Controls) {
				if (c is StagePairControl) {
					screen1.Add(((StagePairControl)c).Pair);
				}
			}
			List<StagePair> screen2 = new List<StagePair>();
			foreach (Control c in tblSSS2.Controls) {
				if (c is StagePairControl) {
					screen2.Add(((StagePairControl)c).Pair);
				}
			}
			return String.Format(
@"* 046B8F5C 7C802378
* 046B8F64 7C6300AE
* 040AF618 5460083C
* 040AF68C 38840002
* 040AF6AC 5463083C
* 040AF6C0 88030001
* 040AF6E8 3860FFFF
* 040AF59C 3860000C
* 060B91C8 00000018
* BFA10014 7CDF3378
* 7CBE2B78 7C7D1B78
* 2D05FFFF 418A0014
* 006B929C 000000{0}
* 066B99D8 000000{0}
{1}* 006B92A4 000000{2}
* 066B9A58 000000{2}
{3}* 06407AAC 000000{4}
{5}",
	screen1.Count.ToString("X2"), ToCodeLines(screen1, definitions),
	screen2.Count.ToString("X2"), ToCodeLines(screen2, definitions),
	definitions.Count.ToString("X2"), ToCodeLines(definitions));
		}
		#endregion

		private void printoutToolStripMenuItem_Click(object sender, EventArgs e) {
			Console.WriteLine();
			Console.WriteLine(ToCode());
		}

		private void switchToFlowLayoutPanelToolStripMenuItem_Click(object sender, EventArgs e) {
			FlowLayoutPanel p = new FlowLayoutPanel() {
				Dock = DockStyle.Fill,
				AutoScroll = true,
				FlowDirection = FlowDirection.TopDown,
			};
			foreach (Control c in tblStageDefinitions.Controls) {
				c.Dock = DockStyle.None;
				if (c is StagePairControl) ((StagePairControl)c).StageNameVisible = false;
				c.Width -= 208;
				tblStageDefinitions.Controls.Remove(c);
				p.Controls.Add(c);
			}
			tabDefinitions.Controls.Remove(tblStageDefinitions);
			tabDefinitions.Controls.Add(p);
			tabDefinitions.Controls.SetChildIndex(p, 0);
		}
	}
}
