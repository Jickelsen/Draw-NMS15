﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public SpriteRenderer WinSprite;
    public SpriteRenderer LoseSprite;
    public GameObject WhiteOut;
    float _soundDelay = 1f;
    float _soundTimer = 0f;
    bool _iWon = false;
    bool _iLost = false;

    public enum GameState {
        Debug,
        Calibration,
        WaitForCalibration,
        CountDown,
        Unprepared,
        Opened,
        Filled,
        Closed,
        Cocked,
        Aiming,
        IWon,
        ILost,
        GameOver
    }

    public GameState STATE = GameState.Calibration;

    public void GotoUnprepared() {
        STATE = GameState.Unprepared;
    }

    public void GotoOpened() {
        STATE = GameState.Opened;
    }

    public void GotoClosed() {
        STATE = GameState.Closed;
    }

    public void GotoAiming() {
        STATE = GameState.Aiming;
    }

    public void Update() {
        if (STATE == GameState.CountDown) {
            GunAnimation.instance.CountDown();
        }
        if (STATE == GameState.Filled) {
            GunAnimation.instance.Close();
        }
        if (STATE == GameState.IWon) {
            _iWon = true;
            WhiteOut.active = true;
            WinSprite.enabled = true;
            STATE = GameState.GameOver;
        }
        if (STATE == GameState.ILost) {
            _iLost = true;
            WhiteOut.active = true;
            LoseSprite.enabled = true;
            STATE = GameState.GameOver;
        }
        if (STATE == GameState.GameOver) {
            _soundTimer += Time.deltaTime;
            if (_soundTimer >= _soundDelay) {
                if (_iWon) {
                    GunAnimation.instance.PlayYouWin();
                    _iWon = false;
                }
                if (_iLost) {
                    GunAnimation.instance.PlayYouLose();
                    _iLost = false;
                }
            }
        }
    }

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
        Rect messageRect = new Rect(Screen.width/10f,Screen.height*0.4f,Screen.width,Screen.height);
        // if (STATE == GameState.IWon) {
        //     GUI.Label(messageRect, "<size=60>I won!</size>");
        // }
        // if (STATE == GameState.ILost) {
        //     GUI.Label(messageRect, "<size=60>I lost!</size>");
        // }
    }

    IEnumerator DelayedRestart(float pDelay) {
        yield return new WaitForSeconds(pDelay);
        Debug.Log("Restarting");
        Network.Disconnect();
        Application.LoadLevel(0);
    }

    [RPC]
    void DoneCalibrating () {

    }
}
