using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    GameManager _gameManager;
    Renderer _renderer;
    Collider _collider;
    bool _chambered = false;
    Transform _chamber;

    // Use this for initialization
    void Start () {
        _gameManager = GameManager.instance;
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update () {
            if (_gameManager.STATE == GameManager.GameState.Opened) {
                _renderer.enabled = true;
                _collider.enabled = true;
            }
            if (_chambered) {
                transform.position = _chamber.transform.position;
                transform.rotation = _chamber.transform.rotation;
            }
            // else {
            //     _renderer.enabled = false;
            //     _collider.enabled = false;
            // }
        

    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Collided with something");
        if (other.tag == "chamber") {
            Destroy(GetComponent<Drag>());
            Debug.Log("Is is chamber " + other.gameObject.name);
            transform.position = other.transform.position;
            _collider.enabled = false;
            _chamber = other.transform;
            _chambered = true;

        }
    }
}
