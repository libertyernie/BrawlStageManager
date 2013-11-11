using BrawlStageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSEditor {
	public class MutableSSS {
		public class StagePair {
			public byte stage;
			public byte icon;
			public override string ToString() {
				return "[0x" + stage.ToString("X2") + ", " + icon.ToString("D2") + "]";
			}
			public override int GetHashCode() {
				return stage * 0x100 + icon;
			}
			public override bool Equals(object obj) {
				return (obj is StagePair && ((StagePair)obj).GetHashCode() == this.GetHashCode());
			}
		}

		public List<StagePair> screen1;
		public List<StagePair> screen2;
		public List<StagePair> definitions;

		public MutableSSS(byte[] data) : this(new CustomSSS(data)) { }

		public MutableSSS(CustomSSS source) {
			screen1 = new List<StagePair>();
			screen2 = new List<StagePair>();
			definitions = new List<StagePair>();
			for (int i = 0; i < source.sss3.Length; i += 2) {
				definitions.Add(new StagePair {
					stage = source.sss3[i],
					icon = source.sss3[i+1],
				});
			}
			foreach (byte b in source.sss1) {
				screen1.Add(definitions[b]);
			}
			foreach (byte b in source.sss2) {
				screen2.Add(definitions[b]);
			}
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			foreach (StagePair pair in screen1) {
				sb.Append(pair.ToString() + " ");
			}
			sb.AppendLine();
			foreach (StagePair pair in screen1) {
				sb.Append(pair.ToString() + " ");
			}
			return sb.ToString();
		}
	}
}
