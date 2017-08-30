using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public static class MessageHandler {
	public static void handle(string msg) {
		Regex reg = new Regex(@"^#([\w\.\-]+)§([\w\.\-]+)&(.*?)$");
		Match match = reg.Match(msg);
		if(!match.Success) {
			Globals.DevConsole.print("Could not recognize message: " + msg);
			return;
		}

		string msg_type = match.Groups[1].ToString();
		switch(msg_type) {
		case "txm" : {
				string msg_sender = match.Groups[2].ToString();
				string msg_content = match.Groups[3].ToString();
				Globals.DevConsole.print(msg_content);
				break;} //regular Textmessage
		case "trs" : {
				string ter_name = match.Groups[2].ToString();
				string ter_owner = match.Groups[3].ToString();
				TerritoryController t = GameObject.Find(ter_name).GetComponent<TerritoryController>();
				TerritoryState newState = new TerritoryState(ter_name, ter_owner);
				newState.Apply();
				break;} //TerritoryState
		case "spt" : {
				string player_name = match.Groups[2].ToString();
				string team_name = match.Groups[3].ToString();
				Globals.getPlayerByName(player_name).team = (Globals.TEAMS) Enum.Parse(typeof(Globals.TEAMS), team_name); //TODO: Differentiating players by their name is terrible
				break;} //Set Player Team
		case "uid" : {
				Globals.InstanceNetwork.gotPlayerId(int.Parse(match.Groups[2].ToString()));
				break;} //Receive player id froms erver
		case "con" : {
				if(!Globals.InstanceNetwork.isHosting()) {
					Globals.DevConsole.print("Confrimed player connection recevied while not hosting game");
					return;
				}
				PlayerInfo player = new PlayerInfo(match.Groups[3].ToString());
				player.id = int.Parse(match.Groups[2].ToString());
				Globals.players.Add(player);
				break;} //Confrim new connected player
		case "evt" : {
				string event_name = match.Groups[2].ToString();
				switch(event_name) {
				case "startGame" : {
						Globals.startGame();
						break;}
				}
				break;} //Start the game
		}
	}

	public static string encode(string regularMsg) {
		return @"#txm§" + Globals.InstancePlayer.name + "&" + regularMsg;
	}

	public static string encode(TerritoryState ts) {
		return @"#trs§" + ts.territoryName + "&" + ts.owner;
	}

	public static string encode(Globals.TEAMS newTeam) {
		return @"#spt§" + Globals.InstancePlayer.name + "&" + newTeam.ToString();
	}

	public static string encodeEvent(string evt, string parameter="") {
		return @"#evt§" + evt + "&" + parameter;
	}

	public static string encode(int uniqueId) {
		return @"#uid§" + uniqueId + "&empty";
	}

	public static string encodeConfrimConnection(int id, string playerName) {
		return @"#con§" + id + "&" + playerName;
	}
}
