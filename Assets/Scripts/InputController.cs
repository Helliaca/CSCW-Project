using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	void Update () {
		if(Input.GetButtonDown("Fire1") && !Globals.Data.EntityMatchWindow.gameObject.activeInHierarchy) {
			Globals.Data.selectedTerritory = Globals.Data.hoverTerritory;
			if(Globals.Data.selectedTerritory) Globals.Data.EntityMatchWindow.gameObject.SetActive(true);
		}
	}
}
