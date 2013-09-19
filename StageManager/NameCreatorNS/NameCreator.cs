using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrawlStageManager.NameCreatorNS {
	public class NameCreatorSettings {
		public Font Font;
		public int VerticalOffset;
		public override string ToString() {
			return String.Format("{0}pt {1} {2} ({3})", Font.SizeInPoints, Font.Name, Font.Style, VerticalOffset);
		}
	}

	public class NameCreator {
		public static NameCreatorSettings selectFont(NameCreatorSettings previous = null) {
			using (NameCreatorDialog d = new NameCreatorDialog()) {
				if (previous != null) d.Settings = previous;
				if (d.ShowDialog() == DialogResult.OK) {
					return d.Settings;
				} else {
					return null;
				}
			}
		}

		public static Bitmap createImage(NameCreatorSettings fontData, string text) {
			Bitmap b = new Bitmap(208, 56);
			Graphics g = Graphics.FromImage(b);
			g.FillRectangle(new SolidBrush(Color.Black), 0, 0, 208, 56);
			g.DrawString(text.Replace("\\n", "\n"), fontData.Font, new SolidBrush(Color.White), 104, 28 - fontData.VerticalOffset, new StringFormat() {
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center,
			});
			return b;
		}
	}
}
