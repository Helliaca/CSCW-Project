using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryController : MonoBehaviour {

	private static Color color_default = new Color(0.7647f, 0.7647f, 0.7647f, 1f);
	private static Color color_selected = new Color(0.854f, 0.814f, 0.814f, 0.5f);
	private static Color color_claimed = new Color(1f, 0.5f, 0.5f, 1f);

	public Globals.TEAMS owner = Globals.TEAMS.NONE;
	public Globals.TEAMS baseOf = Globals.TEAMS.NONE;
	public TerritoryController[] Neighbours;
	public bool available {
		set {
			_available = value;
		}
		get {
			if(owner!=Globals.TEAMS.NONE) return false;
			return _available;
		}
	}

	private bool _available = false; 
	private cakeslice.Outline OutlineEffect;

	void Start() {
		OutlineEffect = transform.GetComponent<cakeslice.Outline>();
		OutlineEffect.enabled = false;
		owner = baseOf;
		if(baseOf!=Globals.TEAMS.NONE) {
			setColor(ColorScheme.getColorOf(ColorScheme.color_context.BASE, baseOf));
			GameObject fl = Instantiate(Globals.InstanceGame.flagPrefab, transform.GetComponent<Renderer>().bounds.center, Globals.InstanceGame.flagPrefab.transform.rotation);
			fl.GetComponent<FlagController>().setColor(ColorScheme.getColorOf(ColorScheme.color_context.FLAG, baseOf));
			foreach(TerritoryController tc in Neighbours) {
				tc.setColor(ColorScheme.getColorOf(ColorScheme.color_context.BORDER, owner));
				if(Globals.InstancePlayer.team==owner) tc.available = true;
			}
		}
	}

	void OnMouseEnter() {
		//Renderer rend = GetComponent<Renderer>();
		//rend.material.SetColor("_Color", color_selected);
		Globals.InstanceGame.hoverTerritory = this.transform;
		if(available) OutlineEffect.enabled = true;
	}

	void OnMouseExit() {
		//Renderer rend = GetComponent<Renderer>();
		//if(owner==Globals.TEAMS.NONE) rend.material.SetColor("_Color", color_default);
		//else rend.material.SetColor("_Color", color_claimed);
		Globals.InstanceGame.hoverTerritory = null;
		OutlineEffect.enabled = false;
	}

	public void setColor(Color c) {
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_Color", c);
	}

	public void setOwner(Globals.TEAMS owner) {
		this.owner = owner;
		switch(owner) {
		default : {
				GetComponent<Renderer>().material.SetColor("_Color", color_claimed);
				break;}
		}
	}

	public void UpdateForAllPlayers() {
		Globals.InstanceNetwork.SendToServer(MessageHandler.encode(getState())); //Send new TerritoryState to Server
	}

	public TerritoryState getState() {
		return new TerritoryState(this.gameObject.name, this.owner.ToString());
	}

	public void setState(TerritoryState ts) {
		setOwner(ts.owner);
	}

	public void addNeighbour(TerritoryController n) {
		TerritoryController[] newNArray = new TerritoryController[Neighbours.Length+1];
		for(int i=0; i<Neighbours.Length; i++) {
			if(n==Neighbours[i]) return; //Element already in list. Return
			newNArray[i] = Neighbours[i];
		}
		newNArray[newNArray.Length - 1] = n;
		Neighbours = newNArray;
	}
}
