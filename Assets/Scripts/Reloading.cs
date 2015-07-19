using UnityEngine;
using System.Collections;

public class Reloading : MonoBehaviour {

    public int FilledChambers = 0;
    public Collider[] Chambers = new Collider[6];
    GameManager _gameManager;

    // Use this for initialization
    void Start () {
        _gameManager = GameManager.instance;
    }

	// Update is called once per frame
    void Update () {
        int count = 0;
        if (_gameManager.STATE == GameManager.GameState.Opened) {
            for(int i = 0; i < Chambers.Length; i++){
                    if (Chambers[i] == null)
                    {
                        count++;
                    }
                }
            // foreach (Collider coll in Chambers) {
            //     if (coll == null) {
            //         i++;
            //     }
            // }
            FilledChambers = count;
            if (count == 6) {
                _gameManager.STATE = GameManager.GameState.Filled;
            }
        }
    }
}
