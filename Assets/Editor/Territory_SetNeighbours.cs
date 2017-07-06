using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * This script automatically finds neighbours for territories and sets them based on the distance of their closest vertecies.
 * Attatch a Map_SetNeighbours.cs script to the root map Object and it will automatically add one of these to each child, remove them and finally self-destruct.
*/

[ExecuteInEditMode]
public class Territory_SetNeighbours : MonoBehaviour {

	public float scanDistance = 5f;		//Maximum distance from Territories global position that Other Territories are searched for
	public float neigDistance = 0.05f; 		//Maximum distance of closest vertecies at which another Territory is considered a neighbour

	private TerritoryController tc;

	void Start () {
		EditorUtility.DisplayProgressBar("SetNeighbours", gameObject.name, 0f);
		tc = transform.GetComponent<TerritoryController>();

		var hitColliders = Physics.OverlapSphere(transform.position, scanDistance);
		Mesh t_mesh = transform.GetComponent<MeshFilter>().sharedMesh;
		int progress = 0;

		foreach(Collider col in hitColliders) {
			progress++;
			EditorUtility.DisplayProgressBar("SetNeighbours", gameObject.name, (float)progress/hitColliders.Length);
			
			float minDistance = Mathf.Infinity;
			Mesh o_mesh = col.transform.GetComponent<MeshFilter>().sharedMesh;
			int vertCounter = 0;

			foreach(Vector3 vert in t_mesh.vertices) {

				foreach(Vector3 vert2 in o_mesh.vertices) {
					float distance = Vector3.Distance(transform.TransformPoint(vert), col.transform.TransformPoint(vert2));
					if(distance < minDistance) minDistance = distance;
				}
			}
			if(minDistance<neigDistance) addNeighbour(col.transform.GetComponent<TerritoryController>());
		}
		EditorUtility.ClearProgressBar();
		DestroyImmediate(this);
	}

	void addNeighbour(TerritoryController n) {
		List<TerritoryController> old = new List<TerritoryController>();
		old.AddRange(tc.Neighbours);
		if(!old.Contains(n) && n!=tc) old.Add(n);
		tc.Neighbours = old.ToArray();
	}
}
