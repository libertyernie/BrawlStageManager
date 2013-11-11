using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrawlStageManager;

namespace SSSEditor {
	public static class Static {
		public static List<KeyValuePair<byte, string>> StagesByID;

		static Static() {
			StagesByID = new List<KeyValuePair<byte,string>>();
			for (byte b = 1; b <= 100; b++) {
				string pac = StageIDMap.PacBasenameForStageID(b);
				if (pac != null) StagesByID.Add(new KeyValuePair<byte, string>(b, pac));
			}
		}
	}
}
