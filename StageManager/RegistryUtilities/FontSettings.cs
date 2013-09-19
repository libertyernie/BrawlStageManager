using BrawlStageManager.NameCreatorNS;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BrawlStageManager.RegistryUtilities {
	public class FontSettings {
		private const string SUBKEY = "SOFTWARE\\libertyernie\\BrawlStageManager";
		private static TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));

		public static string WriteToRegistry(NameCreator.Settings settings) {
			if (settings == null) {
				Clear();
				return null;
			}
			string str = converter.ConvertToString(settings.Font);

			RegistryKey key = Registry.CurrentUser.CreateSubKey(SUBKEY);
			key.SetValue("FrontStnameFont", str);
			key.SetValue("FrontStnameVerticalOffset", settings.VerticalOffset);
			return str;
		}

		public static NameCreator.Settings Get() {
			RegistryKey key = Registry.CurrentUser.CreateSubKey(SUBKEY);
			object fontobj = key.GetValue("FrontStnameFont");
			object voobj = key.GetValue("FrontStnameVerticalOffset");
			if (voobj == null || fontobj == null) {
				return null;
			}
			int offset = Int32.Parse(voobj.ToString());
			return new NameCreator.Settings() {
				Font = (Font)converter.ConvertFromString(fontobj.ToString()),
				VerticalOffset = offset,
			};
		}

		private static void Clear() {
			RegistryKey key = Registry.CurrentUser.CreateSubKey(SUBKEY);
			key.DeleteValue("FrontStnameFont", false);
			key.DeleteValue("FrontStnameVerticalOffset", false);
		}
	}
}
