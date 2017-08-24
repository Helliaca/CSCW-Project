using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DevConsoleController : MonoBehaviour {

	private InputField input;
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
		input = transform.Find("DevConsole/Input").GetComponent<InputField>();
		consoleContainer.SetActive(false);
		output_s = output.text;
	}

	void Update () {
		output.text = output_s;
	}

	void OnGUI() {
		if(input.isFocused && input.text != "" && Input.GetKey(KeyCode.Return)) {
			execute(input.text);
			input.text = "";
		}
	}

	public void execute(string command) {
		if(command == "showPlayers") {
			foreach(PlayerInfo p in Globals.players) {
				this.print(p.name + " :: " + p.team.ToString());
			}
		}
		else {
			this.print("Command not recognized: " + command);
		}
		input.Select();
		input.ActivateInputField();
	}

	public void print(string s) {
		//Debug.Log("CONSOLE:" + s);
		output_s += "\n> " + s;
	}

	public void show() {
		consoleContainer.SetActive(true);
		input.Select();
		input.ActivateInputField();
	}

	public void hide() {
		consoleContainer.SetActive(false);
	}

	public void toggle() {
		if(!consoleContainer.activeInHierarchy) show();
		else hide();
	}
}
