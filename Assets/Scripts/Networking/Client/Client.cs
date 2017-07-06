﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour {
	
	private bool socketReady = false;
	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;

	public void ConnectToServer() {
		//if already connected, ignore
		if(socketReady) return;

		//Default host and port values
		string host = "127.0.0.1";
		int port = 6321;

		//Get input host and port values
		string h;
		int p;
		h = GameObject.Find("HostInput").GetComponent<InputField>().text;
		if(h!="") host = h;
		int.TryParse(GameObject.Find("PortInput").GetComponent<InputField>().text, out p);
		if(p!=0) port = p;

		//Create the socket
		try {
			socket = new TcpClient(host, port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error: " + e.Message);
		}
	}

	void Update() {
		if(socketReady) {
			if(stream.DataAvailable) {
				string data = reader.ReadLine();
				if(data != null)
					OnIncomingData(data);
			}
		}
	}

	void OnIncomingData(string data) {
		Debug.Log("Server: " + data);
	}
}