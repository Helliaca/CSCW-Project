using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class PlayerInfo {
	public enum STATUS {Idle, Ingame};

	public STATUS state;
	Globals.TEAMS _team = Globals.TEAMS.NONE;
	string _name;
	static int idcounter = 0;
	public int id;
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

	public PlayerInfo(string name = "Player", int id = 0) {
		state = STATUS.Idle;
		this.name = name;
		this.id = id;
	}

	public bool isHost() {
		return host;
	}

	public void setHost(bool host) {
		this.host = host;
	}
}
