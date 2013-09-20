using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Win32;
using System.ComponentModel;

namespace BrawlStageManager.RegistryUtilities {
	public class OptionsMenuSettings {
		private static TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));

		public bool RenderModels;
		public bool StaticStageList;
		public Color? RightPanelColor;

		public string ModuleFolderLocation;
		public bool VerifyIDs;
		public bool UseFullRelNames;

		public bool SelmapMarkPreview;
		public enum Format {
			IA4,
			I4,
			Auto,
			CMPR,
			Existing
		};
		public Format SelmapMarkFormat;

		public void SaveToRegistry() {
			RegistryKey key = Registry.CurrentUser.CreateSubKey(GeneralRegistry.SUBKEY);
			key.SetValue("RenderModels", RenderModels);
			key.SetValue("StaticStageList", StaticStageList);
			if (RightPanelColor != null) {
				key.SetValue("RightPanelColor", colorConverter.ConvertToString(RightPanelColor));
			} else {
				key.DeleteValue("RightPanelColor", false);
			}
			key.SetValue("ModuleFolderLocation", ModuleFolderLocation ?? "tjrszjtrtj");
			key.SetValue("VerifyIDs", VerifyIDs);
			key.SetValue("UseFullRelNames", UseFullRelNames);
			key.SetValue("SelmapMarkPreview", SelmapMarkPreview);
			key.SetValue("SelmapMarkFormat", SelmapMarkFormat);
		}

		private static string s(RegistryKey key, string n) {
			return key.GetValue(n).ToString();
		}

		private static bool b(RegistryKey key, string n) {
			return Boolean.Parse(s(key, n));
		}

		private static Format f(RegistryKey key, string n) {
			return (Format)Enum.Parse(typeof(Format), s(key, n));
		}

		public static OptionsMenuSettings LoadFromRegistry() {
			RegistryKey key = Registry.CurrentUser.CreateSubKey(GeneralRegistry.SUBKEY);
			OptionsMenuSettings ret = new OptionsMenuSettings();
			ret.RenderModels = b(key, "RenderModels");
			ret.StaticStageList = b(key, "StaticStageList");
			object tmp = key.GetValue("RightPanelColor");
			if (tmp != null) {
				ret.RightPanelColor = (Color)colorConverter.ConvertFromString(tmp.ToString());
			} else {
				ret.RightPanelColor = null;
			}
			ret.ModuleFolderLocation = s(key, "ModuleFolderLocation");
			ret.VerifyIDs = b(key, "VerifyIDs");
			ret.UseFullRelNames = b(key, "UseFullRelNames");
			ret.SelmapMarkPreview = b(key, "SelmapMarkPreview");
			ret.SelmapMarkFormat = f(key, "SelmapMarkFormat");
			return ret;
		}
	}
}
