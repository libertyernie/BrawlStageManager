﻿using BrawlLib.SSBB.ResourceNodes;
using BrawlLib.Wii.Textures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrawlStageManager {
	public partial class ModifyPAT0Dialog : Form {
		private TextureContainer textures;

		public ModifyPAT0Dialog(TextureContainer tx) {
			InitializeComponent();
			textures = tx;
			AcceptButton = btnOkay;

			if (textures.seriesicon_pat0 == null) {
				selchrBox.Enabled = false;
			} else {
				var i4 = from tex in textures.TEX0Folder.Children
						 where tex is TEX0Node && ((TEX0Node)tex).Format == WiiPixelFormat.I4
						 orderby tex.Name
						 select tex.Name;
				selchrBox.DataSource = i4.ToList();
				if (textures.seriesicon_tex0 != null) selchrBox.SelectedItem = textures.seriesicon_tex0.Name;
				selchrBox.Enabled = true;
			}

			if (textures.selmap_mark_pat0 == null) {
				selmapBox.Enabled = false;
			} else {
				var ia4 = from tex in textures.TEX0Folder.Children
						  where tex is TEX0Node && ((TEX0Node)tex).Format == WiiPixelFormat.IA4
						  orderby tex.Name
						  select tex.Name;
				selmapBox.DataSource = ia4.ToList();
				if (textures.selmap_mark_tex0 != null) selmapBox.SelectedItem = textures.selmap_mark_tex0.Name;
				selmapBox.Enabled = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			//DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOkay_Click(object sender, EventArgs e) {
			//DialogResult = DialogResult.OK;

			if (selchrBox.Enabled && selchrBox.SelectedItem != null && selchrBox.SelectedItem.ToString() != textures.seriesicon_pat0.Texture) {
				textures.seriesicon_pat0.Texture = selchrBox.SelectedItem.ToString();
				textures.seriesicon_pat0.IsDirty = true;
			}
			if (selmapBox.Enabled && selmapBox.SelectedItem != null && selmapBox.SelectedItem.ToString() != textures.selmap_mark_pat0.Texture) {
				textures.selmap_mark_pat0.Texture = selmapBox.SelectedItem.ToString();
				textures.selmap_mark_pat0.IsDirty = true;
			}

			Close();
		}
	}
}