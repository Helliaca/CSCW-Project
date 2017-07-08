using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

	public enum TEAMS {NONE, RED, BLUE};

	public static Globals Data;
	public static Client InstanceClient;
	public static Server InstanceServer;
	public static bool InstanceIsHost = false;
	public static string InstancePlayerName = "Player";

	[HideInInspector]
	public Transform hoverTerritory = null;
	public Transform selectedTerritory = null;

	public Transform EntityMatchWindow;

	void Awake()
	{
		if(Data != null) GameObject.Destroy(Data);
		else Data = this;
		DontDestroyOnLoad(this);
	}
		
	void Start () {
		InstanceClient = new Client();
		InstanceServer = new Server();
	}

	void Update () {
		InstanceServer.Update();
		InstanceClient.Update();
		if(Input.GetKeyDown(KeyCode.S)) InstanceClient.Send("TESTING");
	}

	public void sendToAllPlayers(string data) {
		
	}
}
