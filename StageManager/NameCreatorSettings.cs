﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BrawlStageManager {
	public class NameCreatorSettings {
		public Font Font;
		public int VerticalOffset;
		public override string ToString() {
			return String.Format("{0}pt {1} {2} ({3})", Font.SizeInPoints, Font.Name, Font.Style, VerticalOffset);
		}
	}
}
