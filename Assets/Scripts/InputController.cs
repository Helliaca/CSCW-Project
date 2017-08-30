using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	void Update () {
		if(Input.GetButtonDown("Fire1") && !Globals.InstanceGame.EntityMatchWindow.gameObject.activeInHierarchy) {
			print("yo");
			Globals.InstanceGame.selectedTerritory = Globals.InstanceGame.hoverTerritory;
			if(Globals.InstanceGame.selectedTerritory) Globals.InstanceGame.EntityMatchWindow.gameObject.SetActive(true);
		}
	}
}
