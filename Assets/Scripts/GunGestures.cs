using UnityEngine;
using System.Collections;

public class GunGestures : MonoBehaviour {

    GameManager _gameManager;
	// Use this for initialization
	void Start () {
      _gameManager = GameManager.instance;
	}

	void Update() {
       for (var i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                // Create a particle if hit
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    if (_gameManager.STATE == GameManager.GameState.Unprepared) {
                        if (hit.collider.gameObject.name == "CylinderCollider") {
                            GunAnimation.instance.Open();
                        }
                    }
                    if (_gameManager.STATE == GameManager.GameState.Filled) {
                        if (hit.collider.gameObject.name == "CylinderCollider") {
                            GunAnimation.instance.Close();
                        }
                    }
                    if (_gameManager.STATE == GameManager.GameState.Closed) {
                        if (hit.collider.gameObject.name == "HammerCollider") {
                            GunAnimation.instance.Cock();
                        }
                    }
                }
            }
        }
    }
}
