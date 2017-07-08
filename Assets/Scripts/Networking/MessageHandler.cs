using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class MessageHandler {
	static void handle(string msg) {
		Regex reg = new Regex(@"(.?*)<~>(.?*)<~>(.?*)");
		Match match = reg.Match(msg);
		if(!match.Success) {
			Debug.Log("Message from client not recognized.");
			return;
		}

		string msg_type = match.Groups[0].ToString();
		switch(msg_type) {
		case "txm" : {
				break;} //regular Textmessage
		case "trs" : {
				break;} //TerritoryState
		}
	}

	public static string encode(string regularMsg) {
		return @"txm<~>" + Globals.InstancePlayerName + "<~>" + regularMsg;
	}

	public static string encode(TerritoryState ts) {
		return @"trs<~>" + ts.territoryName + "<~>" + ts.owner;
	}
}
