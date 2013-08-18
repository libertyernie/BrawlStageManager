using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrawlLib.SSBB.ResourceNodes;
using System.IO;
using BrawlLib.Wii.Textures;

namespace BrawlStageManager {
	public partial class MainForm : Form {

		public static OpenFileDialog OpenDialog { get; private set; }
		public static SaveFileDialog SaveDialog { get; private set; }
		public static FolderBrowserDialog FolderDialog { get; private set; }
		static MainForm() {
			OpenDialog = new OpenFileDialog();
			SaveDialog = new SaveFileDialog();
			FolderDialog = new FolderBrowserDialog();
		}

		/// <summary>
		/// The currently opened .pac file's root node.
		/// </summary>
		private ResourceNode _rootNode;
		/// <summary>
		/// The full path to the currently opened .pac file.
		/// </summary>
		private string _rootPath;

		/// <summary>
		/// The list of .pac files in the current directory.
		/// </summary>
		private FileInfo[] pacFiles;

		/// <summary>
		/// Same as System.Environment.CurrentDirectory.
		/// </summary>
		private string CurrentDirectory {
			get {
				return System.Environment.CurrentDirectory;
			}
			set {
				System.Environment.CurrentDirectory = value;
			}
		}

		/// <summary>
		/// Location of the folder with .rel files, relative to the current directory.
		/// </summary>
		private string moduleFolderLocation;

		private List<MDL0TextureNode> texNodes;

		/// <summary>
		/// Labels for RightControl.
		/// </summary>
		public Label chooseLabel, loadingLabel, noMSBinLabel, couldNotOpenLabel;

		/// <summary>
		/// Change the control used on the bottom-right section of the window (either a label or an MSBinViewer.)
		/// Any existing controls in that panel will be removed, and the new control's Dock property will be set to Fill.
		/// </summary>
		public Control RightControl {
			get {
				Control.ControlCollection controls = rightPanel.Controls;
				if (controls.Count > 0) {
					return controls[0];
				} else {
					return null;
				}
			}
			set {
				Control.ControlCollection controls = rightPanel.Controls;
				controls.Clear();
				value.Dock = System.Windows.Forms.DockStyle.Fill;
				controls.Add(value);
			}
		}

		public MainForm(string path, bool shouldVerifyIDs, bool useRelDescription) {
			InitializeComponent();

			moduleFolderLocation = "../../module";

			// Later commands to change the titlebar assume there is a hypen in the title somewhere
			this.Text += " -";

			#region labels
			this.chooseLabel = new Label();
			this.chooseLabel.Name = "chooseLabel";
			this.chooseLabel.Text = "Choose a stage from the list on the left-hand side.";
			this.chooseLabel.BackColor = Color.White;
			this.chooseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

			this.loadingLabel = new Label();
			this.loadingLabel.Name = "loadingLabel";
			this.loadingLabel.Text = "Loading...";
			/*this.loadingLabel.BackColor = Color.Gray;
			this.loadingLabel.ForeColor = Color.White;*/
			this.loadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

			this.noMSBinLabel = new Label();
			this.noMSBinLabel.Name = "noTextLabel";
			this.noMSBinLabel.Text = "There are no MSBin nodes in this stage.";
			this.noMSBinLabel.BackColor = Color.White;
			this.noMSBinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

			this.couldNotOpenLabel = new Label();
			this.couldNotOpenLabel.Name = "couldNotOpenLabel";
			this.couldNotOpenLabel.Text = "Could not open the .PAC file.";
			this.couldNotOpenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			#endregion

			RightControl = chooseLabel;

			// The defaults for these options depend on the defaults of the menu items that control them
			stageInfoControl1.ShouldVerifyIDs = verifyrelStageIDsToolStripMenuItem.Checked = shouldVerifyIDs;
			stageInfoControl1.UseRelDescription = useFullrelNamesToolStripMenuItem.Checked = useRelDescription;

			// Drag and drop for the left and right sides of the window. The dragEnter and dragDrop methods will check which panel the file is dropped onto.
			splitContainer2.AllowDrop = true;
			splitContainer2.DragEnter += new DragEventHandler(dragEnter);
			splitContainer2.DragDrop += new DragEventHandler(dragDrop);
			listBox1.AllowDrop = true;
			listBox1.DragEnter += new DragEventHandler(dragEnter);
			listBox1.DragDrop += new DragEventHandler(dragDrop);

			foreach (var item in selmapMarkFormat.DropDownItems) {
				((ToolStripMenuItem)item).Click += new System.EventHandler(this.selmapMarkFormatToolStripMenuItem_Click);
			}

			FormClosing += MainForm_FormClosing;
			FormClosed += MainForm_FormClosed;

			portraitViewer1.selmapMarkPreview = selmapMarkPreviewToolStripMenuItem.Checked;
			changeDirectory(path);
		}

		#region blacklist
		private static long[] sizes_of_broken_files = { 2261408, 3596224, 3728768, 510496, 911680, 3978464,
														  3560064, 1411712, 555488, 2906656 };
		private static string[] md5sums_of_broken_files = {
			"814f5f640226f1616966317807e1e1a2",  // mewtwo2000 venom
			"0a0767b84bd67e3cc6582f23a0eab6f9", // new pork city small version
			"e47bb210fee934c49c25aba7b7456acb", // brawl minus battlefield
			"feb6ae768107623f3512007bd803b425", // brawl minus yoshi's island melee
			"438612a2260b8f49d8741a87068c30b9", // brawl plus temple
			"30382d7f1a19d88beac5a08ca6b7f93d", // brawl plus new pork city
			"cb04c59e27273304eefec6913095bf3b", // rainbow road
			"0fd97f6cc51cf45c3f586062f4fd949e", // gloam valley
			"d5ce9dc752e19ffab8efa46a4f9da437", // gatelab
			"4c587bb2d124c3173819c051063bdf09", // great desert
			};
		#endregion

		private void open(FileInfo fi) {
			if (_rootNode != null) {
				_rootNode.Dispose();
				_rootNode = null;
				_rootPath = null;
			}
			if (fi == null) { // No .pac file selected (i.e. you just opened the program)
				RightControl = chooseLabel;
				return;
			}
			_rootPath = fi.FullName;
			if (renderModels.Checked) modelPanel1.ClearAll();

			string relname = matchRel(fi.Name);
			updateRel(relname);

			try {
				fi.Refresh(); // Update file size

				int isBrokenIndex = Array.IndexOf(sizes_of_broken_files, fi.Length);

				if (isBrokenIndex >= 0) { // mewtwo2000's venom causes latest brawllib to crash :(
					var md5provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
					byte[] hash = md5provider.ComputeHash(File.ReadAllBytes(fi.FullName));
					var sb = new System.Text.StringBuilder();
					foreach (byte b in hash) {
						sb.Append(b.ToString("x2").ToLower());
					}
					if (md5sums_of_broken_files.Contains(sb.ToString())) {
						throw new FileNotFoundException();
					}
				}
				_rootNode = NodeFactory.FromFile(null, _rootPath);
			} catch (FileNotFoundException) {
				// This might happen if you delete the file from Explorer after this program puts it in the list
				RightControl = couldNotOpenLabel;
			}
			if (_rootNode != null) {
				// Set the stage info labels. Equivalent labels for the .rel file are set when RelFile is changed in StageInfoControl
				stageInfoControl1.setStageLabels(fi.Name + ":", _rootNode.Name, "(" + fi.Length + " bytes)");
				stageInfoControl1.PacFileDeletion = delegate() {
					_rootNode.Dispose(); _rootNode = null;
					FileOperations.Delete(fi.FullName);
					changeDirectory(CurrentDirectory);
				};

				List<ResourceNode> allNodes = FindStageARC(_rootNode).Children; // Find all child nodes of "2"
				List<MSBinNode> msBinNodes = new List<MSBinNode>();
				texNodes = new List<MDL0TextureNode>();
				foreach (ResourceNode node in allNodes) {
					if (node.ResourceType == ResourceType.MSBin) {
						msBinNodes.Add((MSBinNode)node); // This is an MSBin node - add it to the list
					} else if (renderModels.Checked) {
						ResourceNode modelfolder = node.FindChild("3DModels(NW4R)", false);
						if (modelfolder != null) {
							foreach (ResourceNode child in modelfolder.Children) {
								if (child is MDL0Node) {
									MDL0Node model = child as MDL0Node;
									model.Populate();
									model._renderBones = false;
									model._renderPolygons = true;
									model._renderWireframe = false;
									model._renderVertices = false;
									model._renderBox = false;
									model.ApplyCHR(null, 0);
									model.ApplySRT(null, 0);
									if (model.TextureGroup != null) {
										foreach (ResourceNode tex in model.TextureGroup.Children) {
											if (tex is MDL0TextureNode) {
												texNodes.Add((MDL0TextureNode)tex);
											}
										}
									}
									modelPanel1.AddTarget(model);
								}
							}
						}
					}
				}
				if (renderModels.Checked) {
					modelPanel1.SetCamWithBox(new Vector3("-100,-100,-100"), new Vector3("100,100,100"));
					updateTexturesMenu();
				}
				if (msBinNodes.Count > 0) {
					ListControl list = new ListControl(msBinNodes); // Have ListControl manage these; make that the right panel
					RightControl = list;
				} else {
					RightControl = noMSBinLabel;
				}
			}
			portraitViewer1.UpdateImage(PortraitMap.Map[fi.Name]);
			this.Refresh();
		}

		public static ResourceNode FindStageARC(ResourceNode node) {
			foreach (ResourceNode child in node.Children) {
				foreach (ResourceNode child2 in child.Children) {
					if (child2.GetType() == typeof(CollisionNode)) {
						return child;
					}
				}
			}
			// fallback
			return node.FindChild("2", false);
		}

		private void updateTexturesMenu() {
			var items = texturesToolStripMenuItem.DropDownItems;
			items.Clear();
			foreach (MDL0TextureNode tex in texNodes) {
				ToolStripMenuItem item = new ToolStripMenuItem() {
					Text = tex.Name,
					Checked = true,
					CheckOnClick = true
				};
				item.Click += new EventHandler((sender, args) => {
					var menuitem = (sender as ToolStripMenuItem);
					int index = items.IndexOf(menuitem);
					//Console.WriteLine(index + " -> " + texNodes[index]);
					texNodes[index].Enabled = menuitem.Checked;
					modelPanel1.Invalidate();
				});
				items.Add(item);
			}
		}

		/// <summary>
		/// Adds the path to a .rel filename and updates RelFile in StageInfoControl.
		/// </summary>
		/// <param name="relname">Filename (not path) of the .rel file</param>
		private void updateRel(string relname) {
			string path = Path.GetFullPath(moduleFolderLocation + "\\" + relname);
			stageInfoControl1.RelFile = new FileInfo(path);
		}

		/// <summary>
		/// Finds the appropriate .rel filename for a stage .pac file.
		/// </summary>
		/// <param name="pacName">Filename of the .pac file</param>
		/// <returns>Filename of the .rel file (lowercase)</returns>
		public static string matchRel(string pacName) {
			string basename = pacBasename(pacName).ToLower();
			if (basename == "battlefield") {
				basename = "battle";
			} else if (basename == "chararoll") {
				basename = "croll";
			} else if (basename == "configtest") {
				basename = "config";
			} else if (basename == "onlinetraining") {
				basename = "otrain";
			} else if (basename.StartsWith("target")) {
				basename = "tbreak";
			}
			return "st_" + basename + ".rel";
		}

		/// <summary>
		/// Finds the base filename of a .pac file (for .rel matching.)
		/// The first three characters (typically STG) are removed, and the filename is cut off at the first instance of an underscore or period.
		/// </summary>
		/// <param name="pacName">Filename of the .pac file</param>
		/// <returns>"Base" filename of the stage</returns>
		public static string pacBasename(string pacName) {
			char[] cutAt = {'.', '_'};
			int cutAtPos = pacName.IndexOfAny(cutAt);
			if (cutAtPos <= 3) return "";
			return pacName.Substring(3, cutAtPos-3);
		}

		private void changeDirectory(string newpath) {
			changeDirectory(new DirectoryInfo(newpath));
		}
		private void changeDirectory(DirectoryInfo path) {
			CurrentDirectory = path.FullName; // Update the program's working directory
			this.Text = this.Text.Substring(0, this.Text.IndexOf('-')) + "- " + path.FullName; // Update titlebar

			RightControl = chooseLabel;

			pacFiles = path.GetFiles("*.pac");

			// Special code for the root directory of a drive
			if (pacFiles.Length == 0) {
				DirectoryInfo search = new DirectoryInfo(path.FullName + "\\private\\wii\\app\\RSBE\\pf\\stage\\melee");
				if (search.Exists) {
					changeDirectory(search); // Change to the typical stage folder used by the FPC, if it exists on the drive
					return;
				}
			}

			if (useAFixedStageListToolStripMenuItem.Checked) {
				List<string> list = new List<string>();
				foreach (string s in PortraitMap.StageOrder) {
					if (s.Length > 0) {
						list.AddRange(s == "starfox" ? new string[] { "STARFOX_GDIFF" } :
									  s == "emblem" ? new string[] { "EMBLEM_00", "EMBLEM_01", "EMBLEM_02" } :
									  s == "mariopast" ? new string[] { "MARIOPAST_00", "MARIOPAST_01" } :
									  s == "metalgear" ? new string[] { "METALGEAR_00", "METALGEAR_01", "METALGEAR_02" } :
									  s == "tengan" ? new string[] { "TENGAN_1", "TENGAN_2", "TENGAN_3" } :
									  s == "village" ? new string[] { "VILLAGE_00", "VILLAGE_01", "VILLAGE_02", "VILLAGE_03", "VILLAGE_04" } :
									  new string[] { s.ToUpper() });
					}
				}
				pacFiles = list.Select(s => new FileInfo("STG" + s + ".PAC")).ToArray();
			} else {
				Array.Sort(pacFiles, delegate(FileInfo f1, FileInfo f2) {
					return f1.Name.ToLower().CompareTo(f2.Name.ToLower()); // Sort by filename, case-insensitive
				});
			}
			listBox1.Items.Clear();
			listBox1.Items.AddRange(pacFiles);
			listBox1.Refresh();

			stageInfoControl1.setStageLabels("", "", "");
			stageInfoControl1.RelFile = null;

			portraitViewer1.UpdateDirectory();
		}

		public void dragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) { // Must be a file
				string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (s.Length == 1) { // Can only drag and drop one file
					string filename = s[0].ToLower();
					if (sender == listBox1) { // The sender is on the left - add a stage
						if (filename.EndsWith(".pac")) {
							e.Effect = DragDropEffects.Copy;
						}
					} else if (_rootPath != null) { // The sender must be on the right - modify an existing stage/module; ignore if no stage is selected
						if (filename.EndsWith(".pac") || filename.EndsWith(".rel") || Directory.Exists(filename)) {
							e.Effect = DragDropEffects.Copy;
						}
					}
				}
			}
		}

		public void dragDrop(object sender, DragEventArgs e) {
			string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
			string filepath = s[0].ToLower();
			string name;
			if (sender == listBox1) {
				NameDialog nd = new NameDialog();
				nd.Text = "Enter Filename"; // Titlebar
				nd.EntryText = s[0].Substring(s[0].LastIndexOf('\\')+1); // Textbox on the dialog ("Text" is already used by C#)
				if (nd.ShowDialog(this) == DialogResult.OK) {
					if (!nd.EntryText.ToLower().EndsWith(".pac")) {
						nd.EntryText += ".pac"; // Force .pac extension so it shows up in the list
					}
					FileOperations.Copy(filepath, CurrentDirectory + "\\" + nd.EntryText); // Use FileOperations (calls Windows shell -> asks for confirmation to overwrite)
					changeDirectory(CurrentDirectory); // Refresh .pac list
				}
			} else if (_rootPath != null) {
				name = new FileInfo(_rootPath).Name;
				if (_rootNode != null) {
					_rootNode.Dispose(); _rootNode = null; // Close the file before overwriting it!
				}
				if (filepath.EndsWith(".pac")) {
					FileOperations.Copy(filepath, CurrentDirectory + "\\" + name);
				} else if (filepath.EndsWith(".rel")) {
					FileOperations.Copy(filepath, stageInfoControl1.RelFile.FullName);
				} else if (Directory.Exists(filepath)) {
					importDir(filepath);
				}
				listBox1_SelectedIndexChanged(null, null);
			}
		}

		private void importDir(string dirpath) {
			DirectoryInfo dirinfo = new DirectoryInfo(dirpath);

			var pacfiles = dirinfo.EnumerateFiles("*.pac");
			if (!pacfiles.Any()) {
				MessageBox.Show("No .pac files found in this folder.");
				return;
			}
			FileInfo pac = pacfiles.First();

			var relfiles = dirinfo.EnumerateFiles("*.rel");
			if (!relfiles.Any()) {
				MessageBox.Show("No .rel files found in this folder.\nPlease add one that works with the stage.");
				return;
			}
			FileInfo rel = relfiles.First();

			FileOperations.Copy(pac.FullName, _rootPath);
			FileOperations.Copy(rel.FullName, stageInfoControl1.RelFile.FullName);

			if (portraitViewer1.SelmapLoaded) {
				var prevbases = dirinfo.EnumerateFiles("*Prevbase.*");
				if (prevbases.Any()) {
					portraitViewer1.Replace(portraitViewer1.prevbase, prevbases.First().FullName, true);
				}

				var icons = dirinfo.EnumerateFiles("*Icon.*");
				if (icons.Any()) {
					portraitViewer1.Replace(portraitViewer1.icon, icons.First().FullName, true);
				}

				var frontstnames = dirinfo.EnumerateFiles("*FrontStname.*");
				if (frontstnames.Any()) {
					portraitViewer1.Replace(portraitViewer1.frontstname, frontstnames.First().FullName, true);
				}

				var seriesicons = dirinfo.EnumerateFiles("*SeriesIcon.*")
					.Concat(dirinfo.EnumerateFiles("*SelchrMark.*"));
				if (seriesicons.Any()) {
					portraitViewer1.Replace(portraitViewer1.seriesicon, seriesicons.First().FullName, true);
				}

				var selmap_marks = dirinfo.EnumerateFiles("*SelmapMark.*");
				if (selmap_marks.Any()) {
					portraitViewer1.Replace(portraitViewer1.selmap_mark, selmap_marks.First().FullName, true);
				}
			}
		}

		private static string readNameFromPac(FileInfo f) {
			var sb = new System.Text.StringBuilder();
			using (var stream = new FileStream(f.FullName, FileMode.Open, FileAccess.Read)) {
				stream.Seek(16, SeekOrigin.Begin);
				int b = stream.ReadByte();
				while (b == 0) {
					b = stream.ReadByte();
				}
				while (b != 0) {
					sb.Append((char)b);
					b = stream.ReadByte();
				}
			}
			if (sb.ToString().IndexOfAny(Path.GetInvalidFileNameChars()) > -1) {
				return f.Name;
			}
			return sb.ToString() + ".pac";
		}
		private void exportAllToolStripMenuItem_Click(object sender, EventArgs e) {
			if (FolderDialog.ShowDialog() != DialogResult.OK) {
				return;
			}

			string outdir = FolderDialog.SelectedPath;
			if (Directory.Exists(outdir) && Directory.EnumerateFileSystemEntries(outdir).Any()) {
				var dr = MessageBox.Show("Is it OK to delete everything in " + outdir + "?", "", MessageBoxButtons.OKCancel);
				if (dr != DialogResult.OK) {
					return;
				}
				Directory.Delete(outdir, true);
			}
			using (ProgressWindow progress = new ProgressWindow((Control)null, "Exporting...", "", true)) {
				progress.Begin(0, listBox1.Items.Count, 0);
				Directory.CreateDirectory(outdir);
				int i = 0;
				foreach (FileInfo f in listBox1.Items) {
					if (progress.Cancelled) {
						break;
					}
					progress.Update(++i);
					string thisdir = outdir + "/" + f.Name.Substring(0, f.Name.LastIndexOf('.'));
					exportStage(f, thisdir);
				}
			}
		}

		private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			listBox1.SelectedIndex = listBox1.IndexFromPoint(listBox1.PointToClient(Cursor.Position));
		}

		private void exportStageToolStripMenuItem_Click(object sender, EventArgs e) {
			if (FolderDialog.ShowDialog() != DialogResult.OK) {
				return;
			}

			string outdir = FolderDialog.SelectedPath;
			exportStage(listBox1.SelectedItem as FileInfo, outdir);
		}

		#region event handlers
		private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
			open((FileInfo)listBox1.SelectedItem);
		}

		private void changeDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
			FolderDialog.SelectedPath = CurrentDirectory; // Uncomment this if you want the "change directory" dialog to start with the current directory selected
			if (FolderDialog.ShowDialog() == DialogResult.OK) {
				changeDirectory(FolderDialog.SelectedPath);
			}
		}

		private void sameToolStripMenuItem_Click(object sender, EventArgs e) {
			moduleFolderLocation = ".";
			sameToolStripMenuItem.Checked = true;
			moduleToolStripMenuItem.Checked = false;
			if (stageInfoControl1.RelFile != null) updateRel(stageInfoControl1.RelFile.Name); // Refresh the .rel
		}

		private void moduleToolStripMenuItem_Click(object sender, EventArgs e) {
			moduleFolderLocation = "../../module";
			sameToolStripMenuItem.Checked = false;
			moduleToolStripMenuItem.Checked = true;
			if (stageInfoControl1.RelFile != null) updateRel(stageInfoControl1.RelFile.Name); // Refresh the .rel
		}

		private void verifyrelStageIDsToolStripMenuItem_Click(object sender, EventArgs e) {
			stageInfoControl1.ShouldVerifyIDs = verifyrelStageIDsToolStripMenuItem.Checked;
		}
		private void useFullrelNamesToolStripMenuItem_Click(object sender, EventArgs e) {
			stageInfoControl1.UseRelDescription = useFullrelNamesToolStripMenuItem.Checked;
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutBSM(Icon).ShowDialog(this);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Close();
		}

		private void prevbaseSizeToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in prevbaseSize.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == prevbaseOriginalSizeToolStripMenuItem) {
				portraitViewer1.prevbaseResizeTo = null;
			} else if (sender == x128ToolStripMenuItem) {
				portraitViewer1.prevbaseResizeTo = new Size(128, 128);
			} else if (sender == x88ToolStripMenuItem) {
				portraitViewer1.prevbaseResizeTo = new Size(88, 88);
			}
		}

		private void frontstnameSizeToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in frontstnameSizeToolStripMenuItem.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == frontstnameOriginalSizeToolStripMenuItem) {
				portraitViewer1.frontstnameResizeTo = null;
			} else if (sender == x56ToolStripMenuItem) {
				portraitViewer1.frontstnameResizeTo = new Size(104, 56);
			}
		}

		private void selmapMarkToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in selmapMarkSizeToolStripMenuItem.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == selmapMarkOriginalSizeToolStripMenuItem) {
				portraitViewer1.selmapMarkResizeTo = null;
			} else if (sender == x56ToolStripMenuItem1) {
				portraitViewer1.selmapMarkResizeTo = new Size(104, 56);
			}
		}

		private void selmapMarkPreviewToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.selmapMarkPreview = selmapMarkPreviewToolStripMenuItem.Checked;
			selchrMarkAsBGBetaToolStripMenuItem.Enabled = selmapMarkPreviewToolStripMenuItem.Checked;
			portraitViewer1.UpdateImage();
		}

		private void selchrMarkAsBGBetaToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.selchrMarkAsBG = selchrMarkAsBGBetaToolStripMenuItem.Checked;
			portraitViewer1.UpdateImage();
		}

		private void useAFixedStageListToolStripMenuItem_Click(object sender, EventArgs e) {
			var result = MessageBox.Show("This will reload the common5/sc_selmap from the disk.", "Continue?", MessageBoxButtons.YesNo);
			if (result == DialogResult.Yes) {
				changeDirectory(CurrentDirectory); // Refresh .pac list
			}
		}

		private void addmissingPAT0EntriesToolStripMenuItem_Click(object sender, EventArgs e) {
			if (DialogResult.Yes == MessageBox.Show("Would you like to fill in the gaps on SelchrMark and SelmapMark PAT0 entries so there's one for each stage? (The mappings will remain the same until you modify them. Renaming MenSelchrMarks after doing this may cause problems, but for MenSelmapMarks it should be OK.)", "Confirm", MessageBoxButtons.YesNo)) {
				portraitViewer1.AddPAT0FromExisting("MiscData[80]/AnmTexPat(NW4R)/MenSelmapPreview/pasted__stnamelogoM");
				portraitViewer1.AddPAT0FromExisting("MiscData[80]/AnmTexPat(NW4R)/MenSelmapPreview/lambert113");
				portraitViewer1.AddPAT0ByStageNumber("MiscData[80]/AnmTexPat(NW4R)/MenSelmapPreview/basebgM");
				portraitViewer1.AddPAT0ByStageNumber("MiscData[80]/AnmTexPat(NW4R)/MenSelmapPreview/pasted__stnameM");
				portraitViewer1.AddPAT0ByStageNumber("MiscData[80]/AnmTexPat(NW4R)/MenSelmapPreview/pasted__stnameshadowM");
				portraitViewer1.AddPAT0ByStageNumber("MiscData[80]/AnmTexPat(NW4R)/MenSelmapIcon/iconM");
				MessageBox.Show("Save the common5/sc_selmap file and restart the program for the changes to take effect.");
			}
		}

		private void addMenSelmapMarksToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenDialog.Filter = BrawlLib.ExportFilters.TEX0;
			OpenDialog.Multiselect = true;
			if (OpenDialog.ShowDialog() == DialogResult.OK) {
				var result = MessageBox.Show("Ask for a name for each texture?", Text, MessageBoxButtons.YesNoCancel);
				if (result != DialogResult.Cancel) {
					bool ask = (DialogResult.Yes == result);
					foreach (string file in OpenDialog.FileNames) {
						bool r = portraitViewer1.AddMenSelmapMark(file, ask);
						if (!r) break;
					}
				}
			}
			OpenDialog.Multiselect = false;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (portraitViewer1.IsDirty) {
				var result = MessageBox.Show("Would you like to save common5/sc_selmap before closing?", Text, MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Cancel) {
					e.Cancel = true;
				} else if (result == DialogResult.Yes) {
					portraitViewer1.save();
				}
			}
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			TempFiles.TryToDeleteAll();
		}

		private void updateScselcharacter2ToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.copyIconsToSelcharacter2();
		}

		private void downgradeMenSelmapMarksToI4ToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach (FileInfo f in listBox1.Items) {
				int i = PortraitMap.Map[f.Name];
				portraitViewer1.DowngradeMenSelmapMark(i);
			}
		}

		private void selmapMarkFormatToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in selmapMarkFormat.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == selmapMarkFormatIA4) {
				portraitViewer1.selmapMarkFormat = WiiPixelFormat.IA4;
			} else if (sender == selmapMarkFormatI4) {
				portraitViewer1.selmapMarkFormat = WiiPixelFormat.I4;
			} else if (sender == selmapMarkFormatAuto) {
				portraitViewer1.selmapMarkFormat = null;
				portraitViewer1.selmapMarkFallback = PortraitViewer.Fallback.Auto;
			} else if (sender == selmapMarkFormatCMPR) {
				portraitViewer1.selmapMarkFormat = WiiPixelFormat.CMPR;
			} else if (sender == selmapMarkFormatExisting) {
				portraitViewer1.selmapMarkFormat = null;
				portraitViewer1.selmapMarkFallback = PortraitViewer.Fallback.Existing;
			}
		}
		#endregion

		private void exportStage(FileInfo f, string thisdir) {
			Directory.CreateDirectory(thisdir);
			var pacs = Directory.EnumerateFiles(thisdir, "*.pac");
			if (pacs.Any()) {
				var result = MessageBox.Show(this, "This directory already contains a .PAC file. " +
					"Is it okay to remove it and the other stage files in this folder? " +
					"(If the recycle bin is enabled, the files will be sent there.)",
					"Overwrite", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes) {
					foreach (string file in pacs) {
						// Send pac files to recycle bin (if bin is enabled for this drive)
						FileOperations.Delete(file, FileOperations.FileOperationFlags.FOF_NOCONFIRMATION);
					}
					// Also recycle other files
					string[] toRecycle = {"st*.rel", "*Prevbase.*", "*Icon.*", "*FrontStname.*",
											 "*SeriesIcon.*", "*SelchrMark.*", "*SelmapMark.*"};
					foreach (string filename in toRecycle) {
						FileOperations.Delete(thisdir + "/" + filename, FileOperations.FileOperationFlags.FOF_NOCONFIRMATION);
					}
				} else {
					return;
				}
			}
			string p = readNameFromPac(f);
			FileOperations.Copy(f.FullName, thisdir + "/" + p);
			FileInfo rel = new FileInfo("../../module/" + matchRel(f.Name));
			if (rel.Exists) FileOperations.Copy(rel.FullName, thisdir + "/st.rel");

			portraitViewer1.ExportImages(PortraitMap.Map[f.Name], thisdir);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e) {
			Console.WriteLine(e.KeyCode);
			if (e.KeyCode == Keys.PageDown) {
				e.Handled = true;
				if (listBox1.SelectedIndex == listBox1.Items.Count - 1) {
					listBox1.SelectedIndex = 0;
				} else {
					listBox1.SelectedIndex++;
				}
			} else if (e.KeyCode == Keys.PageUp) {
				e.Handled = true;
				if (listBox1.SelectedIndex <= 0) {
					listBox1.SelectedIndex = listBox1.Items.Count - 1;
				} else {
					listBox1.SelectedIndex--;
				}
			} else if (e.KeyCode == Keys.Oemtilde) {
				e.Handled = true;
				portraitViewer1.openModifyPAT0Dialog();
			} else if (e.KeyCode == Keys.Oem5) {
				e.Handled = true;
				portraitViewer1.changeIconBorder();
			}
		}
	}
}
