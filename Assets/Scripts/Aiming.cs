﻿using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour {

    private static Aiming _instance;

    public float CenterPitch = 2.5f;
    public float Aim;
    public bool InSights = false;
    Vector3 _targetDirection;

    AudioSource audio;

    GameManager _gameManager;

    Vector3 AimVector() {
        return Input.gyro.attitude * Vector3.right;
    }

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.pitch = CenterPitch;
        _targetDirection = AimVector();
        _gameManager = GameManager.instance;
    }

    void Update() {
        Aim = Vector3.Dot(_targetDirection, AimVector());
        if (_gameManager.STATE == GameManager.GameState.Aiming) {
            audio.pitch = Aim * CenterPitch;
            if (Aim > 0.95f) {
                Handheld.Vibrate();
                InSights = true;
            }
            else {
                InSights = false;
            }
        }
    }

    void OnGUI() {
            // GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height), "<size=30>"+(AimVector()).ToString() + " and " + Aim + "</size>");
        if (_gameManager.STATE == GameManager.GameState.Calibration) {
            GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height),"<size=30>Aim at opponent and hit Calibrate</size>");
            if(GUI.Button(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/4), "<size=40>Calibrate</size>".ToUpper())){
                _targetDirection = AimVector();
                // STATE = AimState.Aiming;
            }
        }
    }

    public static Aiming instance{
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<Aiming>();
            return _instance;
        }
    }
}
