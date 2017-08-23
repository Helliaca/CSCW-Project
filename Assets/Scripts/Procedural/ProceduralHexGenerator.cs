using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class ProceduralHexGenerator : MonoBehaviour {

	public GameObject hexagonPrefab;
	public List<AxialCoordinate> occupied;

	private int radius, terrAmount, clusterAmount, RngSeed;

	List<Hexagon> queue;
	Cluster[] clusters;
	int counter = 0;

	public void GenerateFromSeed(string seed, Transform origin) {
		parseSeed(seed); //Set variables radius, terrAmount, clusterAmount and RngSeed from string
		UnityEngine.Random.InitState(RngSeed); //Initialize deterministic rng

		occupied = new List<AxialCoordinate>();
		clusters = new Cluster[clusterAmount];
		queue = new List<Hexagon>();
		for(int i=0; i<clusters.Length; i++) {
			clusters[i] = new Cluster();
			clusters[i].generate(new Vector2(origin.position.x, origin.position.z), radius);
		}

		Hexagon first = origin.GetComponent<Hexagon>();
		first.toPosition(new AxialCoordinate(0, 0, origin.position.y));
		occupied.Add(first.pos);
		queue.Add(first);

		for(int i=0; i<terrAmount; i++) placeNewHexagon();
	}

	private void parseSeed(string seed) {
		Regex reg = new Regex(@"\.");
		string[] components = reg.Split(seed);
		if(components.Length != 4) {
			Globals.DevConsole.print("Given seed is not valid. Proceeding with default one. Map seeds should have following format: Radius.Territories.Clusters.RngSeed");
			radius = 23;
			terrAmount = 200;
			clusterAmount = 29;
			RngSeed = -752244017;
		}
		else {
			radius = int.Parse(components[0]);
			terrAmount = int.Parse(components[1]);
			clusterAmount = int.Parse(components[2]);
			RngSeed = int.Parse(components[3]);
		}
	}

	void placeNewHexagon() {
		Hexagon current = getNext();
		foreach(AxialCoordinate.DIR dir in Enum.GetValues(typeof(AxialCoordinate.DIR))) {
			if(!isOccupied(current.pos.next(dir))) {
				queue.Add(current); //cause its not sated
				counter++;
				Hexagon newHex = (Instantiate(hexagonPrefab) as GameObject).GetComponent<Hexagon>();
				newHex.toPosition(current.pos.next(dir));
				occupied.Add(newHex.pos);
				queue.Add(newHex);
				return;
			}
		}
	}

	Hexagon getNext() {
		Hexagon ret=null;
		float maxMag = Mathf.NegativeInfinity;
		foreach(Hexagon h in queue) {
			if(h.getMagnitude(clusters)>maxMag) {
				maxMag = h.getMagnitude(clusters);
				ret = h;
			}
		}
		if(ret==null) Globals.DevConsole.print("QUEUE PROBLEM");
		queue.Remove(ret);
		return ret;
	}

	bool isOccupied(AxialCoordinate place) {
		foreach(AxialCoordinate c in occupied) {
			if(c.q == place.q && c.r == place.r) return true;
		}
		return false;
	}

	void OnDrawGizmos() {
		if(clusters==null) return;
		for(int i=0; i<clusters.Length; i++) {
			if(clusters[i]!=null) {
				if(clusters[i].measure<0) Gizmos.color = Color.red;
				else Gizmos.color = Color.gray;
				float radius = Mathf.Lerp(0.05f, 0.2f, Mathf.Abs(clusters[i].measure));
				Gizmos.DrawSphere(new Vector3(clusters[i].position.x, 0.3f, clusters[i].position.y), radius);
			}
		}
	}
}
