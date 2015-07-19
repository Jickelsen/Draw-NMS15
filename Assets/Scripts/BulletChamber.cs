using UnityEngine;
using System.Collections;

public class BulletChamber : MonoBehaviour {

    GameManager _gameManager;
    Collider _collider;
    bool _full = false;

    // Use this for initialization
    void Start () {
        _gameManager = GameManager.instance;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if (!_full) {
                _collider.enabled = true;
        }
        else {
            // _collider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Collided with something");
        if (other.tag == "bullet") {
            Debug.Log("Is is bullet " + other.gameObject.name);
            GunAnimation.instance.PlayInsertBullet();
            _full = true;
            Destroy(GetComponent<Collider>());
        }
    }
}
