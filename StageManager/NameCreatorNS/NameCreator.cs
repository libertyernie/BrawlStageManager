using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrawlStageManager.NameCreatorNS {
	public class NameCreator {
		public class Settings {
			public Font Font;
			public int VerticalOffset;
		}

		public static Settings selectFont(Font defaultFont = null) {
			using (NameCreatorDialog d = new NameCreatorDialog()) {
				if (defaultFont != null) d.Font = defaultFont;
				if (d.ShowDialog() == DialogResult.OK) {
					Console.WriteLine(d.SelectedFont);
					return new Settings {
						Font = d.SelectedFont,
						VerticalOffset = d.VerticalOffset,
					};
				} else {
					return null;
				}
			}
		}

		public static Bitmap createImage(Settings fontData, string text) {
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
