using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrawlLib.Wii.Animations;
using BrawlLib.SSBB.ResourceNodes;
using System.IO;
using BrawlStageManager;

namespace SSSPreview {
	public partial class SSSPrev : UserControl {
		private ResourceNode common5, sc_selmap;
		private AnimationFrame offset;
		private RectangleF[] iconPos;

		public SSSPrev() {
			InitializeComponent();
			this.ResizeRedraw = true;
			UpdateDirectory();

			if (sc_selmap != null) {
				CHR0Node chr0 = sc_selmap.FindChild("MiscData[80]/AnmChr(NW4R)/MenSelmapPos_TopN__0", false) as CHR0Node;

				int numIcons = 25;
				iconPos = new RectangleF[numIcons + 1];

				CHR0EntryNode entry = chr0.FindChild("MenSelmapPos_TopN", false) as CHR0EntryNode;
				offset = entry.GetAnimFrame(numIcons + 1);

				for (int i = 0; i <= numIcons; i++) {
					entry = chr0.FindChild("pos" + i.ToString("D2"), false) as CHR0EntryNode;
					AnimationFrame frame = entry.GetAnimFrame(numIcons + 1);
					float x = (BRAWLWIDTH / 2 + frame.Translation._x + offset.Translation._x) / BRAWLWIDTH;
					float y = (BRAWLHEIGHT / 2 - frame.Translation._y - offset.Translation._y) / BRAWLHEIGHT;
					float w = 6.4f * (frame.Scale._x) / BRAWLWIDTH;
					float h = 5.6f * (frame.Scale._y) / BRAWLHEIGHT;
					iconPos[i] = new RectangleF(x, y, w, h);
				}
			}
		}

		private static int BRAWLWIDTH = 75; // an estimate
		private static int BRAWLHEIGHT = 50; // an estimate
		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			if (iconPos == null) return;
			//Pen pen = new Pen(new SolidBrush(Color.Blue));
			for (int i = 1; i < iconPos.Length; i++) {
				TextureContainer tc = new TextureContainer(sc_selmap, i);
				if (tc.icon_tex0 == null) continue;
				Image image = tc.icon_tex0.GetImage(0);

				RectangleF r = iconPos[i];
				e.Graphics.DrawImage(image,
					r.X * Width,
					r.Y * Height,
					r.Width * Width,
					r.Height * Height);
			}
		}

		public void UpdateDirectory() {
			Console.WriteLine(System.Environment.CurrentDirectory);
			if (sc_selmap != null) sc_selmap.Dispose();
			if (common5 != null) common5.Dispose();
			if (File.Exists("menu2/sc_selmap.pac")) {
				common5 = null;
				sc_selmap = fcopy("menu2/sc_selmap.pac");
			} else if (File.Exists("menu2/sc_selmap_en.pac")) {
				common5 = null;
				sc_selmap = fcopy("/menu2/sc_selmap_en.pac");
			} else if (File.Exists("system/common5.pac")) {
				common5 = fcopy("system/common5.pac");
				sc_selmap = common5.FindChild("sc_selmap_en", false);
			} else if (File.Exists("system/common5_en.pac")) {
				common5 = fcopy("system/common5_en.pac");
				sc_selmap = common5.FindChild("sc_selmap_en", false);
			} else {
				common5 = null;
				sc_selmap = null;
			}
		}

		private static ResourceNode fcopy(string path) {
			FileInfo f = new FileInfo(path);
			if (!f.Exists) throw new IOException(f.FullName + " doesn't exist");

			string tempfile = TempFiles.Create();
			File.Copy(f.FullName, tempfile, true);
			return NodeFactory.FromFile(null, tempfile);
		}
	}
}
