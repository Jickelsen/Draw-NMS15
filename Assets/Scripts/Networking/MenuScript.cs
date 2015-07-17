using UnityEngine;
using System.Collections;


public class MenuScript : MonoBehaviour {
	
    // Variables
    public GUISkin menuSkin;
    public InitNetworkingScript networking;
    string ip = string.Empty;

    void Start() {
        networking = GetComponent<InitNetworkingScript>();
        ip = "10.155.22.41";
    }

    void OnPlayerConnected() {
        Application.LoadLevel(1);
    }

    void OnGUI() {
        // GUI.color = Color.black;
        //	GUI.skin = menuSkin;
        GUI.skin.textField.fontSize = 25;
        if (!Network.isServer) {
            ip = GUI.TextField(new Rect(Screen.width/2-Screen.width/10, Screen.height - Screen.height/7, 200, 60), ip);
            GUI.Label(new Rect(Screen.width/2-Screen.width/10, Screen.height - Screen.height/4, 300, 50), "<size=30>IP-adress</size>");

            if(GUI.Button(new Rect(Screen.width/4, 0, Screen.width/2, Screen.height/4), "<size=40>Server</size>".ToUpper())){
                networking.StartServer();
            }

            if(GUI.Button(new Rect(Screen.width/4, Screen.height/3, Screen.width/2, Screen.height/4), "<size=40>Client</size>".ToUpper())){
                networking.StartClient(ip);
            }
        }
        if (Network.isServer) {
            GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height),"<size=30>Connect to me with : " + Network.player.ipAddress + "</size>");
            if(GUI.Button(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/4), "<size=40>Back</size>".ToUpper())){
                networking.StopServer();
            }
        }
    }
}
