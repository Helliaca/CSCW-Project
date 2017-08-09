using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
	public float staticX = 0;
	public float amplitude = 0.005f;
	public float speed = 0.015f;
	public float waveAmount = 200;
	public float dampening = 0.1f;

	Vector3[] verts_original;
	Vector3[] verts_deformed;

	MeshFilter mesh;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>();
		verts_original = (Vector3[]) mesh.mesh.vertices.Clone();
		verts_deformed = (Vector3[]) verts_original.Clone();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i<verts_deformed.Length; i++) {
			float maxVal = amplitude * Mathf.Sin((Time.time*speed+verts_deformed[i].x)*waveAmount);
			verts_deformed[i].z = verts_original[i].z + Mathf.Lerp(0, maxVal, (Mathf.Abs(verts_deformed[i].x-staticX))/dampening);
		}
		mesh.mesh.vertices = verts_deformed;
		mesh.mesh.RecalculateNormals();
	}
}
