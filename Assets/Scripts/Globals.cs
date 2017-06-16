using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

	public static Globals Data;

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
