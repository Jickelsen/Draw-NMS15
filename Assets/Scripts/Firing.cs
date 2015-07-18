using UnityEngine;
using System.Collections;

public class Firing : MonoBehaviour {

    GameManager _gameManager;
    double _fireTime = 0;
    bool _wasHit = false;
    bool _firing = false;

    void Start() {
        _gameManager = GameManager.instance;
    }

    void Fire() {
        Debug.Log("Firing");
        if (Aiming.instance.InSights) {
            GunAnimation.instance.Shoot();
            _fireTime = Network.time;
            Debug.Log("Hit with networktime " + _fireTime);
            if (Network.isServer) {
                GetComponent<NetworkView>().RPC("Shot", RPCMode.Others);
            }
            if (Network.isClient) {
                GetComponent<NetworkView>().RPC("Shot", RPCMode.Server);
            }
        }
        else {
            // Play ricochet sound
        }
    }

    [RPC]
    void Shot(NetworkMessageInfo info) {
        Debug.Log("Received fire time " + info.timestamp);
        if (_fireTime == 0) {
            Debug.Log("I was shot before I had the time to fire");
            // I was shot before I had the time to fire
            ILost();
        }
        else {
            // We need to resolve a conflict, check timestamps on bullets
            Debug.Log("Comparison: " + _fireTime + " vs " + info.timestamp);
            if (_fireTime < info.timestamp) {
                // I shot!
            }
            if (_fireTime >= info.timestamp) {
                Debug.Log("I was shot!");
                // I was shot!
                ILost();
            }
        }
    }

    void ILost() {
        if (Network.isServer) {
            Debug.Log("Sending local RPC call that server was shot");
            GetComponent<NetworkView>().RPC("Outcome", RPCMode.All, true);
        }
        if (Network.isClient) {
            Debug.Log("Sending RPC call to server that client was shot");
            GetComponent<NetworkView>().RPC("Outcome", RPCMode.All, false);
        }
    }

    [RPC]
    void Outcome(bool pServerWasShot) {
        Debug.Log("Server was shot? " + pServerWasShot);
        GameManager.instance.DecideOutcome(pServerWasShot);
    }

    void Update () {
        if (_gameManager.STATE == GameManager.GameState.Aiming) {
            for (var i = 0; i < Input.touchCount; ++i) {
                if (Input.GetTouch(i).phase == TouchPhase.Began) {
                    _firing = true;
                    Fire();
                    // // Construct a ray from the current touch coordinates
                    // Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    // // Create a particle if hit
                    // if (Physics.Raycast(ray))
                    //     Instantiate(particle, transform.position, transform.rotation);
                }
            }
            if (Input.GetMouseButtonDown(0)) {
                        _firing = true;
                        Fire();
            }
            else {
                    _firing = false;
            }

        }
    }

    // void OnGUI() {
    //         GUI.Label(new Rect(Screen.width/4f,Screen.height/3f,Screen.width,Screen.height),"<size=30>" + _firing + "</size>");
    // }
}

    // void Fire() {
    //     if (Aiming.instance.InSights && _fireTime == null) {
    //         _firetime = Network.time;
    //         if (Network.isServer) {
    //             if (!_wasHit)
    //                 GetComponent<NetworkView>().RPC("Shot", RPCMode.Others, _fireTime);
    //             else
    //                 GetComponent<NetworkView>().RPC("IWasShot ", RPCMode.Others, Network.time);
    //         }
    //         if (Network.isClient) {
    //             if (!_wasHit)
    //                 GetComponent<NetworkView>().RPC("Shot", RPCMode.Server, _fireTime);
    //             else
    //                 GetComponent<NetworkView>().RPC("IWasShot ", RPCMode.Server, Network.time);
    //         }
    //     }
    //     else
    //         // Play ricochet sound
    // }