using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BrawlLib.SSBB.ResourceNodes;

namespace BrawlStageManager {
	public partial class StageInfoControl : UserControl {
		// _relFile should only be null on startup, when no stage is selected.
		// It's OK to have it pointing to a file that doesn't exist.
		private FileInfo _relFile;
		// The PacFileDeletion delegate is used to delete .pac files from within MainForm.
		public delegate void PacFileDeletionType();
		public PacFileDeletionType PacFileDeletion;

		private bool _useRelDescription;

		public void setStageLabels(string v0, string v1, string v2) {
			stageFilename.Text = v0;
			stageName.Text = v1;
			stageInfo.Text = v2;
		}

		private void setRelLabels(string v0, string v1, string v2) {
			relFilename.Text = v0;
			relName.Text = v1;
			relInfo.Text = v2;
		}

		public FileInfo RelFile {
			get {
				return _relFile;
			}
			set {
				_relFile = value;
				refreshRelFile();
			}
		}

		public bool UseRelDescription {
			get {
				return _useRelDescription;
			}
			set {
				_useRelDescription = value;
				if (_relFile != null) {
					refreshRelFile();
				}
			}
		}

		public StageInfoControl() {
			InitializeComponent();
			setStageLabels("", "", "");
			refreshRelFile();
		}

		private string getModuleName(FileInfo relFile) {
			FileStream stream = relFile.OpenRead();

			stream.Seek(3, SeekOrigin.Begin);
			int id = stream.ReadByte();

			stream.Close();
			/*return sb.ToString();*/
			string name = ModuleIDs.getName(id);
			if (UseRelDescription) foreach (StageModuleConverter.Stage s in StageModuleConverter.StageList) {
				if (s.Filename == name + ".rel") {
					name = s.Name;
					break;
				}
			}
			return name;
		}

		private void refreshRelFile() {
			if (_relFile == null) { // When no stage .pac has been selected
				setRelLabels("", "", "");
				relButton.Text = "N/A";
				relButton.Enabled = false;
			} else {
				if (_relFile.Exists) { // If there is a rel, display the filename/name/size (this is done for .pac files in MainForm)
					_relFile.Refresh();
					setRelLabels(_relFile.Name + ":", getModuleName(_relFile), "(" + _relFile.Length + " bytes)");
					relFilename.ForeColor = Color.Black;
					verifyIDs();
					relButton.Enabled = true;
				} else {
					setRelLabels(_relFile.Name + " (doesn't exist)", "", "");
					relButton.BackColor = Control.DefaultBackColor;
					relFilename.ForeColor = Color.Gray;
					relButton.Text = "N/A";
					relButton.Enabled = false;
				}
			}
		}

		#region Stage ID verification
		private static int getIdealStageID(string filename) {
			if (filename.StartsWith("st_custom")) {
				return 0;
			}
			foreach (StageModuleConverter.Stage s in StageModuleConverter.StageList) {
				if (s.Filename == filename) {
					return s.ID;
				}
			}
			return -1;
		}

		private static int getCurrentStageID(FileInfo relFile) {
			return stageIDScan(relFile, false);
		}

		private static int stageIDScan(FileInfo relFile, bool fix) {
			FileStream stream;
			stream = relFile.Open(FileMode.Open, FileAccess.ReadWrite);

			// search through pointer
			long length = relFile.Length;
			byte[] searchFor = { 0x38, 0xa5, 0x00, 0x00, 0x38, 0x80, 0x00 };
			int indexToCheck = 0;
			bool found = false;

			int i = 0;
			while (!found && i < length) {
				if (stream.ReadByte() == searchFor[indexToCheck]) {
					indexToCheck++;
					if (indexToCheck == searchFor.Length)
						if (StageModuleConverter.IndicesToIgnore.Contains(i + 1)) {
							//MessageBox.Show("ignored " + (i + 1));
							indexToCheck = 0;
						} else {
							found = true;
							if (fix) {
								stream.WriteByte((byte)getIdealStageID(relFile.Name));
							}
						}
				} else
					indexToCheck = 0;
				i++;
			}

			int b = stream.ReadByte();
			stream.Close();
			return b;
		}

		private void verifyIDs() {
			if (_relFile == null) return;
			int currentID = getCurrentStageID(_relFile);
			int idealID = getIdealStageID(_relFile.Name);
			relButton.Text = currentID.ToString("X2");
			if (currentID == idealID) {
				relButton.BackColor = Control.DefaultBackColor;
			} else if (idealID == -1) {
				relButton.BackColor = Color.Beige;
			} else {
				relButton.BackColor = Color.Red;
			}
		}
		#endregion

		private void relButton_Click(object sender, EventArgs e) {
			fixStageIDAutomaticallyToolStripMenuItem_Click(sender, e);
		}

		private void fixStageIDAutomaticallyToolStripMenuItem_Click(object sender, EventArgs e) {
			if (_relFile != null && _relFile.Exists) {
				stageIDScan(_relFile, true);
			}
			refreshRelFile();
		}

		private void relButton_changeID_Click(object sender, EventArgs e) {
			using (StageModuleConverter dlg = new StageModuleConverter()) {
				dlg.Path = _relFile.FullName;
				if (dlg.ShowDialog() == DialogResult.OK) {
					using (FileStream output = _relFile.OpenWrite()) {
						output.Write(dlg.Data, 0, dlg.Data.Length);
					}
				}
			}
			refreshRelFile();
		}

		private void relButton_deleteRelFile_Click(object sender, EventArgs e) {
			FileOperations.Delete(_relFile.FullName);
			RelFile = new FileInfo(_relFile.FullName);
		}

		private void relButton_deletePacFile_Click(object sender, EventArgs e) {
			PacFileDeletion();
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
			relButton_changeID.Enabled = relButton_deleteRelFile.Enabled = (_relFile != null && _relFile.Exists);
		}
	}
}
