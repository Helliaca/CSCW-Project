using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
		LobbyButton.gameObject.SetActive(Globals.InstanceNetwork.isConnected());
		if(Globals.InstanceNetwork.isHosting()) {
			Globals.InstancePlayer.setHost(true);
			serverRunningIndicator.gameObject.SetActive(true);
			startServerButton.interactable = false;
			joinServerButton.interactable = false;
			hostOverride.interactable = false;
			portOverride.interactable = false;
		}
		else {
			Globals.InstancePlayer.setHost(false);
			serverRunningIndicator.gameObject.SetActive(false);
			startServerButton.interactable = true;
			joinServerButton.interactable = true;
			hostOverride.interactable = true;
			portOverride.interactable = true;
		}
	}

	public void startServer() {
		Globals.InstanceNetwork.CreateServer();
		Globals.InstanceNetwork.ConnectToServer(); //Connect to own server
	}

	public void stopServer() {
		Globals.InstanceNetwork.StopServer();
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
		Globals.InstanceNetwork.ConnectToServer(host, port);
	}

	public void startGame() {
		if(Globals.InstancePlayer.isHost())
			Globals.InstanceNetwork.SendToServer(MessageHandler.encodeEvent("startGame"));
	}

	public void changeName() {
		Globals.InstancePlayer.name = playerName.text;
	}

}
