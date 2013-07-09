using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BrawlStageManager {
	public class TempFiles {
		private static Stack<string> tempFiles = new Stack<string>();

		public static string Create() {
			string file = Path.GetTempFileName();
			tempFiles.Push(file);
			return file;
		}

		public static string Create(string extension) {
			string from = Path.GetTempFileName();
			string file = from + extension;
			File.Move(from, file);
			tempFiles.Push(file);
			return file;
		}

		public static void TryToDeleteAll() {
			while (tempFiles.Any()) {
				string s = tempFiles.Pop();
				try {
					File.Delete(s);
				} catch (Exception e) {
					Console.WriteLine(s + ": " + e.Message);
				}
			}
		}
	}
}
