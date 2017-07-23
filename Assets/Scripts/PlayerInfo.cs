using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo {
	Globals.TEAMS _team;
	string _name;
	static int idcounter = 0;
	int id;
	bool isHost = false;

	public Globals.TEAMS team {
		get {
			return _team;
		}
		set {
			this._team = value;
		}
	}

	public string name {
		get {
			return _name;
		}
		set {
			this._name = value;
		}
	}

	public PlayerInfo(string name = "Player") {
		this.name = name;
		id = ++idcounter;
	}
}
