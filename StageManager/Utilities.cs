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

		public static Bitmap Combine(Bitmap bg, Bitmap fg) {
			int w = fg.Width, h = fg.Height;
			Bitmap both = new Bitmap(w, h);
			Graphics g = Graphics.FromImage(both);
			g.DrawImage(bg, 0, 0, w, h);
			g.DrawImage(fg, 0, 0, w, h);
			return both;
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
				Color c;
				if (IsSolidColor(orig, out c)) {
					g.FillRectangle(new SolidBrush(c), 0, 0, resizeTo.Width, resizeTo.Height);
				} else {
					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					g.DrawImage(orig, 0, 0, resizeTo.Width, resizeTo.Height);
				}
			}
			return thumbnail;
		}

		public static bool IsSolidColor(Bitmap bmp, out Color c) {
			c = bmp.GetPixel(0, 0);
			for (int y = 0; y < bmp.Height; y++) {
				for (int x = 0; x < bmp.Width; x++) {
					if (bmp.GetPixel(x, y) != c) {
						return false;
					}
				}
			}
			return true;
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
