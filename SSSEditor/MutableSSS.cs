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
			public ushort ToUshort() {
				return (ushort)(stage * 0x100 + icon);
			}
			public override int GetHashCode() {
				return ToUshort();
			}
			public override bool Equals(object obj) {
				return (obj is StagePair && ((StagePair)obj).ToUshort() == this.ToUshort());
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

		#region Conversion to code text
		private string ToCodeLines(List<StagePair> list) {
			StringBuilder sb = new StringBuilder();
			if (list != definitions) {
				byte[] b = (from sp in list
							select (byte)definitions.IndexOf(sp)).ToArray();
				for (int i = 0; i < b.Length; i += 8) {
					sb.Append("* ");
					for (int j = i; j < i + 4; j++) {
						sb.Append(j < b.Length ? b[j].ToString("X2") : "00");
					}
					sb.Append(" ");
					for (int j = i + 4; j < i + 8; j++) {
						sb.Append(j < b.Length ? b[j].ToString("X2") : "00");
					}
					sb.AppendLine();
				}
			} else {
				for (int i = 0; i < definitions.Count; i += 4) {
					sb.Append("* ");
					for (int j = i; j < i + 2; j++) {
						sb.Append(j < definitions.Count ? definitions[j].ToUshort().ToString("X4") : "0000");
					}
					sb.Append(" ");
					for (int j = i + 2; j < i + 4; j++) {
						sb.Append(j < definitions.Count ? definitions[j].ToUshort().ToString("X4") : "0000");
					}
					sb.AppendLine();
				}
			}
			return sb.ToString();
		}

		public string ToCode() {
			return String.Format(
@"* 046B8F5C 7C802378
* 046B8F64 7C6300AE
* 040AF618 5460083C
* 040AF68C 38840002
* 040AF6AC 5463083C
* 040AF6C0 88030001
* 040AF6E8 3860FFFF
* 040AF59C 3860000C
* 060B91C8 00000018
* BFA10014 7CDF3378
* 7CBE2B78 7C7D1B78
* 2D05FFFF 418A0014
* 006B929C 000000{0}
* 066B99D8 000000{0}
{1}* 006B92A4 000000{2}
* 066B9A58 000000{2}
{3}* 06407AAC 000000{4}
{5}",
	screen1.Count.ToString("X2"), ToCodeLines(screen1),
	screen2.Count.ToString("X2"), ToCodeLines(screen2),
	definitions.Count.ToString("X2"), ToCodeLines(definitions));
		}
		#endregion
	}
}
