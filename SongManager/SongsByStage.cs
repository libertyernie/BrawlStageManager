﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BrawlSongManager {
	public class SongsByStage {

		public class SongInfo {
			public SongInfo(string s) {
				File = new FileInfo(s+".brstm");
			}

			public FileInfo File;

			public override string ToString() {
				string s = File.Name;
				if (File.Exists) {
					s = "* " + s;
				} else {
					s = "  " + s;
				}
				return s;
			}
		}

		public static object[] FromCurrentDir {
			get {
				object[] oa = {
					"BATTLEFIELD",
					new SongInfo("X04"),
					new SongInfo("T02"),
					new SongInfo("X25"),
					new SongInfo("W21"),
					new SongInfo("W23"),
					"FINAL DESTINATION",
					new SongInfo("X05"),
					new SongInfo("T01"),
					new SongInfo("T03"),
					new SongInfo("W25"),
					new SongInfo("W31"),
					"DELFINO PLAZA",
					new SongInfo("A13"),
					new SongInfo("A07"),
					new SongInfo("A08"),
					new SongInfo("A14"),
					new SongInfo("A15"),
					"LUIGI'S MANSION",
					new SongInfo("A09"),
					new SongInfo("A06"),
					new SongInfo("A05"),
					new SongInfo("Q10"),
					new SongInfo("Q11"),
					"MUSHROOMY KINGDOM 1-1",
					new SongInfo("A01"),
					new SongInfo("A16"),
					new SongInfo("A10"),
					"MUSHROOMY KINGDOM 1-2",
					new SongInfo("A02"),
					new SongInfo("A03"),
					new SongInfo("A04"),
					"MARIO CIRCUIT",
					new SongInfo("A20"),
					new SongInfo("A21"),
					new SongInfo("A22"),
					new SongInfo("A23"),
					new SongInfo("R05"),
					new SongInfo("R14"),
					new SongInfo("Q09"),
					"RUMBLE FALLS",
					new SongInfo("B01"),
					new SongInfo("B08"),
					new SongInfo("B05"),
					new SongInfo("B06"),
					new SongInfo("B07"),
					new SongInfo("B10"),
					new SongInfo("B02"),
					"BRIDGE OF ELDIN",
					new SongInfo("C02"),
					new SongInfo("C09"),
					new SongInfo("C01"),
					new SongInfo("C04"),
					new SongInfo("C05"),
					new SongInfo("C08"),
					new SongInfo("C17"),
					new SongInfo("C18"),
					new SongInfo("C19"),
					"PIRATE SHIP",
					new SongInfo("C15"),
					new SongInfo("C16"),
					new SongInfo("C07"),
					new SongInfo("C10"),
					new SongInfo("C13"),
					new SongInfo("C11"),
					new SongInfo("C12"),
					new SongInfo("C14"),
					"NORFAIR",
					new SongInfo("D01"),
					new SongInfo("D03"),
					new SongInfo("D02"),
					new SongInfo("D05"),
					new SongInfo("R12"),
					new SongInfo("R07"),
					"FRIGATE ORPHEON",
					new SongInfo("D04"),
					new SongInfo("D08"),
					new SongInfo("D07"),
					new SongInfo("D06"),
					new SongInfo("D09"),
					new SongInfo("D10"),
					"YOSHI'S ISLAND (BRAWL)",
					new SongInfo("E02"),
					new SongInfo("E07"),
					new SongInfo("E01"),
					new SongInfo("E03"),
					new SongInfo("E05"),
					new SongInfo("E06"),
					"HALBERD",
					new SongInfo("F06"),
					new SongInfo("F01"),
					new SongInfo("F05"),
					new SongInfo("F04"),
					new SongInfo("F02"),
					new SongInfo("F12"),
					new SongInfo("F07"),
					new SongInfo("F08"),
					new SongInfo("F03"),
					new SongInfo("F10"),
					new SongInfo("F09"),
					new SongInfo("F11"),
					"LYLAT CRUISE",
					new SongInfo("G10"),
					new SongInfo("G02"),
					new SongInfo("G01"),
					new SongInfo("G03"),
					new SongInfo("G04"),
					new SongInfo("G11"),
					new SongInfo("G05"),
					new SongInfo("G09"),
					new SongInfo("G07"),
					new SongInfo("G08"),
					new SongInfo("Q12"),
					"POKEMON STADIUM 2",
					new SongInfo("H01"),
					new SongInfo("H03"),
					new SongInfo("H02"),
					new SongInfo("H04"),
					new SongInfo("H05"),
					"SPEAR PILLAR",
					new SongInfo("H06"),
					new SongInfo("H08"),
					new SongInfo("H07"),
					new SongInfo("H09"),
					new SongInfo("H10"),
					"PORT TOWN: AERO DIVE",
					new SongInfo("I01"),
					new SongInfo("I03"),
					new SongInfo("I02"),
					new SongInfo("I04"),
					new SongInfo("I05"),
					new SongInfo("I06"),
					new SongInfo("I07"),
					new SongInfo("I08"),
					new SongInfo("I09"),
					new SongInfo("I10"),
					new SongInfo("R09"),
					new SongInfo("W18"),
					"CASTLE SIEGE",
					new SongInfo("J02"),
					new SongInfo("J04"),
					new SongInfo("J08"),
					new SongInfo("J06"),
					new SongInfo("J07"),
					new SongInfo("J03"),
					new SongInfo("J13"),
					new SongInfo("J09"),
					new SongInfo("J10"),
					new SongInfo("J11"),
					new SongInfo("J12"),
					new SongInfo("W17"),
					"WARIOWARE, INC.",
					new SongInfo("M01"),
					new SongInfo("M02"),
					new SongInfo("M08"),
					new SongInfo("M07"),
					new SongInfo("M06"),
					new SongInfo("M05"),
					new SongInfo("M04"),
					new SongInfo("M03"),
					new SongInfo("M09"),
					new SongInfo("M10"),
					new SongInfo("M11"),
					new SongInfo("M12"),
					new SongInfo("M13"),
					new SongInfo("M15"),
					new SongInfo("M16"),
					new SongInfo("M17"),
					new SongInfo("M18"),
					"DISTANT PLANET",
					new SongInfo("L06"),
					new SongInfo("L01"),
					new SongInfo("L07"),
					new SongInfo("L02"),
					new SongInfo("L04"),
					new SongInfo("L08"),
					new SongInfo("L05"),
					new SongInfo("L03"),
					new SongInfo("R08"),
					"SMASHVILLE",
					new SongInfo("N01"),
					new SongInfo("N02"),
					new SongInfo("N03"),
					new SongInfo("N06"),
					new SongInfo("N05"),
					new SongInfo("N07"),
					new SongInfo("N08"),
					new SongInfo("N09"),
					new SongInfo("N10"),
					new SongInfo("N11"),
					new SongInfo("N12"),
					"NEW PORK CITY",
					new SongInfo("K07"),
					new SongInfo("K09"),
					new SongInfo("K08"),
					new SongInfo("K10"),
					new SongInfo("K05"),
					new SongInfo("K01"),
					"SUMMIT",
					new SongInfo("Q07"),
					new SongInfo("Q06"),
					new SongInfo("Q08"),
					new SongInfo("Q05"),
					new SongInfo("W13"),
					"SKYWORLD",
					new SongInfo("P01"),
					new SongInfo("P03"),
					new SongInfo("P02"),
					new SongInfo("P04"),
					"75M",
					new SongInfo("B04"),
					new SongInfo("B03"),
					new SongInfo("B09"),
					"MARIO BROS.",
					new SongInfo("A17"),
					new SongInfo("Q02"),
					new SongInfo("Q01"),
					new SongInfo("Q13"),
					new SongInfo("Q14"),
					"FLAT ZONE 2",
					new SongInfo("R04"),
					new SongInfo("Q04"),
					new SongInfo("W14"),
					"PICTOCHAT",
					new SongInfo("R02"),
					new SongInfo("R10"),
					new SongInfo("R11"),
					new SongInfo("R15"),
					new SongInfo("R16"),
					new SongInfo("R17"),
					new SongInfo("R13"),
					new SongInfo("R06"),
					new SongInfo("W20"),
					"HANENBOW",
					new SongInfo("R03"),
					"SHADOW MOSES ISLAND",
					new SongInfo("S06"),
					new SongInfo("S02"),
					new SongInfo("S03"),
					new SongInfo("S08"),
					new SongInfo("S04"),
					new SongInfo("S07"),
					new SongInfo("S05"),
					new SongInfo("S10"),
					new SongInfo("S11"),
					"GREEN HILL ZONE",
					new SongInfo("U01"),
					new SongInfo("U04"),
					new SongInfo("U02"),
					new SongInfo("U03"),
					new SongInfo("U06"),
					new SongInfo("U07"),
					new SongInfo("U08"),
					new SongInfo("U09"),
					new SongInfo("U10"),
					new SongInfo("U11"),
					new SongInfo("U12"),
					new SongInfo("U13"),
					"TEMPLE",
					new SongInfo("C03"),
					new SongInfo("W24"),
					"YOSHI'S ISLAND (MELEE)",
					new SongInfo("W05"),
					new SongInfo("W15"),
					"JUNGLE JAPES",
					new SongInfo("W03"),
					new SongInfo("W26"),
					"ONETT",
					new SongInfo("W12"),
					new SongInfo("W19"),
					"CORNERIA",
					new SongInfo("W08"),
					new SongInfo("W28"),
					"RAINBOW CRUISE",
					new SongInfo("W02"),
					new SongInfo("W01"),
					"GREEN GREENS",
					new SongInfo("W07"),
					new SongInfo("W06"),
					"BIG BLUE",
					new SongInfo("W11"),
					new SongInfo("W29"),
					"BRINSTAR",
					new SongInfo("W27"),
					new SongInfo("W04"),
					"POKEMON STADIUM",
					new SongInfo("W09"),
					new SongInfo("W16"),
					new SongInfo("W10"),
					"MENU",
					new SongInfo("X02"),
					new SongInfo("X03"),
					new SongInfo("W30"),
					new SongInfo("X01"),
					"SUBSPACE EMISSARY",
					new SongInfo("Y01"),
					new SongInfo("Y02"),
					new SongInfo("Y03"),
					new SongInfo("Y04"),
					new SongInfo("Y09"),
					new SongInfo("Y10"),
					new SongInfo("Y11"),
					new SongInfo("Y13"),
					new SongInfo("Y14"),
					new SongInfo("Y15"),
					new SongInfo("Y05"),
					new SongInfo("Y07"),
					new SongInfo("Y08"),
					new SongInfo("Y16"),
					new SongInfo("Y17"),
					"VICTORY THEMES",
					new SongInfo("Z01"),
					new SongInfo("Z02"),
					new SongInfo("Z03"),
					new SongInfo("Z04"),
					new SongInfo("Z05"),
					new SongInfo("Z06"),
					new SongInfo("Z07"),
					new SongInfo("Z08"),
					new SongInfo("Z10"),
					new SongInfo("Z11"),
					new SongInfo("Z16"),
					new SongInfo("Z17"),
					new SongInfo("Z18"),
					new SongInfo("Z21"),
					new SongInfo("Z22"),
					new SongInfo("Z23"),
					new SongInfo("Z25"),
					new SongInfo("Z35"),
					new SongInfo("Z46"),
					new SongInfo("Z47"),
					"OTHER",
					new SongInfo("X07"),
					new SongInfo("X08"),
					new SongInfo("X09"),
					new SongInfo("X10"),
					new SongInfo("X11"),
					new SongInfo("X13"),
					new SongInfo("X15"),
					new SongInfo("X16"),
					new SongInfo("X17"),
					new SongInfo("X18"),
					new SongInfo("X19"),
					new SongInfo("X20"),
					new SongInfo("X21"),
					new SongInfo("X23"),
					new SongInfo("X26"),
					new SongInfo("X27"),
					new SongInfo("T05"),
					new SongInfo("X22"),
					new SongInfo("X06"),
					new SongInfo("X24"),
					new SongInfo("Z50"),
					new SongInfo("Z51"),
					new SongInfo("Z54"),
					new SongInfo("Z55"),
					new SongInfo("Z56"),
					new SongInfo("Z57"),
					new SongInfo("Z58"),
				};
				return oa;
			}
		}

	}
}
