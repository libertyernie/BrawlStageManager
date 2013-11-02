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
				return filename.ToLower() == "stg" + PacBasename.ToLower() + ".pac";
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
		private static List<int> sss_order;
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

			string s = @"* 046b8f5c 7c802378
* 046b8f64 7c6300ae
* 040af618 5460083c
* 040af68c 38840002
* 040af6ac 5463083c
* 040af6c0 88030001
* 040af6e8 3860ffff
* 040af59c 3860000c
* 060b91c8 00000018
* bfa10014 7cdf3378
* 7cbe2b78 7c7d1b78
* 2d05ffff 418a0014
* 006b929c 00000019
* 066b99d8 00000019
* 040d140a 032c160c
* 0e2b151c 2d172000
* 0f011a23 18071e2e
* 2f000000 00000000
* 006b92a4 00000019
* 066b9a58 00000019
* 24051208 1b110b1d
* 21192213 26022806
* 10091f25 27303132
* 33000000 00000000
* 06407aac 0000009c
* 01010202 03030404
* 05050606 07070808
* 0909330a 0b0b0c0c
* 0d0d0e0e 130f1410
* 15111612 17131814
* 40151c16 1d171e18
* 1f19201a 211b221c
* 231d241e 251f2932
* 2a332b34 2c352d36
* 2f373038 3139323a
* 2e3b0064 40204121
* 42224323 44244525
* 46264727 48284929
* 4a2a4b2b 4c2c4d2d
* 4e2e4f2f 50305131
* 523d533e 543f5540
* 56415742 58435944
* 5a455b46 5c475d48
* 5e495f4a 604b614c
* 624d634e 00000000";
			loadCustomSSS(s);
		}

		private static ReadOnlyCollection<Stage> _stages;
		public static ReadOnlyCollection<Stage> Stages {
			get {
				if (_stages == null) _stages = stageList.AsReadOnly();
				return _stages;
			}
		}
		private static ReadOnlyCollection<Tuple<int, int>> _stageIconPairs;
		public static ReadOnlyCollection<Tuple<int, int>> StageIconPairs {
			get {
				if (_stageIconPairs == null) _stageIconPairs = sss.AsReadOnly();
				return _stageIconPairs;
			}
		}

		public static List<string> PacFilesBySSSOrder() {
			List<string> list = new List<string>();
			foreach (int index in sss_order) {
				var tuple = StageIconPairs[index];
				if (tuple.Item1 >= 0x40) {
					list.Add("STGCUSTOM" + (tuple.Item1 - 0x3F).ToString("X2") + ".pac");
				} else {
					var q = from s in stageList
							where s.ID == tuple.Item1
							select s.PacNames;
					foreach (string[] ss in q) {
						list.AddRange(ss);
					}
				}
			}
			return list;
		}

		public static void loadCustomSSS(string code) {
			code = code.ToUpper();

			int[] ia = {
				code.IndexOf("066B99D8 000000"),
				code.IndexOf("006B92A4 000000"),
				code.IndexOf("066B9A58 000000"),
				code.IndexOf("06407AAC 000000"),
			};

			string screenA = code.Substring(ia[0], ia[1] - ia[0]);
			string screenB = code.Substring(ia[2], ia[3] - ia[2]);
			char[] charsA = (from c in screenA
							  where char.IsLetterOrDigit(c)
							  select c).ToArray();
			char[] charsB = (from c in screenB
							  where char.IsLetterOrDigit(c)
							  select c).ToArray();
			sss_order = new List<int>();
			for (int i = 16 /* skip a line*/; i < charsA.Length; i += 2) {
				byte index = Convert.ToByte(charsA[i + 0] + "" + charsA[i + 1], 16);
				sss_order.Add(index);
			}
			for (int i = 16 /* skip a line*/; i < charsB.Length; i += 2) {
				byte index = Convert.ToByte(charsB[i + 0] + "" + charsB[i + 1], 16);
				sss_order.Add(index);
			}

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
				stageID = Convert.ToInt32(filename.Substring(9, 2), 16) + 0x3F;
			} else {
				var q = from s in stageList
						where s.ContainsPac(filename)
						select s.ID;
				if (q.Count() > 1) {
					Console.WriteLine("More than one stage matches the search pattern: " + filename);
					return q.First();
				} else if (q.Count() < 1) {
					Console.WriteLine("No stage matches the search pattern: " + filename);
					return -1;
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
					Console.WriteLine("More than one stage matches the search pattern: " + filename);
					return q.First();
				} else if (q.Count() < 1) {
					Console.WriteLine("No stage matches the search pattern: " + filename);
					return "none";
				}
				return q.First();
			}
		}

		#region sc_selcharacter2
		private static int[] sc_selcharacter2_icon_from_sc_selmap_icon = {
			1, // Battlefield
			2, // Final Destination
			3, // Delfino Plaza
			4, // Luigi's Mansion
			5, // Mushroomy Kingdom
			6, // Mario Circuit
			25, // 75 m
			7, // Rumble Falls
			9, // Pirate Ship
			8, // Bridge of Eldin
			10, // Norfair
			11, // Frigate Orpheon
			12, // Yoshi's Island (Brawl)
			13, // Halberd
			14, // Lylat Cruise
			15, // Pokemon Stadium 2
			16, // Spear Pillar
			17, // Port Town Aero Dive
			23, // Summit
			27, // Flat Zone 2
			18, // Castle Siege
			19, // WarioWare Inc.
			20, // Distant Planet
			24, // Skyworld
			26, // Mario Bros.
			22, // New Pork City
			21, // Smashville
			30, // Shadow Moses Island
			31, // Green Hill Zone
			28, // PictoChat
			29, // Hanenbow
			50, // Temple
			51, // Yoshi's Island (Melee)
			52, // Jungle Japes
			53, // Onett
			54, // Green Greens
			55, // Rainbow Cruise
			56, // Corneria
			57, // Big Blue
			58, // Brinstar
			59, // Pokemon Stadium
		};
		public static int selmapIcon(int selcharacter2Icon) {
			int index = -1;
			for (int i = 0; i < sc_selcharacter2_icon_from_sc_selmap_icon.Length; i++) {
				if (sc_selcharacter2_icon_from_sc_selmap_icon[i] == selcharacter2Icon) {
					index = i;
					break;
				}
			}
			return StageIconPairs[index].Item2;
		}
		#endregion
	}
}
