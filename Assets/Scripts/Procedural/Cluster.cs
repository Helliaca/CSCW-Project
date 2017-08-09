using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster {

	public Vector2 position;
	public float measure;

	public Cluster() {
		position = new Vector2(0, 0);
	}

	public void generate(Vector2 origin, float radius) {
		position = UnityEngine.Random.insideUnitCircle;
		position *= radius;
		position += origin;
		measure = UnityEngine.Random.Range(-1.0f, 1.0f);
	}
}
