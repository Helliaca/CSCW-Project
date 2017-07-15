using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Transform[] panels;

	public void showPanel(Transform r) {
		foreach(Transform t in panels) {
			t.gameObject.SetActive(false);
			if(t==r) t.gameObject.SetActive(true);
		}
	}
}
