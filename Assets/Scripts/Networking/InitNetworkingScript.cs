using UnityEngine;
using System.Collections;
using System;

public class InitNetworkingScript : MonoBehaviour {

	//public string ip = "172.20.10.13"; //my local IP
	public int port = 25001;
	public int maxConnections = 10;
	bool show = false;
	string ip = "";
	bool errorGUI = false;

	public void StartServer(){
		if(Network.peerType == NetworkPeerType.Disconnected){
			Network.InitializeServer(maxConnections,port, false);
			//Network.player.ipAddress
			// Application.LoadLevel(Application.loadedLevel+1);
		}
	}

	public void StopServer(){
      Network.Disconnect();
	}
	
	public void StartClient(string ip){
		this.ip = ip;
		if(Network.peerType == NetworkPeerType.Disconnected){
			Network.Connect(ip, port);
			show = true;
			Debug.Log("Starting client " + ip);
		}
	}
	
	// This call works
	void OnConnectedToServer(){
		show = false;
		Application.LoadLevel(Application.loadedLevel+1);
		Debug.Log("Loading level");
	}
	
	// But not this one?? :(
	void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Could not connect to server: " + error);
		
		errorGUI = true;
	}
//	void OnGUI(){
//		if(show){
//			GUI.Label(new Rect(10, Screen.height - Screen.height/20,300,35), "Trying to connect with: " + ip + "....");	
//		}
//		if(errorGUI){
//			GUI.Label(new Rect(200, Screen.height - Screen.height/20,300,35),"Could not connect :( : ");	
//		}
//
//	}

	

	
	
	/*[RPC]
	void AskForColor(){
		if(Network.isServer){
			networkView.RPC("ChangeColor", RPCMode.All);	
		}
	}
	
	[RPC]
	void ChangeColor(){
		target.renderer.material.color = Color.green;	
	}*/
	
		/*				GUI.Label(new Rect(100,100,100,25), "Server");
				GUI.Label(new Rect(100,125,100,25), "Connections: " + Network.connections.Length);
							if(Network.peerType == NetworkPeerType.Server){

				
				if(GUI.Button(new Rect(100,150,100,25), "Logout")){
					Network.Disconnect(250);
				}	
			}*/
		
}