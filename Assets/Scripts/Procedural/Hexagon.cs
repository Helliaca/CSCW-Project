using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hexagon : MonoBehaviour {

	public AxialCoordinate pos;
	public float size = 1.0f;

	void Start () {

	}

	void Update () {

	}

	public void toPosition(AxialCoordinate pos) {
		this.pos = pos;
		Vector2 worldPos = pos.toWorld(size);
		transform.position = new Vector3(worldPos.x, pos.y, worldPos.y);
	}

	public float getMagnitude(Cluster[] clusters) {
		float baseI = 0;

		//Get closest cluster distance
		float smallestDist = Mathf.Infinity;
		foreach(Cluster c in clusters) {
			float d = Vector2.Distance(c.position, new Vector2(transform.position.x, transform.position.z));
			if(d<smallestDist) smallestDist = d;
		}

		foreach(Cluster c in clusters) {
			float d = Vector2.Distance(c.position, new Vector2(transform.position.x, transform.position.z));
			//baseI += (smallestDist/d) * c.measure;
			baseI += c.measure/d;
		}
		return baseI;
		print(baseI);
	}
}
