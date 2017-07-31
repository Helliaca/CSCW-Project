using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public static Client InstanceClient;
	public static Server InstanceServer;

	void Awake() {
		if(Globals.InstanceNetwork != null) Destroy(this);
		else Globals.InstanceNetwork = this;
		DontDestroyOnLoad(this);
	}
		
	void Start () {
		InstanceClient = new Client();
		InstanceServer = new Server();
	}

	void Update () {
		InstanceServer.Update();
		InstanceClient.Update();
	}


	public bool isConnected() {
		return InstanceClient.isConnected();
	}

	public bool isHosting() {
		return InstanceServer.isActive();
	}

	public void DisconnectFromServer() {
		if(isHosting())
			Globals.DevConsole.print("WARNING: Client disconnected from his own server.");
		InstanceClient.Disconnect();
	}

	public void StopServer() {
		DisconnectFromServer();
		InstanceServer.ShutDown();
	}

	public void ConnectToServer(string host = "127.0.0.1", int port = 6321) {
		if(!InstanceClient.ConnectToServer(host, port)) 
			Globals.DevConsole.print("Failed to Connect to Server: " + host + ":" + port);
		else
			Globals.DevConsole.print("Connection Established to Server: " + host + ":" + port);
	}

	public void CreateServer() {
		if(InstanceServer.Initialize()) {
			ConnectToServer("127.0.0.1", InstanceServer.getPort());
		}
	}

	public void SendToServer(string msg) {
		InstanceClient.Send(msg);
	}

	void makeConnection() {
		/*
		 * Connect to server		Accept connection
		 * wait for new id->drop	send them unqiue id
		 * set new id				Hold connection -> drop if nothing
		 * send id playername		Confirm new player connected
		 */
	}
}
