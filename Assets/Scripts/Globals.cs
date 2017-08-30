using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Globals : MonoBehaviour {

	public enum TEAMS {NONE, RED, BLUE, ORANGE, GREEN};
	public enum MAPS {MAP1, MAP2, PROCEDURAL};

	public static PlayerInfo InstancePlayer;
	public static List<PlayerInfo> players;
	public static Globals Data;
	public static NetworkManager InstanceNetwork;
	public static GameManager InstanceGame;
	public static DevConsoleController DevConsole;
	public static bool InstanceIsHost = false;

	public static string proceduralMapSeed = "1";
	public static MAPS selectedMap = MAPS.MAP1;

	void Awake()
	{
		if(Data != null) GameObject.Destroy(Data);
		else Data = this;
		DontDestroyOnLoad(this);
	}
		
	void Start () {
		InstancePlayer = new PlayerInfo();
		players = new List<PlayerInfo>();
		players.Add(InstancePlayer);
	}

	void Update () {
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

	public static int getUniquePlayerId() {
		List<int> ids = new List<int>();
		foreach(PlayerInfo pi in players) ids.Add(pi.id);
		int id = 0;
		while(ids.Contains(id)) id++;
		return id;
	}
}
