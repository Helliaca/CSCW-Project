using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float scrollBoundary = 50;
	public float scrollSpeed = 5;
	public float rotationSpeedHorizontal = 2.0f;
	public float rotationSpeedVertical = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	void Start () {

	}

	void Update() {
		if(Input.GetKey(KeyCode.Mouse1)) {
			yaw += rotationSpeedHorizontal * Input.GetAxis("Mouse X");
			pitch -= rotationSpeedVertical * Input.GetAxis("Mouse Y");
			transform.eulerAngles = new Vector3(0, yaw, pitch);
		}
		if(Input.mousePosition.x > Screen.width - scrollBoundary || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			transform.Translate(0, 0, scrollSpeed * Time.deltaTime);
		}
		if(Input.mousePosition.x < 0 + scrollBoundary || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			transform.Translate(0, 0, -scrollSpeed * Time.deltaTime);
		}
		if(Input.mousePosition.y > Screen.height - scrollBoundary || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			transform.Translate(-scrollSpeed * Time.deltaTime, 0, 0);
		}
		if(Input.mousePosition.y < 0 + scrollBoundary || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			transform.Translate(scrollSpeed * Time.deltaTime, 0, 0);
		}
	}
}
