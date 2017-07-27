using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public InputField hostOverride;
	public InputField portOverride;
	public InputField playerName;
	public Button startServerButton;
	public Button joinServerButton;
	public Transform serverRunningIndicator;
	public Transform LobbyButton;

	// Use this for initialization
	void Start () {
		serverRunningIndicator.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		LobbyButton.gameObject.SetActive(Globals.InstanceClient.isConnected());
		if(Globals.InstanceServer.isActive()) {
			serverRunningIndicator.gameObject.SetActive(true);
			startServerButton.interactable = false;
			joinServerButton.interactable = false;
			hostOverride.interactable = false;
			portOverride.interactable = false;
		}
		else {
			serverRunningIndicator.gameObject.SetActive(false);
			startServerButton.interactable = true;
			joinServerButton.interactable = true;
			hostOverride.interactable = true;
			portOverride.interactable = true;
		}
	}

	public void startServer() {
		Globals.InstanceServer.Initialize();
		Globals.InstanceClient.ConnectToServer(); //Connect to own server
	}

	public void stopServer() {
		Globals.InstanceServer.ShutDown();
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

	public void changeName() {
		Globals.InstancePlayerName = playerName.text;
	}

}
