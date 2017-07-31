using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void submitEM() {
		if(Globals.InstanceGame.selectedTerritory) {
			var tc = Globals.InstanceGame.selectedTerritory.GetComponent<TerritoryController>();
			tc.setOwner(Globals.TEAMS.RED);
			tc.UpdateForAllPlayers();
			Globals.InstanceGame.EntityMatchWindow.gameObject.SetActive(false);
		}
		else Globals.DevConsole.print("User submitted EM without selecting territory.");
	}
}
