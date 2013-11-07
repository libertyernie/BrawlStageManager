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
		private Tuple<Image, RectangleF>[] icons;
		private int _numIcons;
		public int NumIcons {
			get {
				return _numIcons;
			}
			set {
				_numIcons = value;
				ReloadIcons();
			}
		}

		public SSSPrev() {
			InitializeComponent();
			this.ResizeRedraw = true;
			this._numIcons = 25;
			UpdateDirectory();
		}

		private static int BRAWLWIDTH = 75; // an estimate
		private static int BRAWLHEIGHT = 50; // an estimate
		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			if (icons == null) return;
			foreach (var tuple in icons) {
				e.Graphics.DrawImage(tuple.Item1,
					tuple.Item2.X * Width,
					tuple.Item2.Y * Height,
					tuple.Item2.Width * Width,
					tuple.Item2.Height * Height);
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

			ReloadIcons();
		}

		private void ReloadIcons() {
			if (sc_selmap == null) return;
			CHR0Node chr0 = sc_selmap.FindChild("MiscData[80]/AnmChr(NW4R)/MenSelmapPos_TopN__0", false) as CHR0Node;

			icons = new Tuple<Image, RectangleF>[_numIcons + 1];

			CHR0EntryNode entry = chr0.FindChild("MenSelmapPos_TopN", false) as CHR0EntryNode;
			Vector3 offset = entry.GetAnimFrame(_numIcons + 1).Translation;

			TextureContainer tc = new TextureContainer(sc_selmap, 2);
			if (tc.icon_tex0 == null) return;
			Image image = tc.icon_tex0.GetImage(0);

			for (int i = 0; i <= _numIcons; i++) {
				entry = chr0.FindChild("pos" + i.ToString("D2"), false) as CHR0EntryNode;
				AnimationFrame frame = entry.GetAnimFrame(_numIcons + 1);
				float x = (BRAWLWIDTH / 2 + frame.Translation._x + offset._x) / BRAWLWIDTH;
				float y = (BRAWLHEIGHT / 2 - frame.Translation._y - offset._y) / BRAWLHEIGHT;
				float w = 6.4f * (frame.Scale._x) / BRAWLWIDTH;
				float h = 5.6f * (frame.Scale._y) / BRAWLHEIGHT;
				RectangleF r = new RectangleF(x, y, w, h);

				//TextureContainer tc = new TextureContainer(sc_selmap, i);
				//if (tc.icon_tex0 == null) continue;
				//Image image = tc.icon_tex0.GetImage(0);

				icons[i] = new Tuple<Image, RectangleF>(image, r);
			}
			this.Invalidate();
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
