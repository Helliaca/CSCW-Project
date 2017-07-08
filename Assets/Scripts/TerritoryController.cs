using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryController : MonoBehaviour {

	private static Color color_default = new Color(0.7647f, 0.7647f, 0.7647f, 1f);
	private static Color color_selected = new Color(0.854f, 0.814f, 0.814f, 0.5f);
	private static Color color_claimed = new Color(1f, 0.5f, 0.5f, 1f);

	public Globals.TEAMS owner = Globals.TEAMS.NONE;
	public TerritoryController[] Neighbours;

	void OnMouseEnter() {
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_Color", color_selected);
		Globals.Data.hoverTerritory = this.transform;
	}

	void OnMouseExit() {
		Renderer rend = GetComponent<Renderer>();
		if(owner==Globals.TEAMS.NONE) rend.material.SetColor("_Color", color_default);
		else rend.material.SetColor("_Color", color_claimed);
		Globals.Data.hoverTerritory = null;
	}

	public void setOwner(Globals.TEAMS owner) {
		this.owner = owner;
		switch(owner) {
		default : {
				GetComponent<Renderer>().material.SetColor("_Color", color_claimed);
				break;}
		}
		Globals.InstanceClient.Send(MessageHandler.encode(getState())); //Send new TerritoryState to Server
	}

	TerritoryState getState() {
		TerritoryState ts = new TerritoryState();
		ts.owner = this.owner.ToString();
		ts.territoryName = this.gameObject.name;
		return ts;
	}
}
