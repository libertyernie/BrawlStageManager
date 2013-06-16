using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrawlLib.SSBB.ResourceNodes;
using System.IO;
using BrawlLib;

namespace BrawlStageManager {
	public partial class PortraitViewer : UserControl {

		private static OpenFileDialog _openDlg;
		private static SaveFileDialog _saveDlg;
		static PortraitViewer() {
			_openDlg = new OpenFileDialog();
			_saveDlg = new SaveFileDialog();
		}

		private string _openFilePath;

		/// <summary>
		/// The common5 currently being used. If using sc_selcharacter.pac instead, this will be null.
		/// </summary>
		private ResourceNode common5;
		/// <summary>
		/// Either the sc_selmap_en archive within common5.pac or the sc_selmap.pac file.
		/// </summary>
		private ResourceNode sc_selmap;

		/// <summary>
		/// Method lifted directly from BrawlBox.
		/// </summary>
		public static string ApplyExtension(string path, string filter, int filterIndex) {
			int tmp;
			if ((Path.HasExtension(path)) && (!int.TryParse(Path.GetExtension(path), out tmp)))
				return path;

			int index = filter.IndexOfOccurance('|', filterIndex * 2);
			if (index == -1)
				return path;

			index = filter.IndexOf('.', index);
			int len = Math.Max(filter.Length, filter.IndexOfAny(new char[] { ';', '|' })) - index;

			string ext = filter.Substring(index, len);

			if (ext.IndexOf('*') >= 0)
				return path;

			return path + ext;
		}

		private struct Textures {
			public TEX0Node prevbase_tex0, icon_tex0, frontstname_tex0;
		}

		private Textures textures;

		// In case the image needs to be reloaded after replacing the texture
		protected int _iconNum;

		public PortraitViewer() {
			InitializeComponent();

			_iconNum = -1;

			prevbase.DragEnter += panel1_DragEnter;
			prevbase.DragDrop += panel1_DragDrop;

			UpdateDirectory();
		}

		private static TEX0Node nothingnode = new TEX0Node();

		public void UpdateImage(int iconNum) {
			prevbase.BackgroundImage = null;
			_iconNum = -1;

			Textures? retval = get_icons(iconNum);
			if (retval == null) {

			} else {
				textures = retval ?? new Textures();
				prevbase.BackgroundImage = (textures.prevbase_tex0 ?? nothingnode).GetImage(0);
				icon.BackgroundImage = (textures.icon_tex0 ?? nothingnode).GetImage(0);
				frontstname.BackgroundImage = (textures.frontstname_tex0 ?? nothingnode).GetImage(0);

				_iconNum = iconNum;
			}
		}

		private Textures? get_icons(int iconNum) {
			if (common5 != null) {
				label1.Text = "common5: ";
			} else if (sc_selmap != null) {
				label1.Text = "sc_selmap.pac: ";
			} else {
				return null;
			}

			ResourceNode texturesFolder = sc_selmap.FindChild("MiscData[80]/Textures(NW4R)", false);
			if (texturesFolder == null) return null;

			Textures result = new Textures {
				prevbase_tex0 = (TEX0Node)texturesFolder.FindChild("MenSelmapPrevbase." + iconNum.ToString("D2"), false),
				icon_tex0 = (TEX0Node)texturesFolder.FindChild("MenSelmapIcon." + iconNum.ToString("D2"), false),
				frontstname_tex0 = (TEX0Node)texturesFolder.FindChild("MenSelmapFrontStname." + iconNum.ToString("D2"), false),
			};
			return result;
		}

		public void UpdateDirectory() {
			Console.WriteLine(System.Environment.CurrentDirectory);
			try {
				string path = "../../menu2/sc_selmap.pac";
				common5 = null;
				sc_selmap = NodeFactory.FromFile(null, path);
				_openFilePath = path;
			} catch (IOException) {
				try {
					string path = "G:/private/wii/app/rsbe/pf/system/common5.pac";
					common5 = NodeFactory.FromFile(null, path);
					sc_selmap = common5.FindChild("sc_selmap_en", false);
					_openFilePath = path;
				} catch (IOException) {
					try {
						string path = "../../system/common5_en.pac";
						common5 = NodeFactory.FromFile(null, path);
						sc_selmap = common5.FindChild("sc_selmap_en", false);
						_openFilePath = path;
					} catch (IOException) {
						common5 = null;
						sc_selmap = null;
						label1.Text = "Could not load sc_selmap or common5(_en).";
					}
				}
			}
		}

		void panel1_DragEnter(object sender, DragEventArgs e) {
			if (/*tex0 != null && */e.Data.GetDataPresent(DataFormats.FileDrop)) {
				string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (s.Length == 1) { // Can only drag and drop one file
					string filename = s[0].ToLower();
					if (filename.EndsWith(".png") || filename.EndsWith(".gif")
						|| filename.EndsWith(".tex0") || filename.EndsWith(".brres")) {
						e.Effect = DragDropEffects.Copy;
					}
				}
			}
		}

		void panel1_DragDrop(object sender, DragEventArgs e) {
			if (e.Effect == DragDropEffects.Copy) {
				Replace(sender, (e.Data.GetData(DataFormats.FileDrop) as string[])[0]);
			}
		}

		public void Replace(object sender, string filename) {
			var ig = StringComparison.CurrentCultureIgnoreCase;
			if (filename.EndsWith(".tex0", ig) || filename.EndsWith(".brres", ig)) {
				using (ResourceNode node = NodeFactory.FromFile(null, filename)) {
					TEX0Node tex0;
					if (node is TEX0Node) {
						tex0 = (TEX0Node)node;
					} else {
						tex0 = (TEX0Node)node.FindChild("Textures(NW4R)", false).Children[0];
					}
					string tempFile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
					tex0.Export(tempFile);
					Replace(sender, tempFile); // call self with new file
					File.Delete(tempFile);
				}
			} else {
				using (TextureConverterDialog dlg = new TextureConverterDialog()) {
					dlg.ImageSource = filename;
					if (dlg.ShowDialog(null, GetTEX0For(sender)) == DialogResult.OK) {
						UpdateImage(_iconNum);
					}
				}
			}
		}

		private TEX0Node GetTEX0For(object sender) {
			return
				(sender == prevbase) ? textures.prevbase_tex0 :
				(sender == icon) ? textures.icon_tex0 :
				(sender == frontstname) ? textures.frontstname_tex0 :
				null;
		}

		protected void saveButton_Click(object sender, EventArgs e) {
			if (sc_selmap == null) {
				return;
			}

			if (common5 != null) {
				common5.Merge();
				common5.Export(_openFilePath);
			} else {
				sc_selmap.Merge();
				sc_selmap.Export(_openFilePath);
			}
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e) {
			_openDlg.Filter = ExportFilters.TEX0;
			if (_openDlg.ShowDialog() == DialogResult.OK) {
				string fileName = _openDlg.FileName;
				Replace(sender, fileName);
			}
		}

		/// <summary>
		/// From BrawlBox (mostly - some simplification)
		/// </summary>
		private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
			_saveDlg.Filter = ExportFilters.TEX0;
			_saveDlg.FilterIndex = 1;
			if (_saveDlg.ShowDialog() == DialogResult.OK) {
				int fIndex = _saveDlg.FilterIndex;

				//Fix extension
				string fileName = ApplyExtension(_saveDlg.FileName, _saveDlg.Filter, fIndex - 1);
				GetTEX0For(sender).Export(fileName);
			}
		}
	}
}
