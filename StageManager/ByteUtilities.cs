using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrawlStageManager {
	public class ByteUtilities {
		public static byte[] StringToByteArray(string s) {
			char[] numbers = (from c in s
							  where char.IsLetterOrDigit(c)
							  select c).ToArray();
			byte[] b = new byte[numbers.Length / 2];
			for (int i = 0; i < b.Length; i++) {
				string num = numbers[2 * i] + "" + numbers[2 * i + 1];
				b[i] = Convert.ToByte(num, 16);
			}
			return b;
		}

		public static bool ByteArrayEquals(byte[] b1, int offset1, byte[] b2, int offset2, int length) {
			for (int subindex = 0; subindex < length; subindex++) {
				if (b1[subindex + offset1] != b2[subindex + offset2]) {
					return false;
				}
			}
			return true;
		}
	}
}
