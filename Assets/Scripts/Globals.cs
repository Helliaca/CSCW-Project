using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

	public enum TEAMS {NONE, RED, BLUE};

	public static Globals Data;

	[HideInInspector]
	public Transform hoverTerritory = null;
	public Transform selectedTerritory = null;

	public Transform EntityMatchWindow;

	void Awake()
	{
		if(Data != null) GameObject.Destroy(Data);
		else Data = this;
		DontDestroyOnLoad(this);
	}
		
	void Start () {
		
	}

	void Update () {
		
	}
}
