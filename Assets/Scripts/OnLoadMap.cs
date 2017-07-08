using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadMap : MonoBehaviour {

	public Transform emwin;

	// Use this for initialization
	void Start () {
		Globals.Data.EntityMatchWindow = emwin;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
