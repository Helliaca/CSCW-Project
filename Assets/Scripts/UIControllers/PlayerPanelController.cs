using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelController : MonoBehaviour {
	private PlayerInfo pi;

	public Transform PlayerName;

	public void fromPlayerInfo(PlayerInfo player) {
		if(!PlayerName) PlayerName = transform.Find("PlayerName");
		this.pi = player;
		PlayerName.GetComponent<Text>().text = player.name;
	}

	public PlayerInfo getPlayerInfo() {
		return pi;
	}
}
