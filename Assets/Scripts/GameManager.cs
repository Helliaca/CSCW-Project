using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[HideInInspector]
	public Transform hoverTerritory = null;
	public Transform selectedTerritory = null;

	public Transform EntityMatchWindow;

	void Awake() {
		if(Globals.InstanceGame != null) Destroy(this);
		else Globals.InstanceGame = this;
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
