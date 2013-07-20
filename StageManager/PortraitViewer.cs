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
				Replace(sender, (e.Data.GetData(DataFormats.FileDrop) as string[])[0], false);
			}
		}

		public void Replace(object sender, string filename, bool useTextureConverter) {
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
					Replace(sender, tempFile, useTextureConverter); // call self with new file
					File.Delete(tempFile);
				}
			} else {
				TEX0Node tex0 = GetTEX0For(sender);
				if (useTextureConverter) {
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
						if (dlg.ShowDialog(null, tex0) == DialogResult.OK) {
							tex0.IsDirty = true;
							UpdateImage(_iconNum);
						}
					}
				} else {
					Bitmap bmp = new Bitmap(filename);
					if (sender == prevbase && prevbaseResizeTo != null) {
						bmp = Utilities.Resize(bmp, prevbaseResizeTo.Value);
					} else if (sender == frontstname && frontstnameResizeTo != null) {
						bmp = Utilities.Resize(bmp, frontstnameResizeTo.Value);
					} else if (sender == selmap_mark && selmapMarkResizeTo != null) {
						bmp = Utilities.Resize(bmp, selmapMarkResizeTo.Value);
					}
					tex0.Replace(bmp);
					tex0.IsDirty = true;
					UpdateImage(_iconNum);
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
					using (Bitmap thumbnail = Utilities.Resize(orig, resizeTo)) {
						thumbnail.Save(tempFile);
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

		private static int[] SelmapNumForThisSelcharacter2Num =
		{ 00,
		  01,02,03,04,05,
		  06,08,10,09,11,
		  12,13,14,15,16,
		  17,18,21,22,23,
		  27,26,19,24,07,
		  25,20,30,31,28,29,
		  32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,
		  50,51,52,53,54,
		  55,56,57,58,59};
		
		public void copyIconsToSelcharacter2() {
			string fileToSaveTo = null;

			ResourceNode s2 = null;
			if (common5 != null) {
				s2 = common5.FindChild("sc_selcharacter2_en", false);
			} else if (sc_selmap != null) {
				if (File.Exists("../../menu2/sc_selcharacter2.pac")) {
					fileToSaveTo = "../../menu2/sc_selcharacter2.pac";
					s2 = fcopy(fileToSaveTo);
				} else if (File.Exists("../../menu2/sc_selcharacter2_en.pac")) {
					fileToSaveTo = "../../menu2/sc_selcharacter2_en.pac";
					s2 = fcopy(fileToSaveTo);
				}
			}

			if (s2 == null) return;

			ResourceNode md0 = s2.FindChild("MenuRule_en/ModelData[0]", false);
			ResourceNode md80 = sc_selmap.FindChild("MiscData[80]", false);
			if (md0 == null || md80 == null) return;

			using (ProgressWindow w = new ProgressWindow()) {
				w.Begin(0, 60, 0);
				for (int i = 1; i < 60; i++) {
					if (i == 32) i = 50;
					string tempFile1 = TempFiles.Create(".tex0");
					string tempFile2 = TempFiles.Create(".plt0");
					string nameTo = i.ToString("D2");
					string nameFrom = SelmapNumForThisSelcharacter2Num[i].ToString("D2");
					TEX0Node iconFrom = md80.FindChild("Textures(NW4R)/MenSelmapIcon." + nameFrom, false) as TEX0Node;
					TEX0Node iconTo = md0.FindChild("Textures(NW4R)/MenSelmapIcon." + nameTo, false) as TEX0Node;
					var palFrom = md80.FindChild("Palettes(NW4R)/MenSelmapIcon." + nameFrom, false);
					var palTo = md0.FindChild("Palettes(NW4R)/MenSelmapIcon." + nameTo, false);
					if (iconFrom != null && iconTo != null && palFrom != null && palTo != null) {
						iconFrom.Export(tempFile1);
						iconTo.Replace(tempFile1);
						palFrom.Export(tempFile2);
						palTo.Replace(tempFile2);
					}

					TEX0Node prevbase = md80.FindChild("Textures(NW4R)/MenSelmapPrevbase." + SelmapNumForThisSelcharacter2Num[i].ToString("D2"), false) as TEX0Node;
					TEX0Node stageswitch = md0.FindChild("Textures(NW4R)/MenStageSwitch." + i.ToString("D2"), false) as TEX0Node;
					if (prevbase != null && stageswitch != null) {
						Bitmap b128 = new Bitmap(96, 48);
						using (var g = Graphics.FromImage(b128)) {
							g.DrawImage(prevbase.GetImage(0), 0, -24, 96, 96);
						}
						stageswitch.Replace(b128);
					}

					w.Update(i);
				}
			}

			if (fileToSaveTo != null) {
				s2.Export(fileToSaveTo);
			}
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

			List<PAT0TextureEntryNode> childrenList = (from c in tn.Children
													   where c is PAT0TextureEntryNode
													   select (PAT0TextureEntryNode)c).ToList();
			if ((from c in childrenList where c.Key >= 40 && c.Key < 50 select c).Count() >= 10) {
				MessageBox.Show("Skipping " + pathToPAT0TextureNode.Substring(pathToPAT0TextureNode.LastIndexOf('/') + 1) +
					" - mappings for icon numbers 40-49 already exist.");
				return;
			}

			List<Tuple<string, float>> entries = new List<Tuple<string, float>>();
			foreach (var entry in childrenList) {
				entries.Add(new Tuple<string, float>(entry.Texture, entry.Key));
				if (entry.Key != 0) tn.RemoveChild(entry);
			}

			string basename =  (from e in entries
							where e.Item1.Contains('.')
							select e.Item1).First();
			basename = basename.Substring(0, basename.LastIndexOf('.'));

			for (int i = 1; i < 80; i++) {
				string texname =
					fromExisting? ((from e in entries
									where e.Item2 <= i
									orderby e.Item2 descending
									select e.Item1).FirstOrDefault()
									?? "ChangeThisTextureNamePlease")
					: ((i > 31 && i < 50) || (i > 59)) ? basename + "." + "00"
					: basename + "." + i.ToString("D2");
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
