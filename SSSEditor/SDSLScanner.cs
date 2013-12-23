using BrawlStageManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SSSEditor {
	public static class SDSLScanner {
		#region Song names, ids, filenames
		public class Song {
			public string Name {get; private set;}
			public string Filename {get; private set;}
			public ushort ID {get; private set;}

			public Song(string name, string filename, ushort id) {
				Name = name;
				Filename = filename;
				ID = id;
			}

			public override string ToString() {
 				 return Filename + " (" + Name + ")";
			}
		}

		private static Dictionary<ushort, Song> songs;

		public static Song SongFromID(ushort id) {
			return songs[id];
		}

		private static void songadd(string name, string filename, int id, bool unsure) {
			ushort nid = (ushort)id;
			songs.Add(nid, new Song(name, filename, nid));
		}

		static SDSLScanner() {
			songs = new Dictionary<ushort, Song>();
			songadd("Super Smash Bros. Brawl Main Theme", "X01", 0x26f9, false);
			songadd("Menu 1", "X02", 0x26fa, false);
			songadd("Menu 2", "X03", 0x26fb, false);
			songadd("Battlefield", "X04", 0x26fc, false);
			songadd("Final Destination", "X05", 0x26fd, false);
			songadd("Classic mode clear [NAMELESS]", "X06", 0x26fe, true);
			songadd("Online Practice Stage", "X07", 0x26ff, false);
			songadd("Results Display Screen", "X08", 0x2700, false);
			songadd("Tournament Registration", "X09", 0x2701, false);
			songadd("Tournament Grid", "X10", 0x2702, false);
			songadd("Tournament Match End", "X11", 0x2703, false);
			songadd("Classic: Results Screen", "X13", 0x2705, false);
			songadd("All-Star Rest Area", "X15", 0x2707, false);
			songadd("Home-Run Contest", "X16", 0x2708, false);
			songadd("Cruel Brawl", "X17", 0x2709, false);
			songadd("Boss Battle", "X18", 0x270a, false);
			songadd("Trophy Gallery", "X19", 0x270b, false);
			songadd("Sticker Album / Album / Chronicle", "X20", 0x270c, false);
			songadd("Coin Launcher", "X21", 0x270d, false);
			songadd("Classic mode clear - trophy [NAMELESS]", "X22", 0x270e, true);
			songadd("Stage Builder", "X23", 0x270f, false);
			songadd("Battlefield Ver. 2", "X25", 0x2711, false);
			songadd("Target Smash!!", "X26", 0x2712, false);
			songadd("Credits", "X27", 0x2713, false);
			songadd("Ground Theme (Super Mario Bros.)", "A01", 0x2714, false);
			songadd("Underground Theme (Super Mario Bros.)", "A02", 0x2715, false);
			songadd("Underwater Theme (Super Mario Bros.)", "A03", 0x2716, false);
			songadd("Underground Theme (Super Mario Land)", "A04", 0x2717, false);
			songadd("Airship Theme (Super Mario Bros. 3)", "A05", 0x2718, true);
			songadd("Castle / Boss Fortress (Super Mario World / SMB 3)", "A06", 0x2719, true);
			songadd("Title / Ending (Super Mario World)", "A07", 0x271a, false);
			songadd("Main Theme (New Super Mario Bros.)", "A08", 0x271b, false);
			songadd("Luigi's Mansion Theme", "A09", 0x271c, false);
			songadd("Gritzy Desert", "A10", 0x271d, false);
			songadd("Delfino Plaza", "A13", 0x2720, false);
			songadd("Ricco Harbor", "A14", 0x2721, false);
			songadd("Main Theme (Super Mario 64)", "A15", 0x2722, false);
			songadd("Ground Theme 2 (Super Mario Bros.)", "A16", 0x2723, false);
			songadd("Mario Bros.", "A17", 0x2724, false);
			songadd("Mario Circuit", "A20", 0x2725, false);
			songadd("Luigi Circuit", "A21", 0x2726, false);
			songadd("Waluigi Pinball", "A22", 0x2727, false);
			songadd("Rainbow Road", "A23", 0x2728, false);
			songadd("Jungle Level Ver.2", "B01", 0x2729, false);
			songadd("The Map Page / Bonus Level", "B02", 0x272a, false);
			songadd("Opening (Donkey Kong)", "B03", 0x272b, false);
			songadd("Donkey Kong", "B04", 0x272c, false);
			songadd("King K.Rool / Ship Deck 2", "B05", 0x272d, true);
			songadd("Bramble Blast", "B06", 0x272e, false);
			songadd("Battle for Storm Hill", "B07", 0x272f, false);
			songadd("Jungle Level", "B08", 0x2730, false);
			songadd("25m BGM", "B09", 0x2731, false);
			songadd("DK Jungle 1 Theme (Barrel Blast)", "B10", 0x2732, false);
			songadd("Title (The Legend of Zelda)", "C01", 0x2733, false);
			songadd("Main Theme (The Legend of Zelda)", "C02", 0x2734, false);
			songadd("Great Temple / Temple", "C03", 0x2735, false);
			songadd("The Dark World", "C04", 0x2736, false);
			songadd("Hidden Mountain & Forest", "C05", 0x2737, false);
			songadd("Tal Tal Heights", "C07", 0x2739, false);
			songadd("Hyrule Field Theme", "C08", 0x273a, false);
			songadd("Ocarina of Time Medley", "C09", 0x273b, false);
			songadd("Song of Storms", "C10", 0x273c, false);
			songadd("Molgera Battle", "C11", 0x273d, false);
			songadd("Village of the Blue Maiden", "C12", 0x273e, false);
			songadd("Gerudo Valley", "C13", 0x273f, false);
			songadd("Termina Field", "C14", 0x2740, false);
			songadd("Dragon Roost Island", "C15", 0x2741, false);
			songadd("The Great Sea", "C16", 0x2742, false);
			songadd("Main Theme (Twilight Princess)", "C17", 0x2743, false);
			songadd("The Hidden Village", "C18", 0x2744, false);
			songadd("Midna's Lament", "C19", 0x2745, false);
			songadd("Main Theme (Metroid)", "D01", 0x2746, false);
			songadd("Norfair", "D02", 0x2747, false);
			songadd("Ending (Metroid)", "D03", 0x2748, false);
			songadd("Vs. Ridley", "D04", 0x2749, false);
			songadd("Theme of Samus Aran, Space Warrior", "D05", 0x274a, false);
			songadd("Sector 1", "D06", 0x274b, false);
			songadd("Opening / Menu (Metroid Prime)", "D07", 0x274c, false);
			songadd("Vs. Parasite Queen", "D08", 0x274d, false);
			songadd("Vs. Meta Ridley", "D09", 0x274e, false);
			songadd("Multiplayer (Metroid Prime 2)", "D10", 0x274f, false);
			songadd("Ending (Yoshi's Story)", "E01", 0x2750, false);
			songadd("Obstacle Course", "E02", 0x2751, false);
			songadd("Yoshi's Island", "E03", 0x2752, false);
			songadd("Flower Field", "E05", 0x2754, false);
			songadd("Wildlands", "E06", 0x2755, false);
			songadd("The Legendary Air Ride Machine", "F01", 0x2757, false);
			songadd("King Dedede's Theme", "F02", 0x2758, false);
			songadd("Boss Theme Medley", "F03", 0x2759, false);
			songadd("Butter Building", "F04", 0x275a, false);
			songadd("Gourmet Race", "F05", 0x275b, false);
			songadd("Meta Knight's Revenge", "F06", 0x275c, false);
			songadd("Vs. Marx", "F07", 0x275d, false);
			songadd("02 Battle", "F08", 0x275e, true);
			songadd("Forest / Nature Area", "F09", 0x275f, false);
			songadd("Checker Knights", "F10", 0x2760, false);
			songadd("Frozen Hillside", "F11", 0x2761, false);
			songadd("Squeak Squad Theme", "F12", 0x2762, false);
			songadd("Main Theme (Star Fox)", "G01", 0x2763, false);
			songadd("Corneria", "G02", 0x2764, false);
			songadd("Main Theme (Star Fox 64)", "G03", 0x2765, false);
			songadd("Area 6", "G04", 0x2766, false);
			songadd("Star Wolf", "G05", 0x2767, false);
			songadd("Space Battleground", "G07", 0x2769, false);
			songadd("Break Through the Ice", "G08", 0x276a, false);
			songadd("Star Wolf (Star Fox: Assault)", "G09", 0x276b, false);
			songadd("Space Armada", "G10", 0x276c, false);
			songadd("Area 6 Ver. 2", "G11", 0x276d, false);
			songadd("Pokémon Main Theme", "H01", 0x276e, true);
			songadd("Pokémon Center", "H02", 0x276f, true);
			songadd("Road to Viridian City (From Pallet Town / Pewter City)", "H03", 0x2770, true);
			songadd("Pokémon Gym / Evolution", "H04", 0x2771, true);
			songadd("Wild Pokémon Battle! (Ruby / Sapphire)", "H05", 0x2772, true);
			songadd("Victory Road", "H06", 0x2773, false);
			songadd("Wild Pokémon Battle! (Diamond / Pearl)", "H07", 0x2774, true);
			songadd("Dialga / Palkia Battle at Spear Pillar!", "H08", 0x2775, false);
			songadd("Team Galactic Battle!", "H09", 0x2776, false);
			songadd("Route 209", "H10", 0x2777, false);
			songadd("Mute City", "I01", 0x2778, false);
			songadd("White Land", "I02", 0x2779, false);
			songadd("Fire Field", "I03", 0x277a, false);
			songadd("Car Select", "I04", 0x277b, false);
			songadd("Dream Chaser", "I05", 0x277c, false);
			songadd("Devil's Call in Your Heart", "I06", 0x277d, false);
			songadd("Climb Up! And Get The Last Chance!", "I07", 0x277e, false);
			songadd("Brain Cleaner", "I08", 0x277f, false);
			songadd("Shotgun Kiss", "I09", 0x2780, false);
			songadd("Planet Colors", "I10", 0x2781, false);
			songadd("Fire Emblem Theme", "J02", 0x2783, false);
			songadd("Shadow Dragon Medley", "J03", 0x2784, false);
			songadd("With Mila's Divine Protection (Celica Map 1)", "J04", 0x2785, false);
			songadd("Preparing to Advance", "J06", 0x2787, false);
			songadd("Winning Road - Roy's Hope", "J07", 0x2788, false);
			songadd("Attack", "J08", 0x2789, false);
			songadd("Against the Dark Knight", "J09", 0x278a, false);
			songadd("Crimean Army Sortie", "J10", 0x278b, false);
			songadd("Power-Hungry Fool", "J11", 0x278c, false);
			songadd("Victory Is Near", "J12", 0x278d, true);
			songadd("Ike's Theme", "J13", 0x278e, false);
			songadd("Snowman", "K01", 0x278f, false);
			songadd("Humoresque of a Little Dog", "K05", 0x2793, false);
			songadd("Porky's Theme", "K07", 0x2795, false);
			songadd("Mother 3 Love Theme", "K08", 0x2796, false);
			songadd("Unfounded Revenge / Smashing Song of Praise", "K09", 0x2797, false);
			songadd("You Call This a Utopia?!", "K10", 0x2798, false);
			songadd("World Map (Pikmin 2)", "L01", 0x2799, false);
			songadd("Forest of Hope", "L02", 0x279a, false);
			songadd("Environmental Noises", "L03", 0x279b, false);
			songadd("Ai no Uta", "L04", 0x279c, false);
			songadd("Tane no Uta", "L05", 0x279d, false);
			songadd("Main Theme (Pikmin)", "L06", 0x279e, false);
			songadd("Stage Clear / Title (Pikmin)", "L07", 0x279f, false);
			songadd("Ai no Uta (French Version)", "L08", 0x27a0, false);
			songadd("WarioWare, Inc.", "M01", 0x27a1, false);
			songadd("WarioWare, Inc. Medley", "M02", 0x27a2, false);
			songadd("Mona Pizza's Song (Japanese Version)", "M03", 0x27a3, true);
			songadd("Mona Pizza's Song (English Version)", "M04", 0x27a4, true);
			songadd("Mike's Song (Japanese Version)", "M05", 0x27a5, true);
			songadd("Mike's Song (English Version)", "M06", 0x27a6, true);
			songadd("Ashley's Song (Japanese Version)", "M07", 0x27a7, true);
			songadd("Ashley's Song (English Version)", "M08", 0x27a8, true);
			songadd("Title (Animal Crossing)", "N01", 0x27b3, false);
			songadd("Go K.K. Rider!", "N02", 0x27b4, false);
			songadd("2:00 a.m.", "N03", 0x27b5, false);
			songadd("The Roost", "N05", 0x27b7, false);
			songadd("Town Hall and Tom Nook's Store", "N06", 0x27b8, false);
			songadd("K.K. Crusin'", "N07", 0x27b9, true);
			songadd("K.K. Western", "N08", 0x27ba, false);
			songadd("K.K. Gumbo", "N09", 0x27bb, false);
			songadd("Rockin' K.K.", "N10", 0x27bc, true);
			songadd("DJ K.K.", "N11", 0x27bd, true);
			songadd("K.K. Condor", "N12", 0x27be, true);
			songadd("Underworld", "P01", 0x27bf, false);
			songadd("Title (Kid Icarus)", "P02", 0x27c0, false);
			songadd("Skyworld", "P03", 0x27c1, false);
			songadd("Kid Icarus Original Medley", "P04", 0x27c2, false);
			songadd("Famicom Medley", "Q01", 0x27c3, false);
			songadd("Gyromite", "Q02", 0x27c4, false);
			songadd("Chill (Dr. Mario)", "Q04", 0x27c6, false);
			songadd("Clu Clu Land", "Q05", 0x27c7, false);
			songadd("Balloon Trip", "Q06", 0x27c8, false);
			songadd("Ice Climber", "Q07", 0x27c9, false);
			songadd("Shin Onigashima", "Q08", 0x27ca, false);
			songadd("Title (3D Hot Rally)", "Q09", 0x27cb, false);
			songadd("Tetris: Type A", "Q10", 0x27cc, false);
			songadd("Tetris: Type B", "Q11", 0x27cd, false);
			songadd("Tunnel Scene (X)", "Q12", 0x27ce, false);
			songadd("Power-Up Music", "Q13", 0x27cf, false);
			songadd("Douchuumen (Nazo no Murasamejo)", "Q14", 0x27d0, false);
			songadd("PictoChat", "R02", 0x27d2, false);
			songadd("Hanenbow [NAMELESS]", "R03", 0x27d3, true);
			songadd("Flat Zone 2", "R04", 0x27d4, false);
			songadd("Mario Tennis / Mario Golf", "R05", 0x27d5, true);
			songadd("Lip's Theme (Panel de Pon)", "R06", 0x27d6, false);
			songadd("Marionation Gear", "R07", 0x27d7, false);
			songadd("Title (Big Brain Academy)", "R08", 0x27d8, false);
			songadd("Golden Forest (1080Snowboarding)", "R09", 0x27d9, true);
			songadd("Mii Channel", "R10", 0x27da, false);
			songadd("Wii Shop Channel", "R11", 0x27db, false);
			songadd("Battle Scene / Final Boss (Golden Sun)", "R12", 0x27dc, false);
			songadd("Shaberu! DS Cooking Navi", "R13", 0x27dd, false);
			songadd("Excite Truck", "R14", 0x27de, false);
			songadd("Brain Age: Train Your Brain in Minutes a Day", "R15", 0x27df, false);
			songadd("Opening Theme (Wii Sports)", "R16", 0x27e0, false);
			songadd("Charge! (Wii Play)", "R17", 0x27e1, false);
			songadd("Encounter", "S02", 0x27e3, false);
			songadd("Theme of Tara", "S03", 0x27e4, false);
			songadd("Yell \"Dead Cell\"", "S04", 0x27e5, false);
			songadd("Snake Eater (Instrumental)", "S05", 0x27e6, false);
			songadd("MGS4 Theme of Love Smash Bros. Brawl Version", "S06", 0x27e7, true);
			songadd("Cavern", "S07", 0x27e8, false);
			songadd("Battle in the Base", "S08", 0x27e9, false);
			songadd("Theme of Solid Snake", "S10", 0x27eb, false);
			songadd("Calling to the Night", "S11", 0x27ec, false);
			songadd("Credits (Super Smash Bros.)", "T01", 0x27ed, false);
			songadd("Menu (Super Smash Bros. Melee)", "T02", 0x27ee, false);
			songadd("Opening (Super Smash Bros. Melee)", "T03", 0x27ef, false);
			songadd("{FD: Classic Mode Master Hand} [NAMELESS]", "T05", 0x27f1, true);
			songadd("Green Hill Zone", "U01", 0x27f2, false);
			songadd("Scrap Brain Zone", "U02", 0x27f3, false);
			songadd("Emerald Hill Zone", "U03", 0x27f4, false);
			songadd("Angel Island Zone", "U04", 0x27f5, false);
			songadd("Sonic Boom", "U06", 0x27f7, false);
			songadd("Super Sonic Racing", "U07", 0x27f8, false);
			songadd("Open Your Heart", "U08", 0x27f9, false);
			songadd("Live & Learn", "U09", 0x27fa, false);
			songadd("Sonic Heroes", "U10", 0x27fb, false);
			songadd("Right There, Ride On", "U11", 0x27fc, false);
			songadd("HIS WORLD (Instrumental)", "U12", 0x27fd, false);
			songadd("Seven Rings In Hand", "U13", 0x27fe, false);
			songadd("Princess Peach's Castle (Melee)", "W01", 0x27ff, false);
			songadd("Rainbow Cruise (Melee)", "W02", 0x2800, false);
			songadd("Jungle Japes (Melee)", "W03", 0x2801, false);
			songadd("Brinstar Depths (Melee)", "W04", 0x2802, false);
			songadd("Yoshi's Island (Melee)", "W05", 0x2803, false);
			songadd("Fountain of Dreams (Melee)", "W06", 0x2804, false);
			songadd("Green Greens (Melee)", "W07", 0x2805, false);
			songadd("Corneria (Melee)", "W08", 0x2806, false);
			songadd("Pokémon Stadium (Melee)", "W09", 0x2807, true);
			songadd("Poké Floats (Melee)", "W10", 0x2808, true);
			songadd("Big Blue (Melee)", "W11", 0x2809, false);
			songadd("Mother (Melee)", "W12", 0x280a, false);
			songadd("Icicle Mountain (Melee)", "W13", 0x280b, false);
			songadd("Flat Zone (Melee)", "W14", 0x280c, false);
			songadd("Super Mario Bros. 3 (Melee)", "W15", 0x280d, false);
			songadd("Battle Theme (Melee)", "W16", 0x280e, false);
			songadd("Fire Emblem (Melee)", "W17", 0x280f, false);
			songadd("Mach Rider (Melee)", "W18", 0x2810, false);
			songadd("Mother 2 (Melee)", "W19", 0x2811, false);
			songadd("Dr. Mario (Melee)", "W20", 0x2812, false);
			songadd("Battlefield (Melee)", "W21", 0x2813, false);
			songadd("Multi-Man Melee 1 (Melee)", "W23", 0x2815, false);
			songadd("Temple (Melee)", "W24", 0x2816, false);
			songadd("Final Destination (Melee)", "W25", 0x2817, false);
			songadd("Kong Jungle (Melee)", "W26", 0x2818, true);
			songadd("Brinstar (Melee)", "W27", 0x2819, false);
			songadd("Venom (Melee)", "W28", 0x281a, false);
			songadd("Mute City (Melee)", "W29", 0x281b, false);
			songadd("Menu (Melee)", "W30", 0x281c, false);
			songadd("Giga Bowser (Melee)", "W31", 0x281d, false);
			songadd("Adventure Map", "Y01", 0x281f, false);
			songadd("Step: The Plain", "Y02", 0x2820, false);
			songadd("Step: The Cave", "Y03", 0x2821, false);
			songadd("Step: Subspace", "Y04", 0x2822, false);
			songadd("Boss Battle Song 1", "Y05", 0x2823, false);
			songadd("{SSE Results} [NAMELESS]", "???", 0x2824, false);
			songadd("Boss Battle Song 2", "Y07", 0x2825, false);
			songadd("Save Point", "Y08", 0x2826, false);
			songadd("{SSE DK Jungle} [NAMELESS]", "Y09", 0x2827, false);
			songadd("{SSE Luigi Mansion} [NAMELESS]", "Y10", 0x2828, false);
			songadd("{Halberd Interior} [NAMELESS]", "Y11", 0x2829, false);
			songadd("{SSE Data Select} [NAMELESS]", "Y12", 0x282a, false);
			songadd("{SSE Brinstar} [NAMELESS]", "Y13", 0x282b, false);
			songadd("Step: Subspace Ver.2", "Y14", 0x282c, false);
			songadd("Step: Subspace Ver.3", "Y15", 0x282d, false);
			songadd("{Halberd Moving} [NAMELESS]", "Y16", 0x282e, false);
			songadd("???", "Y17", 0x282f, false);
			songadd("???", "Z50", 0x2863, false);
			songadd("???", "Z51", 0x2864, false);
			songadd("???", "Z54", 0x2867, false);
			songadd("???", "Z55", 0x2868, false);
			songadd("???", "Z56", 0x2869, false);
			songadd("???", "Z57", 0x286a, false);
			songadd("???", "Z58", 0x286b, false);
		}
		#endregion

		private static byte[] SDSL_HEADER = { 0x28, 0x70, 0x8c, 0xeb, 0x00, 0x00, 0x00 };
		public static Dictionary<byte, Song> SongsByStage(CustomSSS sss) {
			return SongsByStage(sss.DataBefore.Concat(sss.DataAfter).ToArray());
		}
		public static Dictionary<byte, Song> SongsByStage(byte[] data) {
			Dictionary<byte, Song> dict = new Dictionary<byte, Song>();
			for (int line = 0; line < data.Length; line += 8) {
				if (ByteUtilities.ByteArrayEquals(data, line, SDSL_HEADER, 0, SDSL_HEADER.Length)) {
					byte stageID = data[line + 7];
					byte songID1 = data[line + 22];
					byte songID2 = data[line + 23];
					ushort songID = (ushort)(0x100 * songID1 + songID2);
					if (dict.ContainsKey(stageID)) {
						Console.WriteLine(String.Format("WARNING: code mapping stage {0} to song {1} will not " +
							"take effect, since a later code maps it to song {2}", stageID, dict[stageID].ID, songID));
						dict.Remove(stageID);
					}
					dict.Add(stageID, SongFromID(songID));
					line += 24;
				}
			}
			return dict;
		}
	}
}
