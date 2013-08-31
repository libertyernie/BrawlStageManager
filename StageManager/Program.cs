using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using BrawlLib.SSBB.ResourceNodes;
using System.Diagnostics;
using Microsoft.Win32;

namespace BrawlStageManager {
	public static class Program {
		private static MainForm form;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			if (args.Length > 0) if (args[0] == "--help" || args[0] == "/c") {
				Console.WriteLine(BSMHelp());
				return;
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			string dir = null;
			bool shouldVerifyIDs = true, useRelDescription = true;
			foreach (string arg in args) {
				if (arg == "/v") {
					shouldVerifyIDs = true;
				} else if (arg == "/V") {
					shouldVerifyIDs = false;
				} else if (arg == "/d") {
					useRelDescription = true;
				} else if (arg == "/D") {
					useRelDescription = false;
				} else if (new DirectoryInfo(arg).Exists) {
					dir = arg;
				}
			}
			if (dir == null) {
				dir = (string)Registry.CurrentUser.CreateSubKey("SOFTWARE\\libertyernie\\BrawlStageManager").GetValue("LastDirectory")
					?? System.IO.Directory.GetCurrentDirectory();
			}
			form = new MainForm(dir, shouldVerifyIDs, useRelDescription);
			Application.Run(form);
		}

		private static string BSMHelp() {
			return "Usage: " + Process.GetCurrentProcess().ProcessName + " [args] [path to stage/melee folder]\n" +
				"\n" +
				"Arguments:\n" +
				"  /v  Verify .rel stage IDs (default)\n" +
				"  /V  Don't bother to verify .rel stage IDs\n" +
				"  /d  Show .rel description (default)\n" +
				"  /D  Show .rel original filename";
		}
	}
}
