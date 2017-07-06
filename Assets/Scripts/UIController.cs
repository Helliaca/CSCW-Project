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
		if(Globals.Data.selectedTerritory) {
			var tc = Globals.Data.selectedTerritory.GetComponent<TerritoryController>();
			tc.setOwner(Globals.TEAMS.RED);
			Globals.Data.EntityMatchWindow.gameObject.SetActive(false);
		}
		else Debug.Log("ERR: User submitted EM without selecting territory.");
	}
}
