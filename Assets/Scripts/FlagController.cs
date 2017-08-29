using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour {

	public void setColor(Color c) {
		transform.Find("Flag").GetComponent<Renderer>().material.SetColor("_Color", c);
	}
}
