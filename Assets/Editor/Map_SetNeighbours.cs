using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script automatically attatches a Territory_SetNeighbours to all children of this object.
 * Attatch this script to the root map Object and wait.
 * WARNING: The script can easily take up a few minutes to execute.
*/

[ExecuteInEditMode]
public class Map_SetNeighbours : MonoBehaviour {

	void Start () {
		foreach(Transform child in transform) {
			child.gameObject.AddComponent<Territory_SetNeighbours>();
		}
		DestroyImmediate(this);
	}
}
