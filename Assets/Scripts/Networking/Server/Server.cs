﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;

public class Server {

	private List<ServerClient> clients;
	private List<ServerClient> disconnectList;
	private TcpListener server;
	private bool serverStarted;

	public int port = 6321;


	public void Initialize(int port = 6321) {
		this.port = port;
		clients = new List<ServerClient>();
		disconnectList = new List<ServerClient>();

		try {
			server = new TcpListener(IPAddress.Any, port);
			server.Start();

			StartListening();
			serverStarted = true;
			Globals.DevConsole.print("Server started on port " + port);
		} catch (Exception e) {
			Globals.DevConsole.print("Socket error: " + e.Message);
		}
	}

	public void ShutDown() {
		try {
			server.Stop();
			serverStarted = false;
			Globals.DevConsole.print("Server shut down");
		} catch (Exception e) {
			Globals.DevConsole.print("Socket error: " + e.Message);
		}
	}

	public bool isActive() {
		return serverStarted;
	}

	public void Update() {
		if(!serverStarted) return;

		foreach(ServerClient c in clients) {
			//Is client still conected?
			if(!isConnected(c.tcp)) {
				c.tcp.Close();
				disconnectList.Add(c);
				continue;
			}
			// check messages from client
			else {
				NetworkStream s = c.tcp.GetStream();
				if(s.DataAvailable) {
					//We got a message from him!
					StreamReader reader = new StreamReader(s, true);
					string data = reader.ReadLine();

					if(data!=null) OnIncomingData(c, data);
				}
			}
		}
	}


	void StartListening() {
		server.BeginAcceptTcpClient(AcceptTcpClient, server);
	}


	void AcceptTcpClient(IAsyncResult ar) {
		TcpListener listener = (TcpListener)ar.AsyncState;
		clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
		StartListening();

		//Send message to everyone that someone connected
		Broadcast(MessageHandler.encode(clients[clients.Count-1].clientName + " has connected"), clients);
	}


	bool isConnected(TcpClient c) {
		try {
			if(c!=null && c.Client!=null && c.Client.Connected) {
				if(c.Client.Poll(0, SelectMode.SelectRead)) {
					return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
				}
				return true;
			}
		} catch {
			return false;
		}
		return false;
	}


	void OnIncomingData(ServerClient c, string data) {
		Globals.DevConsole.print("Data recieved:: " + c.clientName + ": " + data);
		Broadcast(data, clients);
	}

	void Broadcast(string data, List<ServerClient> cl) {
		Globals.DevConsole.print("Server: Broadcasting data:: " + data);
		foreach(ServerClient c in cl) {
			try {
				StreamWriter writer = new StreamWriter(c.tcp.GetStream());
				writer.WriteLine(data);
				writer.Flush();
			}
			catch (Exception e) {
				Globals.DevConsole.print("Write error: " +  e.Message + " to client " + c.clientName);
			}
		}
	}

}



public class ServerClient {
	public TcpClient tcp;
	public string clientName;

	public ServerClient(TcpClient clientSocket) {
		clientName = "Guest";
		tcp = clientSocket;
	}
}
