using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo {
	public enum STATUS {Idle, Ingame};

	public STATUS state;
	Globals.TEAMS _team;
	string _name;
	static int idcounter = 0;
	int id;
	bool host = false;

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
		state = STATUS.Idle;
		this.name = name;
		id = ++idcounter;
	}

	public bool isHost() {
		return host;
	}

	public void setHost(bool host) {
		this.host = host;
	}
}
