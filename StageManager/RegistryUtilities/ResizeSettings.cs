using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrawlStageManager.RegistryUtilities {
	public class ResizeSettings {
		private const string SUBKEY = "SOFTWARE\\libertyernie\\BrawlStageManager";

		public static bool WriteToRegistry(PortraitViewer pv) {
			return WriteToRegistry(pv.prevbaseResizeTo, pv.frontstnameResizeTo, pv.selmapMarkResizeTo);
		}

		public static bool WriteToRegistry(Size? prevbase, Size? frontstname, Size? selmapMark) {
			Set("Prevbase", prevbase);
			Set("FrontStname", frontstname);
			Set("SelmapMark", selmapMark);
			if ((prevbase ?? frontstname ?? selmapMark) == null) {
				MessageBox.Show("The auto-resize settings have been cleared (all three set to \"Off.\")");
				return false;
			} else {
				MessageBox.Show("The default texture sizes have been set in HKEY_CURRENT_USER.");
				return true;
			}
		}

		private static void Set(string texname, Size? size) {
			RegistryKey key = Registry.CurrentUser.CreateSubKey(SUBKEY);
			if (size != null) {
				key.SetValue(texname + "Width", size.Value.Width);
				key.SetValue(texname + "Height", size.Value.Height);
			} else {
				key.DeleteValue(texname + "Width", false);
				key.DeleteValue(texname + "Height", false);
			}
		}

		private static Size? Get(string texname) {
			RegistryKey key = Registry.CurrentUser.CreateSubKey(SUBKEY);
			object w = key.GetValue(texname + "Width");
			object h = key.GetValue(texname + "Height");
			if (w == null || h == null) {
				return null;
			}
			return new Size(Int32.Parse(w.ToString()), Int32.Parse(h.ToString()));
		}

		public static Size?[] Get() {
			return new Size?[] { Get("Prevbase"), Get("FrontStname"), Get("SelmapMark") };
		}

		public static void Clear() {
			WriteToRegistry(null, null, null);
		}
	}
}
