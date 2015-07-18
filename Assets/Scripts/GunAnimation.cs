using UnityEngine;
using System.Collections;

public class GunAnimation: MonoBehaviour {

    private Animator _anim;
    private AudioSource _audio;
    public AudioClip YouWin;
    public AudioClip YouLose;
    public AudioClip CountDownOne;
    public AudioClip CountDownTwo;
    public AudioClip CountDownThree;
    public AudioClip Draw;
    public AudioClip CockingSound;
    public AudioClip CylinderClickSound;
    public AudioClip ShotSound;
    GameManager _gameManager;
    bool _countDown = false;

    void Awake() {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    void Start() {
        _gameManager = GameManager.instance;
    }

    public void PlayYouWin() {
        _audio.PlayOneShot(YouWin);
    }

    public void PlayYouLose() {
        _audio.PlayOneShot(YouLose);
    }

    public void PlayCountDownOne() {
        _audio.PlayOneShot(CountDownOne);
    }

    public void PlayCountDownTwo() {
        _audio.PlayOneShot(CountDownTwo);
    }

    public void PlayCountDownThree() {
        _audio.PlayOneShot(CountDownThree);
    }

    public void PlayDraw() {
        _audio.PlayOneShot(Draw);
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

    public void CountDown() {
        if (!_countDown) {
            _anim.SetTrigger("CountDown");
            _countDown = true;
        }
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
        if (_gameManager.STATE == GameManager.GameState.CountDown) {
            GUI.Label(messageRect,"<size=40>Take three steps and turn around!</size>");
        }
        if (_gameManager.STATE == GameManager.GameState.Unprepared) {
            GUI.Label(messageRect,"<size=40>Tap cylinder!</size>");
        }
        if (_gameManager.STATE == GameManager.GameState.Opened) {
            GUI.Label(messageRect,"<size=40>Drag in ALL the bullets!</size>");
        }
        if (_gameManager.STATE == GameManager.GameState.Closed) {
            GUI.Label(messageRect,"<size=40>Tap the hammer!</size>");
        }
        if (_gameManager.STATE == GameManager.GameState.Aiming) {
            GUI.Label(messageRect,"<size=40>AIM AND TAP TO FIRE</size>");
        }
    }
}
