using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BrawlStageManager {
	public class CustomSSS {
		private byte[] sss1, sss2, sss3;

		public Tuple<byte, byte> this[int index] {
			get {
				return new Tuple<byte, byte>(sss3[2*index], sss3[2*index + 1]);
			}
		}

		private Tuple<byte[], byte[]> _iconsInGroups;
		public Tuple<byte[], byte[]> IconsInGroups {
			get {
				if (_iconsInGroups == null) {
					byte[] b1 = new byte[sss1.Length];
					for (int i = 0; i < b1.Length; i++) {
						b1[i] = sss3[sss1[i] * 2 + 1];
					}
					byte[] b2 = new byte[sss2.Length];
					for (int i = 0; i < b2.Length; i++) {
						b2[i] = sss3[sss2[i] * 2 + 1];
					}
					_iconsInGroups = new Tuple<byte[], byte[]>(b1, b2);
				}
				return _iconsInGroups;
			}
		}

		private byte[] _stageIDsInOrder;
		public byte[] StageIDsInOrder {
			get {
				if (_stageIDsInOrder == null) {
					_stageIDsInOrder = new byte[sss1.Length + sss2.Length];
					for (int i = 0; i < sss1.Length; i++) {
						_stageIDsInOrder[i] = sss3[sss1[i] * 2];
					}
					for (int i = 0; i < sss2.Length; i++) {
						_stageIDsInOrder[sss1.Length + i] = sss3[sss2[i] * 2];
					}
				}
				return _stageIDsInOrder;
			}
		}

		public byte IconForStage(int stage_id) {
			for (int i = 0; i < sss3.Length; i += 2) {
				if (sss3[i] == stage_id) {
					return sss3[i + 1];
				}
			}
			return 0xFF;
		}

		public byte StageForIcon(int icon_id) {
			for (int i = 0; i < sss3.Length; i += 2) {
				if (sss3[i+1] == icon_id) {
					return sss3[i];
				}
			}
			return 0xFF;
		}

		private static byte[] StringToByteArray(string s) {
			char[] numbers = (from c in s
							  where char.IsLetterOrDigit(c)
							  select c).ToArray();
			byte[] b = new byte[numbers.Length / 2];
			for (int i = 0; i < b.Length; i ++) {
				string num = numbers[2*i] + "" + numbers[2*i+1];
				b[i] = Convert.ToByte(num, 16);
			}
			return b;
		}

		private static bool ByteArrayEquals(byte[] b1, int offset1, byte[] b2, int offset2, int length) {
			for (int subindex = 0; subindex < length; subindex++) {
				if (b1[subindex + offset1] != b2[subindex + offset2]) {
					return false;
				}
			}
			return true;
		}

		public CustomSSS(string[] s) {
			Regex r = new Regex(@"(\* )?[A-Fa-f0-9]{8} [A-Fa-f0-9]{8}");
			var matching_lines = 
				from line in s
				where r.IsMatch(line)
				select line;

			byte[] data = StringToByteArray(string.Join("\n", matching_lines));
			init(data);
		}

		public CustomSSS(byte[] data) {
			init(data);
		}

		private static byte[] SSS_HEADER = StringToByteArray("046b8f5c 7c802378");
		private void init(byte[] data) {
			int index = -1;
			for (int line = 0; line < data.Length; line += 8) {
				if (ByteArrayEquals(data, line, SSS_HEADER, 0, SSS_HEADER.Length)) {
					index = line;
					break;
				}
			}
			if (index == -1) {
				throw new Exception("No Custom SSS code found.");
			}

			index += 14 * 8;
			byte sss1_count = data[index - 1];
			sss1 = new byte[sss1_count];
			Array.ConstrainedCopy(data, index, sss1, 0, sss1_count);

			index += sss1_count;
			while (index % 8 != 0) index++;

			index += 2 * 8;
			byte sss2_count = data[index - 1];
			sss2 = new byte[sss2_count];
			Array.ConstrainedCopy(data, index, sss2, 0, sss2_count);

			index += sss2_count;
			while (index % 8 != 0) index++;

			index += 1 * 8;
			byte sss3_count = data[index - 1];
			sss3 = new byte[sss3_count];
			Array.ConstrainedCopy(data, index, sss3, 0, sss3_count);

			/*int i = 0;
			foreach (byte b in sss1) {
				Console.Write(b.ToString("X2"));
				if (++i % 8 == 0) Console.WriteLine();
			}
			Console.WriteLine();
			i = 0;
			foreach (byte b in sss2) {
				Console.Write(b.ToString("X2"));
				if (++i % 8 == 0) Console.WriteLine();
			}
			Console.WriteLine();
			i = 0;
			foreach (byte b in sss3) {
				Console.Write(b.ToString("X2"));
				if (++i % 8 == 0) Console.WriteLine();
			}*/
		}

		public override string ToString() {
			return String.Format("Custom SSS: {0}/{1} stages, from pool of {2} pairs",
				sss1.Length, sss2.Length, sss3.Length/2);
		}
	}
}
