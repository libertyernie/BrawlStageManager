using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BrawlStageManager {
	public class StageIDMap {
		#region Definition of "Stage" inner class
		public class Stage {
			public byte ID { get; private set; }
			public string Name { get; private set; }
			public string RelName { get; private set; }
			public string PacBasename { get; private set; }

			#region Finding .pac names - for the stage sorter in MainForm and the MenSelmapMark report
			public string[] PacNames {
				get {
					string s = PacBasename;
					return  s == "starfox" ? new string[] { "STGSTARFOX_GDIFF.pac" } :
							s == "emblem" ? new string[] {
								"STGEMBLEM_00.pac",
								"STGEMBLEM_01.pac",
								"STGEMBLEM_02.pac" } :
							s == "mariopast" ? new string[] {
								"STGMARIOPAST_00.pac",
								"STGMARIOPAST_01.pac" } :
							s == "metalgear" ? new string[] {
								"STGMETALGEAR_00.pac",
								"STGMETALGEAR_01.pac",
								"STGMETALGEAR_02.pac" } :
							s == "tengan" ? new string[] {
								"STGTENGAN_1.pac",
								"STGTENGAN_2.pac",
								"STGTENGAN_3.pac" } :
							s == "village" ? new string[] {
								"STGVILLAGE_00.pac",
								"STGVILLAGE_01.pac",
								"STGVILLAGE_02.pac",
								"STGVILLAGE_03.pac",
								"STGVILLAGE_04.pac" } :
							s == "custom" ? new string[0] :
							new string[] { "STG" + s.ToUpper() + ".pac" };
				}
			}
			private static string[] from(string basename, int start, int count) {
				return (from i in Enumerable.Range(start, count) select "STG" + basename + i.ToString("X2") + ".pac").ToArray();
			}
			#endregion

			public bool ContainsPac(string filename) {
				return filename.ToLower().Contains(PacBasename.ToLower());
			}

			public Stage(byte id, string name, string relname, string pac_basename) {
				this.ID = id;
				this.Name = name;
				this.RelName = relname;
				this.PacBasename = pac_basename;
			}

			public override string ToString() { return Name; }
		}
		#endregion

		private static List<Stage> stageList = new List<Stage>();
		private static List<Tuple<int, int>> sss;
		static StageIDMap() {
			// static initializer
			#region Arrays containing stage data
			string[] relnames = {"st_custom##.rel",
				"st_battle.rel", "st_final.rel",
				"st_dolpic.rel", "st_mansion.rel", "st_mariopast.rel",
				"st_kart.rel", "st_donkey.rel", "st_jungle.rel",
				"st_pirates.rel", "st_norfair.rel", "st_orpheon.rel",
				"st_crayon.rel", "st_halberd.rel", "st_starfox.rel",
				"st_stadium.rel", "st_tengan.rel", "st_fzero.rel",
				"st_ice.rel", "st_gw.rel", "st_emblem.rel",
				"st_madein.rel", "st_earth.rel", "st_palutena.rel",
				"st_famicom.rel", "st_newpork.rel", "st_village.rel",
				"st_metalgear.rel", "st_greenhill.rel", "st_pictchat.rel",
				"st_plankton.rel", "st_config.rel", "st_dxshrine.rel",
				"st_dxyorster.rel", "st_dxgarden.rel", "st_dxonett.rel",
				"st_dxgreens.rel", "st_dxpstadium.rel", "st_dxrcruise.rel",
				"st_dxcorneria.rel", "st_dxbigblue.rel", "st_dxzebes.rel",
				"st_oldin.rel", "st_homerun.rel", "st_stageedit.rel",
				"st_heal.rel", "st_otrain.rel", "st_tbreak.rel"};
			string[] pac_basenames = {"custom",
				"battlefield", "final",
				"dolpic", "mansion", "mariopast",
				"kart", "donkey", "jungle",
				"pirates", "norfair", "orpheon",
				"crayon", "halberd", "starfox",
				"stadium", "tengan", "fzero",
				"ice", "gw", "emblem",
				"madein", "earth", "palutena",
				"famicom", "newpork", "village",
				"metalgear", "greenhill", "pictchat",
				"plankton", "configtest", "dxshrine",
				"dxyorster", "dxgarden", "dxonett",
				"dxgreens", "dxpstadium", "dxrcruise",
				"dxcorneria", "dxbigblue", "dxzebes",
				"oldin", "homerun", "edit",
				"heal", "onlinetraining", "targetlv"};
			string[] stagenames = {"STGCUSTOM##.pac", "Battlefield",
				"Final Destination", "Delfino Plaza", "Luigi's Mansion",
				"Mushroomy Kingdom", "Mario Circuit", "75 m",
				"Rumble Falls", "Pirate Ship", "Norfair",
				"Frigate Orpheon", "Yoshi's Island (Brawl)", "Halberd",
				"Lylat Cruise", "Pokemon Stadium 2", "Spear Pillar",
				"Port Town Aero Dive", "Summit", "Flat Zone 2",
				"Castle Siege", "WarioWare Inc.", "Distant Planet",
				"Skyworld", "Mario Bros.", "New Pork City", "Smashville",
				"Shadow Moses Island", "Green Hill Zone", "PictoChat",
				"Hanenbow", "ConfigTest", "Temple",
				"Yoshi's Island (Melee)", "Jungle Japes", "Onett",
				"Green Greens", "Pokemon Stadium", "Rainbow Cruise",
				"Corneria", "Big Blue", "Brinstar", "Bridge of Eldin",
				"Homerun", "Edit", "Heal", "Online Training",
				"TargetBreak"};
			byte[] ids = {0,1,2,3,4,5,6,7,
				8,9,11,12,13,14,19,20,21,
				22,23,24,25,28,29,30,31,32,33,
				34,35,36,37,38,41,
				42,43,44,45,46,47,48,49,50,51,
				52,53,54,55,56};
			#endregion
			for (int i = 0; i < ids.Length; i++)
				stageList.Add(new Stage(ids[i], stagenames[i], relnames[i], pac_basenames[i]));

			string s = @"Classic Expansion SSS v5.3
* 046B8F5C 7C802378
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
* 006B929C 00000027
* 066B99D8 00000027
* 00010203 04050709
* 080A0B0C 0D0E0F10
* 11141516 1A191217
* 0618132A 1D1E1B1C
* 2B2C2D2E 2F303100
* 006B92A4 00000026
* 066B9A58 00000026
* 1F202122 23242526
* 27283233 34353637
* 38393A3B 3C3D3E3F
* 40414243 44454647
* 48494A4B 4C4D0000
* 06407AAC 0000009C
* 01010202 03030404
* 05050606 07070808
* 0909330A 0B0B0C0C
* 0D0D0E0E 130F1410
* 15111612 17131814
* 19151C16 1D171E18
* 1F19201A 211B221C
* 231D241E 251F2932
* 2A332B34 2C352D36
* 2F373038 3139323A
* 2E3BFFFF 40204121
* 42224323 44244525
* 46264727 48284929
* 4A2A4B2B 4C2C4D2D
* 4E2E4F2F 50305131
* 523D533E 543F5540
* 56415742 58435944
* 5A455B46 5C475D48
* 5E495F4A 604B614C
* 624D634E 00000000";
			loadCustomSSS(s);
		}

		private static ReadOnlyCollection<Stage> _stages;
		public static ReadOnlyCollection<Stage> Stages {
			get {
				if (_stages == null) _stages = stageList.AsReadOnly();
				return _stages;
			}
		}

		public static void loadCustomSSS(string code) {
			code = code.Substring(code.IndexOf("06407AAC 000000"));
			char[] chars = (from c in code
							where char.IsLetterOrDigit(c)
							select c).ToArray();
			sss = new List<Tuple<int, int>>();
			for (int i = 16 /* skip a line*/; i < chars.Length; i += 4) {
				byte stage = Convert.ToByte(chars[i + 0] + "" + chars[i + 1], 16);
				byte icon  = Convert.ToByte(chars[i + 2] + "" + chars[i + 3], 16);
				Tuple<int, int> pair = new Tuple<int, int>(stage, icon);
				sss.Add(pair);
			}
		}

		public static int IconForPac(string filename) {
			int stageID = -1;
			if (filename.StartsWith("STGCUSTOM", StringComparison.InvariantCultureIgnoreCase)) {
				stageID = Int32.Parse(filename.Substring(9, 2)) + 0x3F;
			} else {
				var q = from s in stageList
						where s.ContainsPac(filename)
						select s.ID;
				if (q.Count() > 1) {
					throw new Exception("More than one stage matches the search pattern: " + filename);
				} else if (q.Count() < 1) {
					throw new Exception("No stage matches the search pattern: " + filename);
				}
				stageID = q.First();
			}
			return (from pair in sss
					where pair.Item1 == stageID
					select pair.Item2).FirstOrDefault();
		}

		public static string PacBasenameForIcon(int iconID) {
			int stageID = (from pair in sss
						   where pair.Item2 == iconID
						   select pair.Item1).FirstOrDefault();
			if (stageID >= 0x40) {
				return "STGCUSTOM" + (stageID - 0x3F).ToString("X2") + ".pac";
			} else {
				var q = from s in stageList
						where s.ID == stageID
						select s.PacBasename;
				return q.First();
			}
		}

		public static string RelNameForPac(string filename) {
			if (filename.StartsWith("STGCUSTOM", StringComparison.InvariantCultureIgnoreCase)) {
				return "st_custom" + filename.Substring(9, 2) + ".rel";
			} else {
				var q = from s in stageList
						where s.ContainsPac(filename)
						select s.RelName;
				if (q.Count() > 1) {
					throw new Exception("More than one stage matches the search pattern: " + filename);
				} else if (q.Count() < 1) {
					throw new Exception("No stage matches the search pattern: " + filename);
				}
				return q.First();
			}
		}
	}
}
