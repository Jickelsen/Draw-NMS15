using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour {

    private static Aiming _instance;

    public float CenterPitch = 2.5f;
    public float Aim;
    public bool InSights = false;
    Vector3 _targetDirection;
    int _calibrationCount = 0;
    bool _playing = false;

    AudioSource audio;

    GameManager _gameManager;

    Vector3 AimVector() {
        return Input.gyro.attitude * Vector3.right;
    }

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.pitch = 1f;
        _targetDirection = AimVector();
        _gameManager = GameManager.instance;
    }

    void Update() {
        Aim = Vector3.Dot(_targetDirection, AimVector());
        if (_gameManager.STATE == GameManager.GameState.Aiming) {
            if (!_playing) {
                audio.Play();
                _playing = true;
            }
            audio.pitch = Aim * CenterPitch;
            if (Aim > 0.95f) {
                Handheld.Vibrate();
                InSights = true;
            }
            else {
                InSights = false;
            }
        }
        else {
            audio.Stop();
            _playing = false;
        }
    }

    void OnGUI() {
            // GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height), "<size=30>"+(AimVector()).ToString() + " and " + Aim + "</size>");
        if (_gameManager.STATE == GameManager.GameState.Calibration) {
            if(GUI.Button(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/4), "<size=40>Calibrate</size>".ToUpper())){
                _targetDirection = -AimVector();
                GetComponent<NetworkView>().RPC("ICalibrated",RPCMode.All);
                _gameManager.STATE = GameManager.GameState.WaitForCalibration;
                // _gameManager.STATE = GameManager.GameState.Unprepared;
            }
        }
        if (_gameManager.STATE == GameManager.GameState.WaitForCalibration) {
            if (_calibrationCount == 2) {
                GameManager.instance.STATE = GameManager.GameState.CountDown;
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

    [RPC]
    void ICalibrated() {
        _calibrationCount++;
    }
}
