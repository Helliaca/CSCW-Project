using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryController : MonoBehaviour {

	private static Color color_default = new Color(0.7647f, 0.7647f, 0.7647f, 1f);
	private static Color color_selected = new Color(0.854f, 0.814f, 0.814f, 0.5f);
	//private static Color color_claimed = new Color(1f, 0.5f, 0.5f, 1f);

	public TerritoryController[] Neighbours;

	void OnMouseEnter() {
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_Color", color_selected);
		Globals.Data.selectedTerritory = this.transform;
	}

	void OnMouseExit() {
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_Color", color_default);
		Globals.Data.selectedTerritory = null;
	}
}
