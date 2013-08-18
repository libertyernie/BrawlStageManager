using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BrawlStageManager {
	public class Utilities {
		public static Bitmap AlphaSwap(Bitmap source, Bitmap behind = null) {
			Bitmap ret = new Bitmap(source.Width, source.Height);
			for (int x = 0; x < ret.Width; x++) {
				for (int y = 0; y < ret.Height; y++) {
					Color fromColor = source.GetPixel(x, y);
					int toColor = fromColor.A == 0 ? -0x01000000 : fromColor.A * 0x10101 + fromColor.R * 0x1000000;
					ret.SetPixel(x, y, Color.FromArgb(toColor));
				}
			}
			if (behind != null) {
				int w = source.Width, h = source.Height;
				Bitmap both = new Bitmap(w, h);
				Graphics g = Graphics.FromImage(both);
				g.DrawImage(Invert(AlphaSwap(behind)), 0, 0, w, h);
				g.DrawImage(ret, 0, 0, w, h);
				ret = both;
			}
			return ret;
		}

		public static Bitmap Invert(Bitmap source) {
			Bitmap ret = new Bitmap(source.Width, source.Height);
			for (int x = 0; x < ret.Width; x++) {
				for (int y = 0; y < ret.Height; y++) {
					Color c = source.GetPixel(x, y);
					ret.SetPixel(x, y, Color.FromArgb(c.A, 255-c.R, 255-c.G, 255-c.B));
				}
			}
			return ret;
		}

		public static Bitmap Resize(Bitmap orig, Size resizeTo) {
			Bitmap thumbnail = new Bitmap(resizeTo.Width, resizeTo.Height);
			using (Graphics g = Graphics.FromImage(thumbnail)) {
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.DrawImage(orig, 0, 0, resizeTo.Width, resizeTo.Height);
			}
			return thumbnail;
		}

		public static bool HasAlpha(Bitmap bmp) {
			// TODO only do this if no alpha in the image
			return true;
		}

		public static Bitmap IA4toI4(Bitmap bmp) {
			int w = bmp.Width, h = bmp.Height;
			Bitmap ret = new Bitmap(w, h);
			var graphics = Graphics.FromImage(ret);
			graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, w, h);
			graphics.DrawImage(bmp, 0, 0, w, h);
			bmp.Save("test1.png");
			ret.Save("test2.png");
			return ret;
		}
	}
}
