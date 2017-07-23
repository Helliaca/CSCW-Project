using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPanelController : MonoBehaviour {
	private List<PlayerInfo> players;
	private List<GameObject> playerPanels;

	public Globals.TEAMS team;
	public Transform joinButton;
	public GameObject playerPanelPrefab;

	void Awake() {
		playerPanels = new List<GameObject>();
		players = new List<PlayerInfo>();
	}

	void OnEnable () {
		UpdatePlayerList();
	}

	void FixedUpdate() {
		UpdatePlayerList();
	}

	public void joinTeam() {
		Globals.InstanceClient.Send(MessageHandler.encode(team));
	}

	void UpdatePlayerList() {
		foreach(PlayerInfo p in Globals.players) {
			if(p.team == this.team && !players.Contains(p)) addPlayer(p);
			else if(p.team != this.team && players.Contains(p)) removePlayer(p);
		}
	}

	void addPlayer(PlayerInfo p) {
		if(p.team != team) {
			Globals.DevConsole.print("ERR: Requested adding player to TeamPanel, but TeamPanel does not correspond with player team.");
			return;
		}
		if(players.Contains(p)) return;
		players.Add(p);
		GameObject go = Instantiate(playerPanelPrefab, transform);
		go.GetComponent<PlayerPanelController>().fromPlayerInfo(p);
		playerPanels.Add(go);
		if(joinButton) joinButton.SetAsLastSibling();
	}

	void removePlayer(PlayerInfo p) {
		if(p.team == team) {
			Globals.DevConsole.print("ERR: Requested removing player from TeamPanel, but TeamPanel still corresponds with player team.");
			return;
		}
		if(!players.Contains(p)) {
			Globals.DevConsole.print("ERR: Requested removing player from TeamPanel, but TeamPanel coes not include player.");
			return;
		}
		players.Remove(p);
		for(int i = playerPanels.Count-1; i>=0; i--) {
			if(playerPanels[i].GetComponent<PlayerPanelController>().getPlayerInfo() == p) {
				Destroy(playerPanels[i]);
				playerPanels.RemoveAt(i);
			}
		}
	}
}
