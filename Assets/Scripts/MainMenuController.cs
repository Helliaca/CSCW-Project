using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void startServer() {
		Globals.InstanceServer.Initialize();
		//Globals.InstanceClient.ConnectToServer(); //Connect to own server
	}

	public void connectToServer() {
		string host = "127.0.0.1";
		int port = 6321;
		string h;
		int p;
		h = GameObject.Find("HostInput").GetComponent<InputField>().text;
		if(h!="") host = h;
		int.TryParse(GameObject.Find("PortInput").GetComponent<InputField>().text, out p);
		if(p!=0) port = p;
		Globals.InstanceClient.ConnectToServer(host, port);
	}

	public void startGame() {
		SceneManager.LoadScene("Main");
	}


}
