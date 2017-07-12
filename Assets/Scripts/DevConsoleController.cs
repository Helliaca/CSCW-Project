using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevConsoleController : MonoBehaviour {

	private Text output;
	private GameObject consoleContainer;
	private string output_s;

	void Awake()
	{
		DontDestroyOnLoad(transform);
	}

	void Start () {
		Globals.DevConsole = this;
		consoleContainer = transform.Find("DevConsole").gameObject;
		output = transform.Find("DevConsole/Output").GetComponent<Text>();
		consoleContainer.SetActive(false);
		output_s = output.text;
	}

	void Update () {
		output.text = output_s;
	}

	public void print(string s) {
		//Debug.Log("CONSOLE:" + s);
		output_s += "\n> " + s;
	}

	public void show() {
		consoleContainer.SetActive(true);
	}

	public void hide() {
		consoleContainer.SetActive(false);
	}

	public void toggle() {
		consoleContainer.SetActive(!consoleContainer.activeInHierarchy);
	}
}
