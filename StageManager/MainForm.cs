using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BrawlLib.SSBB.ResourceNodes;
using System.IO;

namespace BrawlStageManager {
	public partial class MainForm : Form {
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

			changeDirectory(path);
		}

		private void open(FileInfo fi) {
			RightControl = loadingLabel;
			this.Refresh();
			if (_rootNode != null) {
				_rootNode.Dispose(); _rootNode = null;
			}
			if (fi == null) { // No .pac file selected (i.e. you just opened the program)
				RightControl = chooseLabel;
				return;
			}
			try {
				fi.Refresh(); // Update file size
				_rootNode = NodeFactory.FromFile(null, _rootPath = fi.FullName);
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

				string relname = matchRel(fi.Name);
				updateRel(relname);

				bool RenderModels = true;
				if (RenderModels) modelPanel1.ClearAll();
				List<ResourceNode> allNodes = _rootNode.FindChild("2", false).Children; // Find all child nodes of "2"
				List<MSBinNode> msBinNodes = new List<MSBinNode>();
				texNodes = new List<MDL0TextureNode>();
				foreach (ResourceNode node in allNodes) {
					Console.WriteLine(node);
					if (node.ResourceType == ResourceType.MSBin) {
						msBinNodes.Add((MSBinNode)node); // This is an MSBin node - add it to the list
					} else if (RenderModels) {
						ResourceNode modelfolder = node.FindChild("3DModels(NW4R)", false);
						if (modelfolder != null) {
							foreach (ResourceNode child in modelfolder.Children) {
								if (child is MDL0Node) {
									MDL0Node model = child as MDL0Node;
									model._renderBones = false;
									model._renderPolygons = CheckState.Checked;
									model._renderVertices = false;
									model._renderBox = false;
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
				if (RenderModels) {
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
		private int q = 0;

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
			if (pacFiles.Length == 0 && path.Parent == null) {
				DirectoryInfo search = new DirectoryInfo(path.FullName + "private\\wii\\app\\RSBE\\pf\\stage\\melee");
				if (search.Exists) {
					changeDirectory(search); // Change to the typical stage folder used by the FPC, if it exists on the drive
				}
			}

			Array.Sort(pacFiles, delegate(FileInfo f1, FileInfo f2) {
				return f1.Name.ToLower().CompareTo(f2.Name.ToLower()); // Sort by filename, case-insensitive
			});
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
					} else if (_rootNode != null) { // The sender must be on the right - modify an existing stage/module; ignore if no stage is selected
						if (filename.EndsWith(".pac") || filename.EndsWith(".rel")) {
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
			} else if (_rootNode != null) {
				name = new FileInfo(_rootPath).Name;
				_rootNode.Dispose(); _rootNode = null; // Close the file before overwriting it!
				if (filepath.EndsWith(".pac")) {
					FileOperations.Copy(filepath, CurrentDirectory + "\\" + name);
				} else if (filepath.EndsWith(".rel")) {
					FileOperations.Copy(filepath, stageInfoControl1.RelFile.FullName);
				}
				listBox1_SelectedIndexChanged(null, null);
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
			open((FileInfo)listBox1.SelectedItem);
		}

		private void changeDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
//			fbd.SelectedPath = CurrentDirectory; // Uncomment this if you want the "change directory" dialog to start with the current directory selected
			if (fbd.ShowDialog() == DialogResult.OK) {
				changeDirectory(fbd.SelectedPath);
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

	}
}
