using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorScheme {

	public enum color_context {BASE, BORDER, FLAG};

	public static Color getColorOf(color_context cc, Globals.TEAMS team) {
		switch(cc) {
		case color_context.BORDER : {
				switch(team) {
				case Globals.TEAMS.RED : {return new Color(1f, 0.6f, 0.6f, 1f);}
				case Globals.TEAMS.BLUE : {return new Color(0.6f, 0.6f, 1f, 1f);}
				case Globals.TEAMS.GREEN : {return new Color(0.6f, 1f, 0.6f, 1f);}
				case Globals.TEAMS.ORANGE : {return new Color(1.0f, 1.0f, 0.6f, 1f);}
				}
				break;
			}
		case color_context.BASE : {
				switch(team) {
				case Globals.TEAMS.RED : {return new Color(1f, 0.3f, 0.3f, 1f);}
				case Globals.TEAMS.BLUE : {return new Color(0.3f, 0.3f, 1f, 1f);}
				case Globals.TEAMS.GREEN : {return new Color(0.3f, 1f, 0.3f, 1f);}
				case Globals.TEAMS.ORANGE : {return new Color(1.0f, 0.8f, 0.3f, 1f);}
				}
				break;
			}
		case color_context.FLAG : {
				switch(team) {
				case Globals.TEAMS.RED : {return new Color(1f, 0f, 0f, 1f);}
				case Globals.TEAMS.BLUE : {return new Color(0f, 0f, 1f, 1f);}
				case Globals.TEAMS.GREEN : {return new Color(0f, 1f, 0f, 1f);}
				case Globals.TEAMS.ORANGE : {return new Color(1f, 0.6f, 0f, 1f);}
				}
				break;
			}
		}
		return new Color(1f, 0f, 1f, 1f);
	}
}
