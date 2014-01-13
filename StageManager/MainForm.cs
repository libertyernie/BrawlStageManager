﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrawlLib.SSBB.ResourceNodes;
using System.IO;
using BrawlLib.Wii.Textures;
using BrawlStageManager.RegistryUtilities;
using BrawlManagerLib;

namespace BrawlStageManager {
	public partial class MainForm : Form {
		private static OpenFileDialog OpenDialog = new OpenFileDialog();
		private static SaveFileDialog SaveDialog = new SaveFileDialog();
		private static FolderBrowserDialog FolderDialog = new FolderBrowserDialog();

		#region Folder-scope variables
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
		#endregion

		#region Stage-scope variables
		/// <summary>
		/// The currently opened .pac file's root node.
		/// </summary>
		private ResourceNode _rootNode;
		/// <summary>
		/// The full path to the currently opened .pac file.
		/// </summary>
		private string _rootPath;

		private List<MDL0TextureNode> texNodes;
		#endregion

		#region Window-scope variables
		/// <summary>
		/// Labels for the MSBin viewer area.
		/// </summary>
		private Label noMSBinLabel, couldNotOpenLabel;

		/// <summary>
		/// Change the control used on the upper-middle section of the window (either a label or an MSBinViewer.)
		/// Any existing controls in that panel will be removed, and the new control's Dock property will be set to Fill.
		/// </summary>
		private Control RightControl {
			get {
				Control.ControlCollection controls = msBinPanel.Controls;
				if (controls.Count > 0) {
					return controls[0];
				} else {
					return null;
				}
			}
			set {
				Control.ControlCollection controls = msBinPanel.Controls;
				controls.Clear();
				if (value != null) value.Dock = System.Windows.Forms.DockStyle.Fill;
				if (value != null) controls.Add(value);
			}
		}
		#endregion

		public MainForm(string path, bool useRelDescription) {
			InitializeComponent();

			clearDefaultDirectoryToolStripMenuItem.Enabled = (DefaultDirectory.Get() != null);

			#region initialize from registry
			Size?[] sizes = ResizeSettings.Get();

			if (sizes[0] != null) {
				portraitViewer1.prevbaseResizeTo = sizes[0];
				prevbaseOriginalSizeToolStripMenuItem.Checked = false;
				if (sizes[0] == new Size(88, 88)) {
					x88ToolStripMenuItem.Checked = true;
				} else if (sizes[0] == new Size(128, 128)) {
					x128ToolStripMenuItem.Checked = true;
				} else {
					customPrevbaseSizeToolStripMenuItem.Checked = true;
				}
			}

			if (sizes[1] != null) {
				portraitViewer1.frontstnameResizeTo = sizes[1];
				frontstnameOriginalSizeToolStripMenuItem.Checked = false;
				if (sizes[1] == new Size(104, 56)) {
					x56ToolStripMenuItem.Checked = true;
				}
			}

			if (sizes[2] != null) {
				portraitViewer1.selmapMarkResizeTo = sizes[2];
				selmapMarkOriginalSizeToolStripMenuItem.Checked = false;
				if (sizes[2] == new Size(60, 56)) {
					x56ToolStripMenuItem1.Checked = true;
				}
			}

			portraitViewer1.fontSettings = FontSettings.Get() ?? portraitViewer1.fontSettings;
			#endregion

			moduleFolderLocation = "../../module";

			// Later commands to change the titlebar assume there is a hyphen in the title somewhere
			this.Text += " -";

			#region labels
			this.noMSBinLabel = new Label();
			this.noMSBinLabel.Name = "noTextLabel";
			this.noMSBinLabel.Text = "There are no MSBin nodes in this stage.";
			this.noMSBinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

			this.couldNotOpenLabel = new Label();
			this.couldNotOpenLabel.Name = "couldNotOpenLabel";
			this.couldNotOpenLabel.Text = "Could not open the .PAC file.";
			this.couldNotOpenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			#endregion

			RightControl = null;

			// The defaults for these options depend on the defaults of the menu items that control them
			stageInfoControl1.UseRelDescription = useFullrelNamesToolStripMenuItem.Checked = useRelDescription;

			// Drag and drop for the left and right sides of the window. The dragEnter and dragDrop methods will check which panel the file is dropped onto.
			splitContainer1.Panel2.AllowDrop = true;
			splitContainer1.Panel2.DragEnter += new DragEventHandler(dragEnter);
			splitContainer1.Panel2.DragDrop += new DragEventHandler(dragDrop);
			listBox1.AllowDrop = true;
			listBox1.DragEnter += new DragEventHandler(dragEnter);
			listBox1.DragDrop += new DragEventHandler(dragDrop);

			foreach (var item in selmapMarkFormat.DropDownItems) {
				((ToolStripMenuItem)item).Click += new System.EventHandler(this.switchSelmapMarkFormat);
			}

			foreach (var item in prevbaseSize.DropDownItems) {
				((ToolStripMenuItem)item).Click += new System.EventHandler(this.switchPrevbaseSize);
			}
			foreach (var item in frontstnameSizeToolStripMenuItem.DropDownItems) {
				((ToolStripMenuItem)item).Click += new System.EventHandler(this.switchFrontstnameSize);
			}
			foreach (var item in selmapMarkSizeToolStripMenuItem.DropDownItems) {
				((ToolStripMenuItem)item).Click += new System.EventHandler(this.switchSelmapMarkSize);
			}

			fileToolStripMenuItem.DropDownOpening += (o, e) => {
				MoveToolStripItems(contextMenuStrip1.Items, currentStageToolStripMenuItem.DropDownItems);
			};
			
			FormClosing += MainForm_FormClosing;
			FormClosed += MainForm_FormClosed;

			clbTextures.ItemCheck += (o, e) => {
				texNodes[e.Index].Enabled = (e.NewValue == CheckState.Checked);
				modelPanel1.Invalidate();
			};

			portraitViewer1.selmapMarkPreview = selmapMarkPreviewToolStripMenuItem.Checked;
			portraitViewer1.useTextureConverter = useTextureConverterToolStripMenuItem.Checked;
			LoadFromRegistry();
			changeDirectory(path);
		}

		private static void MoveToolStripItems(ToolStripItemCollection from, ToolStripItemCollection to) {
			ToolStripItem[] arr = new ToolStripItem[from.Count];
			from.CopyTo(arr, 0);
			to.AddRange(arr);
		}

		/// <summary>
		/// If the common5/sc_selmap is dirty, asks the user whether they want to save it.
		/// </summary>
		/// <returns>true if the file did not need to be saved OR the user saved it; false otherwise.</returns>
		private bool saveCommon5IfNecessary() {
			if (portraitViewer1.IsDirty) {
				var result = MessageBox.Show("Would you like to save common5/sc_selmap before closing?", Text, MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Cancel) {
					return false;
				} else if (result == DialogResult.Yes) {
					portraitViewer1.save();
					return true;
				} else if (result == DialogResult.No) {
					return true;
				}
			}
			// not dirty
			return true;
		}

		#region Opening files
		private void open(FileInfo fi) {
			if (_rootNode != null) {
				_rootNode.Dispose();
				_rootNode = null;
				_rootPath = null;
			}
			if (fi == null) { // No .pac file selected (i.e. you just opened the program)
				RightControl = null;
				return;
			}

			_rootPath = fi.FullName;
			if (renderModels.Checked) modelPanel1.ClearAll();

			string relname = StageIDMap.RelNameForPac(fi.Name);
			updateRel(relname);

			try {
				fi.Refresh(); // Update file size

				var md5provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
				byte[] hash = md5provider.ComputeHash(File.ReadAllBytes(fi.FullName));
				var sb = new System.Text.StringBuilder();
				foreach (byte b in hash) {
					sb.Append(b.ToString("x2").ToLower());
				}
				stageInfoControl1.MD5 = sb.ToString();
				_rootNode = NodeFactory.FromFile(null, _rootPath);
			} catch (FileNotFoundException) {
				// This might happen if you delete the file from Explorer after this program puts it in the list
				RightControl = couldNotOpenLabel;
			}
			
			if (_rootNode != null) {
				// Set the stage info labels. Equivalent labels for the .rel file are set when RelFile is changed in StageInfoControl
				stageInfoControl1.setStageLabels(fi.Name + ":", _rootNode.Name, "(" + fi.Length + " bytes)");

				#region Scan for 3D models and MSBin text nodes
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
								if (child is MDL0Node && !child.Name.StartsWith("MShadow")) {
									try {
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
									} catch (InvalidOperationException e) {
										Console.Error.WriteLine(child.Name + ": " + e.Message);
									}
								}
							}
						}
					}
				}
				if (renderModels.Checked) {
					modelPanel1.SetCamWithBox(new Vector3("-100,-100,-100"), new Vector3("100,100,100"));
					
					// Update textures list
					var items = clbTextures.Items;
					items.Clear();
					foreach (MDL0TextureNode tex in texNodes) {
						items.Add(tex, true);
					}
				}
				if (msBinNodes.Count > 0) {
					ListControl list = new ListControl(msBinNodes); // Have ListControl manage these; make that the right panel
					RightControl = list;
				} else {
					RightControl = noMSBinLabel;
				}
				#endregion
			}

			int stage_id = StageIDMap.StageIDForPac(fi.Name);
			portraitViewer1.UpdateImage(portraitViewer1.BestSSS.IconForStage(stage_id));
			#region finding .brstm
			listBoxSongs.Items.Clear();
			if (!loadbrstmsToolStripMenuItem.Checked) {
				songPanel1.Close();
			} else {
				Song song;
				if (portraitViewer1.BestSSS.SongsByStage.TryGetValue((byte)stage_id, out song)) {
					songContainerPanel.Visible = true;
					listBoxSongs.Items.Add(new SongListItem("../../sound/strm/" + song.Filename + ".brstm"));
					listBoxSongs.SelectedIndex = 0;
				} else {
					string[] arr = SongsByStageID.ForPac(fi.Name);
					if (arr != null) {
						songContainerPanel.Visible = true;
						listBoxSongs.Items.AddRange(arr);
						listBoxSongs.SelectedIndex = 0;
					} else {
						songContainerPanel.Visible = false;
						songPanel1.Close();
					}
				}
			}
			if (listBoxSongs.Items.Count > 0) {
				listBoxSongs.Visible = true;
				listBoxSongs.SelectedIndex = 0;
			} else {
				listBoxSongs.Visible = false;
			}
			exportbrstmToolStripMenuItem.Enabled = deletebrstmToolStripMenuItem.Enabled = songPanel1.FileOpen;
			#endregion

			this.Refresh();
		}

		/// <summary>
		/// Adds the path to a .rel filename and updates RelFile in StageInfoControl.
		/// </summary>
		/// <param name="relname">Filename (not path) of the .rel file</param>
		private void updateRel(string relname) {
			string path = Path.GetFullPath(moduleFolderLocation + "\\" + relname);
			stageInfoControl1.RelFile = new FileInfo(path);
		}
		#endregion

		#region Changing directory
		private void changeDirectory(string newpath) {
			changeDirectory(new DirectoryInfo(newpath));
		}
		private void changeDirectory(DirectoryInfo path) {
			CurrentDirectory = path.FullName; // Update the program's working directory
			this.Text = this.Text.Substring(0, this.Text.IndexOf('-')) + "- " + path.FullName; // Update titlebar

			RightControl = null;

			pacFiles = path.GetFiles("*.pac");

			// Special code for the root directory of a drive
			if (pacFiles.Length == 0) {
				DirectoryInfo search = new DirectoryInfo(path.FullName + "\\private\\wii\\app\\RSBE\\pf\\stage\\melee");
				if (search.Exists) {
					changeDirectory(search); // Change to the typical stage folder used by the FPC, if it exists on the drive
					return;
				}
				search = new DirectoryInfo(path.FullName + "\\projectm\\pf\\stage\\melee");
				if (search.Exists) {
					changeDirectory(search); // Change to the typical stage folder used by Project M, if it exists on the drive
					return;
				}
			}

			if (useAFixedStageListToolStripMenuItem.Checked) {
				pacFiles = StageIDMap.PacFilesBySSSOrder(portraitViewer1.BestSSS).Select(s => new FileInfo(s)).ToArray();
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

			Console.WriteLine(songPanel1.findInfoFile());

			portraitViewer1.UpdateDirectory();

			if (portraitViewer1.BestSSS.OtherCodesIgnoredInSameFile > 0) {
				MessageBox.Show(this, "More than one Custom SSS code found in the codeset. All but the last one will be ignored.",
					this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		#endregion

		#region Replacing & exporting files
		#region drag-and-drop
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
				nd.LabelText = "Enter the filename to copy to (with or without the .pac extension):";
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
		#endregion

		private void importDir(string dirpath) {
			DirectoryInfo dirinfo = new DirectoryInfo(dirpath);

			var pacfiles = dirinfo.EnumerateFiles("*.pac");
			if (!pacfiles.Any()) {
				MessageBox.Show("No .pac files found in this folder.");
				return;
			}
			FileInfo pac = pacfiles.First();

			var relfiles = dirinfo.EnumerateFiles("*.rel");
			string rel = relfiles.Any() ? relfiles.First().FullName : "No .rel file found";

			DialogResult r = new CopyPacRelDialog(pac.FullName, _rootPath, rel, stageInfoControl1.RelFile.FullName).ShowDialog();
			if (r == DialogResult.Cancel) return;
			if (r == DialogResult.Yes) {
				File.Copy(pac.FullName, _rootPath, true);
				if (File.Exists(rel)) File.Copy(rel, stageInfoControl1.RelFile.FullName, true);
			}

			if (portraitViewer1.SelmapLoaded) {
				var prevbases = dirinfo.EnumerateFiles("*Prevbase.*");
				if (prevbases.Any()) {
					portraitViewer1.Replace(portraitViewer1.prevbase, prevbases.First().FullName);
				}

				var icons = dirinfo.EnumerateFiles("*Icon.*");
				if (icons.Any()) {
					portraitViewer1.Replace(portraitViewer1.icon, icons.First().FullName);
				}

				var frontstnames = dirinfo.EnumerateFiles("*FrontStname.*");
				if (frontstnames.Any()) {
					portraitViewer1.Replace(portraitViewer1.frontstname, frontstnames.First().FullName);
				}

				var seriesicons = dirinfo.EnumerateFiles("*SeriesIcon.*")
					.Concat(dirinfo.EnumerateFiles("*SelchrMark.*"));
				if (seriesicons.Any()) {
					portraitViewer1.Replace(portraitViewer1.seriesicon, seriesicons.First().FullName);
				}

				var selmap_marks = dirinfo.EnumerateFiles("*SelmapMark.*");
				if (selmap_marks.Any()) {
					portraitViewer1.Replace(portraitViewer1.selmap_mark, selmap_marks.First().FullName);
				}
			}
		}

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
			string relname = StageIDMap.RelNameForPac(f.Name);
			FileInfo rel = new FileInfo("../../module/" + relname);
			if (rel.Exists) FileOperations.Copy(rel.FullName, thisdir + "/st.rel");

			portraitViewer1.ExportImages(portraitViewer1.BestSSS.IconForStage(StageIDMap.StageIDForPac(f.Name)), thisdir);
		}
		#endregion

		#region Reading .pac files
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

		private static ResourceNode FindStageARC(ResourceNode node) {
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
		#endregion

		#region event handlers
		private void exportpacrelToolStripMenuItem_Click(object sender, EventArgs e) {
			if (FolderDialog.ShowDialog() != DialogResult.OK) {
				return;
			}

			string outdir = FolderDialog.SelectedPath;
			exportStage(listBox1.SelectedItem as FileInfo, outdir);
		}
		private void deletepacrelToolStripMenuItem_Click(object sender, EventArgs e) {
			_rootNode.Dispose();
			_rootNode = null;
			FileOperations.Delete(stageInfoControl1.RelFile.FullName);
			FileOperations.Delete((listBox1.SelectedItem as FileInfo).FullName);
			changeDirectory(CurrentDirectory);
		}
		private void exportbrstmToolStripMenuItem_Click(object sender, EventArgs e) {
			songPanel1.Export();
		}
		private void deletebrstmToolStripMenuItem_Click(object sender, EventArgs e) {
			songPanel1.Delete();
		}
		private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
			open((FileInfo)listBox1.SelectedItem);
		}
		private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			listBox1.SelectedIndex = listBox1.IndexFromPoint(listBox1.PointToClient(Cursor.Position));
			MoveToolStripItems(currentStageToolStripMenuItem.DropDownItems, contextMenuStrip1.Items);
			e.Cancel = false;
		}

		private void changeDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
			FolderDialog.SelectedPath = CurrentDirectory; // Uncomment this if you want the "change directory" dialog to start with the current directory selected
			if (FolderDialog.ShowDialog() == DialogResult.OK) {
				changeDirectory(FolderDialog.SelectedPath);
			}
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
					string thisdir = outdir + "\\" + f.Name.Substring(0, f.Name.LastIndexOf('.'));
					Directory.CreateDirectory(thisdir);
					exportStage(f, thisdir);
				}
			}
		}
		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Close();
		}

		private void saveCurrentDirectoryAsDefaultToolStripMenuItem_Click(object sender, EventArgs e) {
			DefaultDirectory.Set(CurrentDirectory);
			clearDefaultDirectoryToolStripMenuItem.Enabled = true;
		}
		private void clearDefaultDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
			DefaultDirectory.Clear();
			clearDefaultDirectoryToolStripMenuItem.Enabled = false;
		}
		private void saveAllStageManagerSettingsToolStripMenuItem_Click(object sender, EventArgs e) {
			DefaultDirectory.Set(CurrentDirectory);
			bool anyNotNull = ResizeSettings.WriteToRegistry(portraitViewer1);
			string str = FontSettings.WriteToRegistry(portraitViewer1.fontSettings);
			SaveToRegistry();
			MessageBox.Show(
				(str != null
					? "The default font has been set to: " + str
					: "The default font settings have been cleared.")
				+ "\n"
				+ (anyNotNull
					? "The default texture sizes have been set in HKEY_CURRENT_USER."
					: "The auto-resize settings have been cleared (all three set to \"Off.\")"));

		}
		private void clearAllStageManagerSettingsToolStripMenuItem_Click(object sender, EventArgs e) {
			ClearRegistry();
		}

		private void useTextureConverterToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.useTextureConverter = useTextureConverterToolStripMenuItem.Checked;
		}
		private void useAFixedStageListToolStripMenuItem_Click(object sender, EventArgs e) {
			bool cont = saveCommon5IfNecessary();
			if (cont) {
				changeDirectory(CurrentDirectory); // Refresh .pac list
			}
		}
		private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e) {
			using (ColorDialog cd = new ColorDialog()) {
				cd.Color = portraitViewer1.BackColor;
				if (cd.ShowDialog() == DialogResult.OK) {
					portraitViewer1.BackColor = cd.Color;
				}
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
		private void useFullrelNamesToolStripMenuItem_Click(object sender, EventArgs e) {
			stageInfoControl1.UseRelDescription = useFullrelNamesToolStripMenuItem.Checked;
		}
		private void selmapMarkPreviewToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.selmapMarkPreview = selmapMarkPreviewToolStripMenuItem.Checked;
			portraitViewer1.UpdateImage();
		}
		private void switchSelmapMarkFormat(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in selmapMarkFormat.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == selmapMarkFormatIA4) {
				portraitViewer1.selmapMarkFormat = WiiPixelFormat.IA4;
			} else if (sender == selmapMarkFormatI4) {
				portraitViewer1.selmapMarkFormat = WiiPixelFormat.I4;
			} else if (sender == selmapMarkFormatAuto) {
				portraitViewer1.selmapMarkFormat = null;
				portraitViewer1.useExistingAsFallback = false;
			} else if (sender == selmapMarkFormatCMPR) {
				portraitViewer1.selmapMarkFormat = WiiPixelFormat.CMPR;
			} else if (sender == selmapMarkFormatExisting) {
				portraitViewer1.selmapMarkFormat = null;
				portraitViewer1.useExistingAsFallback = true;
			}
		}
		private void frontStnameGenerationFontToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.changeFrontStnameFont();
		}

		private void snapshotPortraiticonToolStripMenuItem_Click(object sender, EventArgs e) {
			Bitmap screenshot = modelPanel1.GrabScreenshot(false);

			int size = Math.Min(screenshot.Width, screenshot.Height);
			Bitmap square = new Bitmap(size, size);
			using (Graphics g = Graphics.FromImage(square)) {
				g.DrawImage(screenshot,
					(screenshot.Width - size) / -2,
					(screenshot.Height - size) / -2);
			}

			string iconFile = TempFiles.Create(".png");
			BitmapUtilities.Resize(square, new Size(64, 56)).Save(iconFile);
			portraitViewer1.Replace(portraitViewer1.icon, iconFile);
			string prevbaseFile = TempFiles.Create(".png");
			BitmapUtilities.Resize(square, portraitViewer1.prevbaseResizeTo ?? new Size(176, 176)).Save(prevbaseFile);
			portraitViewer1.Replace(portraitViewer1.prevbase, prevbaseFile);
		}
		private void repaintIconBorderToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.repaintIconBorder();
		}
		private void updateMumenumainToolStripMenuItem_Click(object sender, EventArgs e) {
			if (songPanel1.IsInfoBarDirty()) {
				var dr = MessageBox.Show(this, "This will copy the song titles that are currently entered, including those not saved to info.pac yet. Is this OK?",
					"Note", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				if (dr == DialogResult.No) return;
			}
			string msbintmp = TempFiles.Create();
			songPanel1.ExportMSBin(msbintmp);
			portraitViewer1.updateMuMenumain(msbintmp);
		}
		private void updateScselcharacter2ToolStripMenuItem_Click(object sender, EventArgs e) {
			portraitViewer1.copyIconsToSelcharacter2();
		}
		private void addMenSelmapMarksToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenDialog.Filter = BrawlLib.FileFilters.TEX0;
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
		private void listMenSelmapMarkUsageToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutBSM(null, null) {
				AboutText = portraitViewer1.MenSelmapMarkUsageReport()
			}.ShowDialog(this);
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
		private void downgradeMenSelmapMarksToolStripMenuItem_Click(object sender, EventArgs e) {
			if (MessageBox.Show(this, "Are you sure you want to convert all IA4 MenSelmapMarks currently in use to CMPR?" +
			"This should cut their filesize in half.", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK) {
				foreach (FileInfo f in listBox1.Items) {
					int i = portraitViewer1.BestSSS.IconForStage(StageIDMap.StageIDForPac(f.Name));
					portraitViewer1.DowngradeMenSelmapMark(i);
				}
			}
		}
		private void resizeAllPrevbasesToolStripMenuItem_Click(object sender, EventArgs e) {
			var dialog = new EnterSizeDialog() {
				SizeEntry = portraitViewer1.prevbaseResizeTo ?? new Size(176, 176)
			};
			if (dialog.ShowDialog() == DialogResult.OK) {
				portraitViewer1.ResizeAllPrevbases(dialog.SizeEntry);
			}
		}
		
		private void switchPrevbaseSize(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in prevbaseSize.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == prevbaseOriginalSizeToolStripMenuItem) {
				portraitViewer1.prevbaseResizeTo = null;
			} else if (sender == x128ToolStripMenuItem) {
				portraitViewer1.prevbaseResizeTo = new Size(128, 128);
			} else if (sender == x88ToolStripMenuItem) {
				portraitViewer1.prevbaseResizeTo = new Size(88, 88);
			} else if (sender == customPrevbaseSizeToolStripMenuItem) {
				EnterSizeDialog dialog = new EnterSizeDialog();
				dialog.SizeEntry = portraitViewer1.prevbaseResizeTo ?? new Size(128, 128);
				if (dialog.ShowDialog() == DialogResult.OK) {
					portraitViewer1.prevbaseResizeTo = dialog.SizeEntry;
				}
			}
		}
		private void switchFrontstnameSize(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in frontstnameSizeToolStripMenuItem.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == frontstnameOriginalSizeToolStripMenuItem) {
				portraitViewer1.frontstnameResizeTo = null;
			} else if (sender == x56ToolStripMenuItem) {
				portraitViewer1.frontstnameResizeTo = new Size(104, 56);
			}
		}
		private void switchSelmapMarkSize(object sender, EventArgs e) {
			foreach (ToolStripMenuItem item in selmapMarkSizeToolStripMenuItem.DropDownItems) {
				item.Checked = (item == sender);
			}
			if (sender == selmapMarkOriginalSizeToolStripMenuItem) {
				portraitViewer1.selmapMarkResizeTo = null;
			} else if (sender == x56ToolStripMenuItem1) {
				portraitViewer1.selmapMarkResizeTo = new Size(104, 56);
			}
		}

		private void texturesToolStripMenuItem_Click(object sender, EventArgs e) {
			splitContainerLeft.Panel2Collapsed = !texturesToolStripMenuItem.Checked;
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutBSM(null, System.Reflection.Assembly.GetExecutingAssembly()).ShowDialog(this);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e) {
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
			} else if (e.KeyCode == Keys.OemOpenBrackets) {
				e.Handled = true;
				portraitViewer1.repaintIconBorder();
			} else if (e.KeyCode == Keys.OemCloseBrackets) {
				e.Handled = true;
				portraitViewer1.generateName();
			}
		}

		private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e) {
			songPanel1.Open(new FileInfo("../../sound/strm/" + listBoxSongs.SelectedItem + ".brstm"));
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = !saveCommon5IfNecessary();
		}
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			TempFiles.TryToDeleteAll();
		}
		#endregion

		#region registry <-> options menu
		private void SaveToRegistry() {
			new OptionsMenuSettings() {
				UseTextureConverter = portraitViewer1.useTextureConverter,
				RenderModels = renderModels.Checked,
				LoadBrstms = loadbrstmsToolStripMenuItem.Checked,
				StaticStageList = useAFixedStageListToolStripMenuItem.Checked,
				RightPanelColor = portraitViewer1.BackColor,
				ModuleFolderLocation = moduleFolderLocation,
				UseFullRelNames = useFullrelNamesToolStripMenuItem.Checked,
				SelmapMarkPreview = selmapMarkPreviewToolStripMenuItem.Checked,
				SelmapMarkFormat = selmapMarkFormatIA4.Checked ? "IA4"
								 : selmapMarkFormatI4.Checked ? "I4"
								 : selmapMarkFormatAuto.Checked ? "Auto"
								 : selmapMarkFormatCMPR.Checked ? "CMPR"
																: "Existing",
			}.SaveToRegistry();
			MessageBox.Show("Registry settings saved.");
		}

		private static void set(ToolStripMenuItem item, bool value) {
			item.Checked = !value;
			item.PerformClick();
		}

		private void LoadFromRegistry() {
			OptionsMenuSettings settings = OptionsMenuSettings.LoadFromRegistry();
			if (settings == null) {
				//MessageBox.Show("Could not load settings. They may not be present in the registry.");
				return;
			}

			set(useTextureConverterToolStripMenuItem, settings.UseTextureConverter);
			set(useAFixedStageListToolStripMenuItem, settings.StaticStageList);
			set(renderModels, settings.RenderModels);
			set(loadbrstmsToolStripMenuItem, settings.LoadBrstms);
			portraitViewer1.BackColor = settings.RightPanelColor ?? portraitViewer1.BackColor;

			{
				moduleFolderLocation = settings.ModuleFolderLocation;
				moduleToolStripMenuItem.Checked = (moduleFolderLocation == "../../module");
				sameToolStripMenuItem.Checked = (moduleFolderLocation == ".");
				if (stageInfoControl1.RelFile != null) updateRel(stageInfoControl1.RelFile.Name);
			}
			set(useFullrelNamesToolStripMenuItem, settings.UseFullRelNames);

			set(selmapMarkPreviewToolStripMenuItem, settings.SelmapMarkPreview);
			{
				string s = settings.SelmapMarkFormat;
				var menuItem = s == "IA4" ? selmapMarkFormatIA4
							 : s == "I4" ? selmapMarkFormatI4
							 : s == "Auto" ? selmapMarkFormatAuto
							 : s == "CMPR" ? selmapMarkFormatCMPR
										   : selmapMarkFormatExisting;
				set(menuItem, true);
			}
		}

		private void ClearRegistry() {
			GeneralRegistry.ClearAllStageManager();
			clearDefaultDirectoryToolStripMenuItem.Enabled = false;
			MessageBox.Show("Registry settings for BrawlStageManager have been cleared.");
		}
		#endregion
	}
}
