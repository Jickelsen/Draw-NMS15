using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour {

    private static Aiming _instance;

    public float CenterPitch = 2.5f;
    public float Aim;
    Vector3 _targetDirection;

    AudioSource audio;

    public enum AimState {
        Debug,
        Calibrating,
        Preparing,
        Aiming
    }

    public AimState STATE = AimState.Aiming;

    void Start() {
        audio = GetComponent<AudioSource>();
        audio.pitch = CenterPitch;
        _targetDirection = Input.gyro.attitude * Vector3.forward;
    }
    
    void Update() {
        Vector3 gyroAttitude = Input.gyro.attitude * Vector3.forward;
        Aim = Vector3.Dot(_targetDirection, gyroAttitude);
        if (STATE == AimState.Aiming) {
            audio.pitch = Aim * CenterPitch;
            if (Aim > 0.9f) {
                Handheld.Vibrate();
            }
        }
    }

    void OnGUI() {
        if (STATE == AimState.Debug) {
            GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height), (Input.gyro.attitude * Vector3.forward).ToString() + " and " + Aim);
        }
        if (STATE == AimState.Calibrating) {
            GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height),"<size=30>Aim at opponent and hit Calibrate</size>");
            if(GUI.Button(new Rect(Screen.width/4, Screen.height/2, Screen.width/2, Screen.height/4), "<size=40>Calibrate</size>".ToUpper())){
                _targetDirection = Input.gyro.attitude * Vector3.forward;
                STATE = AimState.Aiming;
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
