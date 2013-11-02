﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BrawlStageManager {
	public class CustomSSS {
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

		private static byte[] SSS_HEADER = StringToByteArray("046b8f5c 7c802378");
		public CustomSSS(string[] s) {
			Regex r = new Regex(@"(\* )?[A-Fa-f0-9]{8} [A-Fa-f0-9]{8}");
			var matching_lines = 
				from line in s
				where r.IsMatch(line)
				select line;

			byte[] data = StringToByteArray(string.Join("\n", matching_lines));
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
			byte[] sss1 = new byte[sss1_count];
			Array.ConstrainedCopy(data, index, sss1, 0, sss1_count);

			index += sss1_count;
			while (index % 8 != 0) index++;

			index += 2 * 8;
			byte sss2_count = data[index - 1];
			byte[] sss2 = new byte[sss2_count];
			Array.ConstrainedCopy(data, index, sss2, 0, sss2_count);

			index += sss2_count;
			while (index % 8 != 0) index++;

			index += 1 * 8;
			byte sss3_count = data[index - 1];
			byte[] sss3 = new byte[sss3_count];
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
	}
}
