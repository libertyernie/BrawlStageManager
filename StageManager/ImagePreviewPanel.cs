﻿using BrawlLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrawlStageManager {
	public class ImagePreviewPanel : Panel {

		public ImagePreviewPanel() {
			this.AllowDrop = true;
			this.ContextMenuStrip = new ContextMenuStrip();

			ToolStripMenuItem replace = new ToolStripMenuItem("Replace");
			replace.Click += new System.EventHandler(this.replace_Click);
			ToolStripMenuItem export = new ToolStripMenuItem("Export");
			export.Click += new System.EventHandler(this.export_Click);

			this.ContextMenuStrip.Items.Add(replace);
			this.ContextMenuStrip.Items.Add(export);
		}

		private PortraitViewer getPVParent() {
			Control p = Parent;
			while (p != null) {
				if (p is PortraitViewer) {
					return (PortraitViewer)p;
				} else {
					p = p.Parent;
				}
			}
			return null;
		}

		private void replace_Click(object sender, EventArgs e) {
			PortraitViewer pv = getPVParent();
			if (pv != null) {
				MainForm.OpenDialog.Filter = ExportFilters.TEX0;
				if (MainForm.OpenDialog.ShowDialog() == DialogResult.OK) {
					string fileName = MainForm.OpenDialog.FileName;
					pv.Replace(this, fileName);
				}
			}
		}

		private void export_Click(object sender, EventArgs e) {
			PortraitViewer pv = getPVParent();
			if (pv != null) {
				MainForm.SaveDialog.Filter = ExportFilters.TEX0;
				MainForm.SaveDialog.FilterIndex = 1;
				if (MainForm.SaveDialog.ShowDialog() == DialogResult.OK) {
					int fIndex = MainForm.SaveDialog.FilterIndex;

					//Fix extension
					string fileName = ApplyExtension(MainForm.SaveDialog.FileName, MainForm.SaveDialog.Filter, fIndex - 1);
					pv.GetTEX0For(this).Export(fileName);
				}
			}
		}

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

		public override string ToString() {
			return Name + " " + base.ToString();
		}
	}
}
