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

		private string _openFilePath;
		public Size? prevbaseResizeTo;

		/// <summary>
		/// The common5 currently being used. If using sc_selcharacter.pac instead, this will be null.
		/// </summary>
		private ResourceNode common5;
		/// <summary>
		/// Either the sc_selmap_en archive within common5.pac or the sc_selmap.pac file.
		/// </summary>
		private ResourceNode sc_selmap;

		private struct Textures : IEnumerable<TEX0Node> {
			public TEX0Node prevbase_tex0, icon_tex0, frontstname_tex0;

			public IEnumerator<TEX0Node> GetEnumerator() {
				return new List<TEX0Node> { prevbase_tex0, icon_tex0, frontstname_tex0 }.GetEnumerator();
			}
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
				return GetEnumerator();
			}
		}

		private Textures textures;

		// In case the image needs to be reloaded after replacing the texture
		protected int _iconNum;

		public PortraitViewer() {
			InitializeComponent();

			_iconNum = -1;

			foreach (Panel p in new Panel[] {prevbase, icon, frontstname}) {
				p.DragEnter += panel1_DragEnter;
				p.DragDrop += panel1_DragDrop;
			}

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
				setBG(prevbase);
				setBG(icon);
				setBG(frontstname);

				if (textures.prevbase_tex0 != null && textures.frontstname_tex0 != null) {
					label1.Text = "prevbase: " + textures.prevbase_tex0.Width + "x" + textures.prevbase_tex0.Height
						+ "\nfrontstname: " + textures.frontstname_tex0.Width + "x" + textures.frontstname_tex0.Height;
				}

				_iconNum = iconNum;
			}
		}

		private void setBG(Panel panel) {
			panel.BackgroundImage = new Bitmap(
				(GetTEX0For(panel) ?? nothingnode).GetImage(0),
				new Size(panel.Width, panel.Height));
		}

		private Textures? get_icons(int iconNum) {
			if (common5 != null) {
				saveButton.Text = "Save common5";
			} else if (sc_selmap != null) {
				saveButton.Text = "Save sc_selmap";
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
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
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
					if (sender == prevbase && prevbaseResizeTo != null) {
						string tempFile = Path.GetTempFileName();
						using (Bitmap orig = new Bitmap(filename)) {
							using (Bitmap resized = new Bitmap(orig, prevbaseResizeTo ?? Size.Empty)) {
								resized.Save(tempFile);
							}
						}
						dlg.ImageSource = tempFile;
					} else {
						dlg.ImageSource = filename;
					}
					if (dlg.ShowDialog(null, GetTEX0For(sender)) == DialogResult.OK) {
						UpdateImage(_iconNum);
						GetTEX0For(sender).IsDirty = true;
					}
				}
			}
		}

		public TEX0Node GetTEX0For(object sender) {
			return
				(sender == prevbase) ? textures.prevbase_tex0 :
				(sender == icon) ? textures.icon_tex0 :
				(sender == frontstname) ? textures.frontstname_tex0 :
				(sender is Control) ? GetTEX0For((sender as Control).Parent) :
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
	}
}
