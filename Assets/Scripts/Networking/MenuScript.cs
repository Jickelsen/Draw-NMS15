using UnityEngine;
using System.Collections;


public class MenuScript : MonoBehaviour {
	
    // Variables
    public GUISkin menuSkin;
    public InitNetworkingScript networking;
    string ip = string.Empty;
    public SpriteRenderer Background;

    void Start() {
			  ip = (PlayerPrefsX.GetStringArray("IP", "192.168.1.227", 1))[0];
        networking = GetComponent<InitNetworkingScript>();
        // ip = "10.155.22.41";
    }

    void OnPlayerConnected() {
        string[] stringArray = {ip};
			  PlayerPrefsX.SetStringArray("IP", stringArray);
        Application.LoadLevel(1);
    }

    void OnGUI() {
        // GUI.color = Color.black;
        //	GUI.skin = menuSkin;
        GUI.skin.textField.fontSize = 25;
        if (!Network.isServer) {
            Background.enabled = true;
            ip = GUI.TextField(new Rect(Screen.width*0.53f, Screen.height - Screen.height/7, 200, 60), ip);
            GUI.Label(new Rect(Screen.width*0.3f, Screen.height - Screen.height/7, 300, 50), "<size=30>IP-adress</size>");

            if(GUI.Button(new Rect(Screen.width*0.03f, Screen.height*0.45f, Screen.width*0.4f, Screen.height/4), "".ToUpper(), GUIStyle.none)){
                networking.StartServer();
            }

            if(GUI.Button(new Rect(Screen.width*0.61f, Screen.height*0.45f, Screen.width*0.4f, Screen.height/4), "".ToUpper(), GUIStyle.none)){
                networking.StartClient(ip);
            }
        }
        if (Network.isServer) {
            Background.enabled = false;
            GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height),"<size=30>Connect to me with : " + Network.player.ipAddress + "</size>");
            if(GUI.Button(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/4), "<size=40>Back</size>".ToUpper())){
                networking.StopServer();
            }
        }
    }
}
