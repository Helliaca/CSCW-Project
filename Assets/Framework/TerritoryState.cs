using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TerritoryState {

	public TerritoryState(string name, Globals.TEAMS owner) {
		this.territoryName = name;
		this.owner = owner;
	}

	public TerritoryState(string name, string owner) {
		this.territoryName = name;
		try {
			this.owner = (Globals.TEAMS) Enum.Parse(typeof(Globals.TEAMS), owner);
		} catch (Exception e) {
			Globals.DevConsole.print("Could not set territory owner: " + e.ToString());
			this.owner = Globals.TEAMS.NONE;
		}
	}

	public void Apply() {
		GameObject.Find(territoryName).GetComponent<TerritoryController>().setState(this);
	}

	public string territoryName;
	public Globals.TEAMS owner;
}
