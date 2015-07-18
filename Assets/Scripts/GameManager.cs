﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public enum GameState {
        Debug,
        Calibration,
        Preparation,
        Aiming,
        IWon,
        ILost
    }

    public GameState STATE = GameState.Aiming;

    public void DecideOutcome(bool pServerWasShot) {
        if (pServerWasShot) {
            if (Network.isServer) {
                STATE = GameState.ILost;
            }
            else {
                STATE = GameState.IWon;
            }
        }
        else {
            if (Network.isServer) {
                STATE = GameState.IWon;
            }
            else {
                STATE = GameState.ILost;
            }
        }
        StartCoroutine(DelayedRestart(3f));
    }

    public static GameManager instance{
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }
    void OnGUI(){
        if (STATE == GameState.IWon) {
            GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height),"<size=30>I won!</size>");
        }
        if (STATE == GameState.ILost) {
            GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height),"<size=30>I lost!</size>");
        }
    }

    IEnumerator DelayedRestart(float pDelay) {
        yield return new WaitForSeconds(pDelay);
        Debug.Log("Restarting");
        Network.Disconnect();
        Application.LoadLevel(0);
    }
}
