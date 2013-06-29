using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BrawlStageManager {
	public class Utilities {
		public static Bitmap AlphaSwap(Bitmap source) {
			Bitmap ret = new Bitmap(source.Width, source.Height);
			for (int x = 0; x < ret.Width; x++) {
				for (int y = 0; y < ret.Height; y++) {
					Color fromColor = source.GetPixel(x, y);
					int toColor = fromColor.A == 0 ? -0x01000000 : fromColor.A * 0x10101 + fromColor.R * 0x1000000;
					ret.SetPixel(x, y, Color.FromArgb(toColor));
				}
			}
			return ret;
		}
	}
}
