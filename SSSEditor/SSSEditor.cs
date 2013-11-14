using BrawlLib.SSBB.ResourceNodes;
using BrawlStageManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSSEditor {
	public partial class SSSEditor : Form {
		// Source data
		private CustomSSS sss;
		private BRESNode md80;

		public SSSEditor() {
			InitializeComponent();

			sss = new CustomSSS(System.IO.File.ReadAllBytes("F:\\codes\\RSBE01.gct"));

			ReloadIfValidPac(@"F:\private\wii\app\RSBE\pf\system\common5.pac");
		}

		private void ReloadData() {
			tblStageDefinitions.Controls.Clear();
			tblSSS1.Controls.Clear();
			tblSSS2.Controls.Clear();

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

		private void ReloadIfValidPac(string file, CustomSSS sssIfOtherFileValid = null) {
			ResourceNode node = NodeFactory.FromFile(null, file);
			ResourceNode p1icon = node.FindChild("MenSelmapCursorPly.1", true);
			BRESNode candidate = (p1icon != null) ? p1icon.Parent.Parent as BRESNode : null;
			if (candidate == null) {
				MessageBox.Show(this, "No SSS icons were found in the selected file.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				if (md80 != null) md80.Dispose();
				md80 = candidate;
				sss = sssIfOtherFileValid ?? sss;
				ReloadData();
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

		private void openCodesetgcttxtToolStripMenuItem_Click(object sender, EventArgs e) {
			using (var dialog = new OpenFileDialog()) {
				dialog.Filter = "Ocarina codes (*.gct, *.txt)|*.gct;*.txt";
				dialog.Multiselect = false;
				if (dialog.ShowDialog() == DialogResult.OK) {
					if (dialog.FileName.EndsWith("gct", StringComparison.InvariantCultureIgnoreCase)) {
						sss = new CustomSSS(File.ReadAllBytes(dialog.FileName));
					} else {
						sss = new CustomSSS(File.ReadAllLines(dialog.FileName));
					}
					ReloadData();
				}
			}
		}

		private void openStageIconspacbrresToolStripMenuItem_Click(object sender, EventArgs e) {
			using (var dialog = new OpenFileDialog()) {
				dialog.Filter = "Brawl data files (*.pac, *.brres)|*.pac;*.brres";
				dialog.Multiselect = false;
				if (dialog.ShowDialog() == DialogResult.OK) {
					ReloadIfValidPac(dialog.FileName);
				}
			}
		}

		private void openSDCardRootToolStripMenuItem_Click(object sender, EventArgs e) {
			using (var dialog = new FolderBrowserDialog()) {
				if (dialog.ShowDialog() == DialogResult.OK) {
					CustomSSS candidateSSS;

					if (File.Exists(dialog.SelectedPath + "/codes/RSBE01.gct")) {
						candidateSSS = new CustomSSS(File.ReadAllBytes(dialog.SelectedPath + "/codes/RSBE01.gct"));
					} else if (File.Exists(dialog.SelectedPath + "/data/gecko/codes/RSBE01.gct")) {
						candidateSSS = new CustomSSS(File.ReadAllBytes(dialog.SelectedPath + "/data/gecko/codes/RSBE01.gct"));
					} else if (File.Exists(dialog.SelectedPath + "/RSBE01.gct")) {
						candidateSSS = new CustomSSS(File.ReadAllBytes(dialog.SelectedPath + "/RSBE01.gct"));
					} else {
						MessageBox.Show(this, "Could not find codes/RSBE01.gct or data/gecko/codes/RSBE01.gct.",
							"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					string root;
					if (File.Exists(dialog.SelectedPath + "/private/wii/app/RSBE/pf/menu2/sc_selmap.pac")) {
						root = dialog.SelectedPath + "/private/wii/app/RSBE/pf/menu2/sc_selmap.pac";
					} else if (File.Exists(dialog.SelectedPath + "/private/wii/app/RSBE/pf/menu2/sc_selmap_en.pac")) {
						root = dialog.SelectedPath + "/private/wii/app/RSBE/pf/menu2/sc_selmap_en.pac";
					} else if (File.Exists(dialog.SelectedPath + "/private/wii/app/RSBE/pf/system/common5.pac")) {
						root = dialog.SelectedPath + "/private/wii/app/RSBE/pf/system/common5.pac";
					} else if (File.Exists(dialog.SelectedPath + "/private/wii/app/RSBE/pf/system/common5_en.pac")) {
						root = dialog.SelectedPath + "/private/wii/app/RSBE/pf/system/common5_en.pac";
					} else if (File.Exists(dialog.SelectedPath + "/MiscData[80].brres")) {
						root = dialog.SelectedPath + "/MiscData[80].brres";
					} else {
						MessageBox.Show(this, "Could not find common5 or sc_selmap.",
							"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					ReloadIfValidPac(root, candidateSSS);
				}
			}
		}

		private void saveCodesetgctToolStripMenuItem_Click(object sender, EventArgs e) {
			MessageBox.Show(this, "This feature is not yet implemented.",
							"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void saveSSSCodeOnlytxtToolStripMenuItem_Click(object sender, EventArgs e) {
			using (var dialog = new SaveFileDialog()) {
				dialog.Filter = "Text files (*.txt)|*.txt";
				dialog.OverwritePrompt = true;
				if (dialog.ShowDialog() == DialogResult.OK) {
					File.WriteAllText(dialog.FileName, ToCode());
				}
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Close();
		}

		private void viewCodeToolStripMenuItem_Click(object sender, EventArgs e) {
			using (Form f = new Form() { Text = "Custom SSS Code" }) {
				TextBox t = new TextBox() {
					Multiline = true,
					Dock = DockStyle.Fill,
					ScrollBars = ScrollBars.Vertical,
					Text = ToCode(),
					Font = new System.Drawing.Font("Consolas", 12)
				};
				f.Controls.Add(t);
				f.ShowDialog(this);
			}
		}
	}
}
