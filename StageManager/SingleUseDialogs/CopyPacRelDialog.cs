﻿using BrawlManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BrawlStageManager {
	public partial class CopyPacRelDialog : Form {
		public CopyPacRelDialog(string pacNew, string pacExisting, string relNew, string relExisting) {
			InitializeComponent();

			var dialog = this;
			dialog.lblPacNewName.Text = Path.GetFileName(pacNew);
			dialog.lblPacNewMD5.Text = ByteUtilities.MD5Sum(pacNew);
			dialog.lblPacExistingName.Text = Path.GetFileName(pacExisting);
			dialog.lblPacExistingMD5.Text = ByteUtilities.MD5Sum(pacExisting);
			dialog.lblRelNewName.Text = Path.GetFileName(relNew);
			dialog.lblRelNewMD5.Text = ByteUtilities.MD5Sum(relNew);
			dialog.lblRelExistingName.Text = Path.GetFileName(relExisting);
			dialog.lblRelExistingMD5.Text = ByteUtilities.MD5Sum(relExisting);

			if (dialog.lblPacNewMD5.Text == dialog.lblPacExistingMD5.Text) {
				dialog.lblPacExistingMD5.ForeColor = dialog.lblPacNewMD5.ForeColor = Color.Green;
			}
			if (dialog.lblRelNewMD5.Text == dialog.lblRelExistingMD5.Text) {
				dialog.lblRelExistingMD5.ForeColor = dialog.lblRelNewMD5.ForeColor = Color.Green;
			}
			if (dialog.lblRelExistingMD5.Text.StartsWith("No", StringComparison.InvariantCultureIgnoreCase)) {
				dialog.lblRelExistingMD5.ForeColor = Color.Red;
			}
			if (dialog.lblRelNewMD5.Text.StartsWith("No", StringComparison.InvariantCultureIgnoreCase)) {
				dialog.lblRelNewMD5.ForeColor = Color.Red;
			}
		}
	}
}
