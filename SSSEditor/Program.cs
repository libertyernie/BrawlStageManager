using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSSEditor {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            string gct = args.Length > 0 ? args[0] : "F:\\codes\\RSBE01.gct";
            string pac = args.Length > 1 ? args[1] : @"F:\private\wii\app\RSBE\pf\system\common5.pac";

			Application.Run(new SSSEditorForm(gct, pac));
		}
	}
}
