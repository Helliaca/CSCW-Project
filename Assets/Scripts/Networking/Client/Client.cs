using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;
using System;

public class Client {
	
	private bool socketReady = false;
	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;

	public void ConnectToServer(string host = "127.0.0.1", int port = 6321) {
		if(socketReady) return; //Already connected -> ignore

		//Create the socket
		try {
			socket = new TcpClient(host, port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
		}
		catch (Exception e) {
			Globals.DevConsole.print("ERR: Could not create socket. " + e.Message);
		}
	}

	public bool isConnected() {
		return socketReady;
	}

	public void Update() {
		if(socketReady) {
			if(stream.DataAvailable) {
				string data = reader.ReadLine();
				if(data != null)
					OnIncomingData(data);
			}
		}
	}

	void OnIncomingData(string data) {
		Globals.DevConsole.print("Received data from server: " + data);
		MessageHandler.handle(data);
	}

	public void Send(string data) {
		if(!socketReady) return;
		writer.WriteLine(data);
		writer.Flush();
	}


}
