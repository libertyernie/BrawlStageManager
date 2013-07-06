﻿using System;
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
using BrawlLib.Wii.Textures;

namespace BrawlStageManager {
	public partial class PortraitViewer : UserControl {

		public Size? prevbaseResizeTo;
		public Size? frontstnameResizeTo;
		public Size? selmapMarkResizeTo;
		public bool selmapMarkPreview;
		public bool IsDirty {
			get {
				return common5 != null ? common5.IsDirty
					: sc_selmap != null ? sc_selmap.IsDirty
					: false;
			}
		}

		/// <summary>
		/// The common5 currently being used. If using sc_selcharacter.pac instead, this will be null.
		/// </summary>
		private ResourceNode common5;
		/// <summary>
		/// Either the sc_selmap_en archive within common5.pac or the sc_selmap.pac file.
		/// </summary>
		private ResourceNode sc_selmap;

		public TEX0Node GetTEX0For(object sender) {
			return
				(sender == prevbase) ? textures.prevbase_tex0 :
				(sender == icon) ? textures.icon_tex0 :
				(sender == frontstname) ? textures.frontstname_tex0 :
				(sender == seriesicon) ? textures.seriesicon_tex0 :
				(sender == selmap_mark) ? textures.selmap_mark_tex0 :
				null;
		}

		private TextureContainer textures;

		private string _openFilePath;
		// In case the image needs to be reloaded after replacing the texture
		private int _iconNum;

		public PortraitViewer() {
			InitializeComponent();

			_iconNum = -1;
			fileSizeBar.Style = ProgressBarStyle.Continuous;

			foreach (Control child in flowLayoutPanel1.Controls) {
				if (child is ImagePreviewPanel) {
					(child as ImagePreviewPanel).DragEnter += panel1_DragEnter;
					(child as ImagePreviewPanel).DragDrop += panel1_DragDrop;
				}
			}

			UpdateDirectory();
		}

		public void UpdateImage(int iconNum) {
			prevbase.BackgroundImage = null;
			_iconNum = -1;

			TextureContainer retval = get_icons(iconNum);
			if (retval == null) {

			} else {
				textures = retval ?? new TextureContainer();
				foreach (Control child in flowLayoutPanel1.Controls) {
					if (child is ImagePreviewPanel) {
						setBG(child as ImagePreviewPanel);
					}
				}

				if (textures.prevbase_tex0 != null && textures.frontstname_tex0 != null) {
					label1.Text = "P " + size(textures.prevbase_tex0)
						+ " - F " + size(textures.frontstname_tex0)
						+ " - M " + size(textures.selmap_mark_tex0);
				}

				_iconNum = iconNum;
			}
		}

		public static string size(TEX0Node node) {
			if (node == null) return "null";
			return node.Width + "x" + node.Height;
		}

		private void setBG(Panel panel) {
			TEX0Node tex0 = GetTEX0For(panel);
			if (tex0 == null) {
				Bitmap b = new Bitmap(1, 1);
				b.SetPixel(0, 0, Color.Brown);
				panel.BackgroundImage = b;
			} else {
				Bitmap image = new Bitmap(
					tex0.GetImage(0),
					new Size(panel.Width, panel.Height));
				if (panel == selmap_mark && selmapMarkPreview) {
					panel.BackgroundImage = Utilities.AlphaSwap(image);
				} else {
					panel.BackgroundImage = image;
				}
			}
		}

		private TextureContainer get_icons(int iconNum) {
			if (common5 != null) {
				saveButton.Text = "Save common5";
			} else if (sc_selmap != null) {
				saveButton.Text = "Save sc_selmap";
			} else {
				return null;
			}

			TextureContainer result = new TextureContainer(sc_selmap, iconNum);
			return result;
		}

		private ResourceNode fcopy(string path) {
			FileInfo f = new FileInfo(path);
			if (!f.Exists) throw new IOException(f.FullName + " doesn't exist");

			string tempfile = TempFiles.Create();
			File.Copy(f.FullName, tempfile, true);
			return NodeFactory.FromFile(null, tempfile);
		}

		public void UpdateDirectory() {
			Console.WriteLine(System.Environment.CurrentDirectory);
			if (sc_selmap != null) sc_selmap.Dispose();
			if (common5 != null) common5.Dispose();
			_openFilePath = null;
			label1.Text = "";
			fileSizeBar.Maximum = 1214283;
			if (File.Exists("../../menu2/sc_selmap.pac")) {
				common5 = null;
				sc_selmap = fcopy("../../menu2/sc_selmap.pac");
				_openFilePath = "../../menu2/sc_selmap.pac";
			} else if (File.Exists("../../menu2/sc_selmap_en.pac")) {
				common5 = null;
				sc_selmap = fcopy("../../menu2/sc_selmap_en.pac");
				_openFilePath = "../../menu2/sc_selmap_en.pac";
			} else if (File.Exists("../../system/common5.pac")) {
				common5 = fcopy("../../system/common5.pac");
				sc_selmap = common5.FindChild("sc_selmap_en", false);
				_openFilePath = "../../system/common5.pac";
			} else if (File.Exists("../../system/common5_en.pac")) {
				common5 = fcopy("../../system/common5_en.pac");
				sc_selmap = common5.FindChild("sc_selmap_en", false);
				_openFilePath = "../../system/common5_en.pac";
			} else {
				common5 = null;
				sc_selmap = null;
				label1.Text = "Could not load sc_selmap(_en) or common5(_en).";
			}
			if (_openFilePath != null) {
				updateFileSize();
			} else {
				fileSizeBar.Value = 0;
				fileSizeLabel.Text = "";
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
						dlg.ImageSource = resizeToTempFile(filename, prevbaseResizeTo);
					} else if (sender == frontstname && frontstnameResizeTo != null) {
						dlg.ImageSource = resizeToTempFile(filename, frontstnameResizeTo);
					} else if (sender == selmap_mark && selmapMarkResizeTo != null) {
						dlg.ImageSource = resizeToTempFile(filename, selmapMarkResizeTo);
					} else {
						dlg.ImageSource = filename;
					}
					TEX0Node tex0 = GetTEX0For(sender);
					if (dlg.ShowDialog(null, tex0) == DialogResult.OK) {
						tex0.IsDirty = true;
						UpdateImage(_iconNum);
					}
				}
			}
		}

		private string resizeToTempFile(string filename, Size? resizeToArg) {
			Size resizeTo = resizeToArg ?? Size.Empty;
			string tempFile = TempFiles.Create();
			using (Bitmap orig = new Bitmap(filename)) {
				if (orig.Size.Width <= resizeTo.Width && orig.Size.Height <= resizeTo.Height) {
					File.Copy(filename, tempFile, true);
				} else {
					using (Bitmap resized = new Bitmap(orig, resizeTo)) {
						resized.Save(tempFile);
					}
				}
			}
			return tempFile;
		}

		public void save() {
			if (sc_selmap == null) {
				return;
			}

			ResourceNode toSave = common5 ?? sc_selmap;
			try {
				toSave.Merge();
				toSave.Export(_openFilePath);
			} catch (IOException) {
				toSave.Export(_openFilePath + ".out.pac");
				MessageBox.Show(_openFilePath + " could not be accessed.\nFile written to " + _openFilePath + ".out.pac");
			}

			updateFileSize();
		}
		
		protected void saveButton_Click(object sender, EventArgs e) {
			save();
		}

		private void updateFileSize() {
			long length;
			if (common5 != null) {
				string tempfile = Path.GetTempFileName();
				sc_selmap.Export(tempfile);
				length = new FileInfo(tempfile).Length;
				File.Delete(tempfile);
			} else {
				length = new FileInfo(_openFilePath).Length;
			}
			fileSizeBar.Value = Math.Min(fileSizeBar.Maximum, (int)length);
			fileSizeLabel.Text = length + " / " + fileSizeBar.Maximum;
		}

		private void button1_Click(object sender, EventArgs e) {
			if (flowLayoutPanel1.Visible) {
				flowLayoutPanel1.Visible = false;
				button1.Text = "<";
			} else {
				flowLayoutPanel1.Visible = true;
				button1.Text = ">";
			}
		}

		public void ExportImages(int p, string thisdir) {
			TextureContainer texs = get_icons(p);
			if (texs == null) return;

			if (texs.prevbase_tex0 != null) texs.prevbase_tex0.Export(thisdir + "/MenSelmapPrevbase.png");
			if (texs.icon_tex0 != null) texs.icon_tex0.Export(thisdir + "/MenSelmapIcon.png");
			if (texs.frontstname_tex0 != null) texs.frontstname_tex0.Export(thisdir + "/MenSelmapFrontStname.png");
			if (texs.seriesicon_tex0 != null) texs.seriesicon_tex0.Export(thisdir + "/MenSelchrMark.png");
			if (texs.selmap_mark_tex0 != null) texs.selmap_mark_tex0.Export(thisdir + "/MenSelmapMark.png");
		}

		public void openModifyPAT0Dialog() {
			modifyPAT0.PerformClick();
		}

		private void modifyPAT0_Click(object sender, EventArgs e) {
			if (textures == null) return;

			var result = new ModifyPAT0Dialog(textures).ShowDialog();
			if (result == DialogResult.OK) {
				// The dialog will mark the pat0 as dirty if changed
				UpdateImage(_iconNum);
			}
		}

		public void AddPAT0FromExisting(string pathToPAT0TextureNode) {
			AddPAT0(pathToPAT0TextureNode, true);
		}

		public void AddPAT0ByStageNumber(string pathToPAT0TextureNode) {
			AddPAT0(pathToPAT0TextureNode, false);
		}

		private void AddPAT0(string pathToPAT0TextureNode, bool fromExisting) {
			var look = sc_selmap.FindChild(pathToPAT0TextureNode, false).Children[0];
			if (!(look is PAT0TextureNode)) {
				throw new FormatException(look.Name);
			}

			bool icon = look.Parent.Name == "iconM";

			PAT0TextureNode tn = look as PAT0TextureNode;

			List<Tuple<string, float>> entries = new List<Tuple<string, float>>();
			foreach (var child in tn.Children.ToList()) {
				if (!(child is PAT0TextureEntryNode)) {
					throw new FormatException(child.Name);
				}
				PAT0TextureEntryNode entry = child as PAT0TextureEntryNode;
				entries.Add(new Tuple<string, float>(entry.Texture, entry.Key));
				if (entry.Key != 0) tn.RemoveChild(child);
			}

			string basename =  (from e in entries
							where e.Item1.Contains('.')
							select e.Item1).First();
			basename = basename.Substring(0, basename.LastIndexOf('.'));

			for (int i = 1; i < 80; i++) {
				string texname =
					!fromExisting ? basename + "." + i.ToString("D2")
					: ((i > 31 && i < 50) || (i > 59)) ? basename + "." + "00"
					: ((from e in entries
						where e.Item2 <= i
						orderby e.Item2 descending
						select e.Item1).FirstOrDefault()
						?? "ChangeThisTextureNamePlease");
				var entry = new PAT0TextureEntryNode();
				tn.AddChild(entry);
				entry.Key = i;
				entry.Texture = texname;
				if (icon) {
					entry.Palette = entry.Texture;
				}
			}

			var moreThan79query = from e in entries
								  where e.Item2 >= 80
								  orderby e.Item2 ascending
								  select e;
			foreach (var tuple in moreThan79query) {
				var entry = new PAT0TextureEntryNode();
				tn.AddChild(entry);
				entry.Key = tuple.Item2;
				entry.Texture = tuple.Item1;
				if (icon) {
					entry.Palette = entry.Texture;
				}
			}
		}

		public bool AddMenSelmapMark(string path, bool ask) {
			Bitmap bitmap = new Bitmap(path);
			string name = Path.GetFileNameWithoutExtension(path);
			if (ask) {
				using (var nameDialog = new AskNameDialog(bitmap)) {
					nameDialog.Text = name;
					if (nameDialog.ShowDialog() != DialogResult.OK) {
						return false;
					} else {
						name = nameDialog.NameText;
					}
				}
			}
			BRESNode bres = sc_selmap.FindChild("MiscData[80]", false) as BRESNode;
			TEX0Node tex0 = bres.CreateResource<TEX0Node>(name);
			tex0.ReplaceRaw(TextureConverter.IA4.EncodeTEX0Texture(bitmap, 1));
			return true;
		}
	}
}
