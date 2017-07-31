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

	public bool ConnectToServer(string host, int port) {
		if(socketReady) return false; //Already connected -> ignore

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
			return false;
		}
		return true;
	}

	public void Disconnect() {
		writer.Close();
		reader.Close();
		socket.Close();
		socketReady = false;
		Globals.DevConsole.print("Disconnected from Server");
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
		MessageHandler.handle(data);
	}

	public void Send(string data) {
		if(!socketReady) return;
		writer.WriteLine(data);
		writer.Flush();
	}


}
