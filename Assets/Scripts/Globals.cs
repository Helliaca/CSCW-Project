using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Globals : MonoBehaviour {

	public enum TEAMS {NONE, RED, BLUE, ORANGE, GREEN};

	public static PlayerInfo InstancePlayer;
	public static List<PlayerInfo> players;
	public static Globals Data;
	public static Client InstanceClient;
	public static Server InstanceServer;
	public static DevConsoleController DevConsole;
	public static bool InstanceIsHost = false;
	//public static string InstancePlayerName = "Player";

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
		InstancePlayer = new PlayerInfo();
		players = new List<PlayerInfo>();
		players.Add(InstancePlayer);
	}

	void Update () {
		InstanceServer.Update();
		InstanceClient.Update();
		if(Input.GetKeyDown(KeyCode.Comma)) DevConsole.toggle();
	}

	public static PlayerInfo getPlayerByName(string name) {
		foreach(PlayerInfo p in Globals.players) {
			if(p.name == name) return p;
		}
		return null;
	}

	public static void startGame() {
		if(Globals.InstancePlayer.state == PlayerInfo.STATUS.Ingame) {
			Globals.DevConsole.print("Requested start Game but Player is already ingame.");
			return;
		}
		Globals.InstancePlayer.state = PlayerInfo.STATUS.Ingame;
		SceneManager.LoadScene("Main");
	}
}
