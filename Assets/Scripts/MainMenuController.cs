using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public InputField hostOverride;
	public InputField portOverride;
	public Button startServerButton;
	public Button joinServerButton;
	public Transform serverRunningIndicator;

	// Use this for initialization
	void Start () {
		serverRunningIndicator.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Globals.InstanceServer.isActive()) {
			serverRunningIndicator.gameObject.SetActive(true);
			startServerButton.enabled = false;
			joinServerButton.enabled = false;
		}
		else {
			serverRunningIndicator.gameObject.SetActive(false);
			startServerButton.enabled = true;
			joinServerButton.enabled = true;
		}
	}

	public void startServer() {
		Globals.InstanceServer.Initialize();
		Globals.InstanceClient.ConnectToServer(); //Connect to own server
		serverRunningIndicator.gameObject.SetActive(true);
		startServerButton.enabled = false;
		joinServerButton.enabled = false;
	}

	public void connectToServer() {
		string host = "127.0.0.1";
		int port = 6321;
		string h;
		int p;
		h = hostOverride.text;
		if(h!="") host = h;
		int.TryParse(portOverride.text, out p);
		if(p!=0) port = p;
		Globals.InstanceClient.ConnectToServer(host, port);
	}

	public void startGame() {
		SceneManager.LoadScene("Main");
	}


}
