using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadMap : MonoBehaviour {

	public Transform emwin;
	public GameObject map1, map2;
	public GameObject ProceduralGenerator;
	public GameObject HexagonPrefab;

	// Use this for initialization
	void Start () {
		//Globals.InstanceGame.EntityMatchWindow = emwin;

		if(Globals.selectedMap == Globals.MAPS.PROCEDURAL) {
			GameObject gen = Instantiate(ProceduralGenerator);
			GameObject origin = Instantiate(HexagonPrefab, Vector3.zero, Quaternion.identity);
			origin.name = "territory_origin";
			GameObject container = new GameObject("map_proc");
			origin.transform.SetParent(container.transform);
			gen.GetComponent<ProceduralHexGenerator>().GenerateFromSeed(Globals.proceduralMapSeed, origin.transform);
		}
		else {
			GameObject mapToSpawn;
			switch(Globals.selectedMap) {
				case Globals.MAPS.MAP1 : {mapToSpawn=map1; break;}
				case Globals.MAPS.MAP2 : {mapToSpawn=map2; break;}
				default : {mapToSpawn=map1; Globals.DevConsole.print("GLobals.SelectedMap not recognized. Using default map"); break;}
			}
			Instantiate(mapToSpawn, Vector3.zero, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
