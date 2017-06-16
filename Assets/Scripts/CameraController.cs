using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float scrollBoundary = 50;
	public float scrollSpeed = 5;

	void Start () {

	}

	void Update() {
		if(Input.mousePosition.x > Screen.width - scrollBoundary) {
			transform.Translate(0, 0, scrollSpeed * Time.deltaTime);
		}
		if(Input.mousePosition.x < 0 + scrollBoundary) {
			transform.Translate(0, 0, -scrollSpeed * Time.deltaTime);
		}
		if(Input.mousePosition.y > Screen.height - scrollBoundary) {
			transform.Translate(-scrollSpeed * Time.deltaTime, 0, 0);
		}
		if(Input.mousePosition.y < 0 + scrollBoundary) {
			transform.Translate(scrollSpeed * Time.deltaTime, 0, 0);
		}
	}
}
