using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProceduralHexGenerator : MonoBehaviour {

	public Transform origin;
	public GameObject hexagonPrefab;
	public int max = 25;
	public float radius = 3;
	public List<AxialCoordinate> occupied;

	List<Hexagon> queue;
	Cluster[] clusters;
	int counter = 0;

	// Use this for initialization
	void Start () {

	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) Run();
	}

	void Run() {
		occupied = new List<AxialCoordinate>();
		clusters = new Cluster[10];
		queue = new List<Hexagon>();
		for(int i=0; i<clusters.Length; i++) {
			clusters[i] = new Cluster();
			clusters[i].generate(new Vector2(0,0), radius);
		}

		Hexagon first = origin.GetComponent<Hexagon>();
		first.toPosition(new AxialCoordinate(0,0,origin.position.y));
		occupied.Add(first.pos);
		queue.Add(first);

		for(int i=0; i<max; i++) placeNewHexagon();
	}

	void placeNewHexagon() {
		Hexagon current = getNext();
		foreach(AxialCoordinate.DIR dir in Enum.GetValues(typeof(AxialCoordinate.DIR))) {
			if(!isOccupied(current.pos.next(dir))) {
				queue.Add(current); //cause it snot sated
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
		if(ret==null) print("QUEUE PROBLEM");
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
				Gizmos.DrawSphere(new Vector3(clusters[i].position.x, 0, clusters[i].position.y), radius);
			}
		}
	}
}
