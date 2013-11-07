using BrawlLib.SSBB.ResourceNodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BrawlStageManager {
	public static class IconsToMenumain {
		public static void Copy(ResourceNode scSelmap, ResourceNode muMenumain) {
			ResourceNode miscData0 = muMenumain.FindChild("MiscData[0]", false);
			List<ResourceNode> chrToKeep = miscData0.FindChild("AnmChr(NW4R)", false).Children;
			Dictionary<string, string> tempFiles = new Dictionary<string, string>(chrToKeep.Count);
			foreach (ResourceNode n in chrToKeep) {
				string file = TempFiles.Create(".chr0");
				tempFiles.Add(n.Name, file);
				n.Export(file);
			}

			ResourceNode miscData80 = scSelmap.FindChild("MiscData[80]", false);
			string file80 = TempFiles.Create(".brres");
			miscData80.Export(file80);

			miscData0.Replace(file80);
			List<ResourceNode> chrToReplace = miscData0.FindChild("AnmChr(NW4R)", false).Children;
			foreach (ResourceNode n in chrToReplace) {
				string file = tempFiles[n.Name];
				n.Replace(file);
			}

			ResourceNode xx = miscData0.FindChild("Textures(NW4R)/MenSelmapIcon.XX", false);
			if (xx != null) {
				string tempfile = TempFiles.Create(".png");
				xx.Export(tempfile);
				foreach (ResourceNode tex in miscData0.FindChild("Textures(NW4R)", false).Children) {
					byte icon_id;
					if (tex.Name.StartsWith("MenSelmapIcon.") && Byte.TryParse(tex.Name.Substring(14, 2), out icon_id)) {
						byte stage_id = StageIDMap.BestSSS.StageForIcon(icon_id);
						if (icon_id != 100 && (stage_id == 25 || stage_id > 0x33)) {
							tex.Replace(tempfile);
						}
					}
				}
				File.Delete(tempfile);
			}
		}
	}
}
