using UnityEngine;
using System.Collections;

public class GunAnimation: MonoBehaviour {

    private Animator _anim;
    private AudioSource _audio;
    public AudioClip CockingSound;
    public AudioClip CylinderClickSound;
    public AudioClip ShotSound;
    GameManager _gameManager;

    void Awake() {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    void Start() {
        _gameManager = GameManager.instance;
    }

    public void PlayCock() {
        _audio.PlayOneShot(CockingSound);
    }

    public void PlayCylinderClick() {
        _audio.PlayOneShot(CylinderClickSound);
    }

    public void PlayShot() {
        _audio.PlayOneShot(ShotSound);
    }

    public void Open() {
        _anim.SetTrigger("Open");
    }

    public void Close() {
        _anim.SetTrigger("Close");
    }

    public void Cock() {
        _anim.SetTrigger("Cock");
    }

    public void Shoot() {
        _anim.SetTrigger("Cock");
    }

    private static GunAnimation _instance;
    public static GunAnimation instance{
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<GunAnimation>();
            return _instance;
        }
    }

    void OnGUI() {
        Rect messageRect = new Rect(Screen.width/4f,Screen.height*0.8f,Screen.width,Screen.height);
        if (_gameManager.STATE == GameManager.GameState.Aiming) {
            GUI.Label(messageRect,"<size=30>AIM AND TAP TO FIRE</size>");
        }
    }
}
