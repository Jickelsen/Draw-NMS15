using UnityEngine;
using System.Collections;

public class GyroRotation : MonoBehaviour {

    Vector3 AimVector() {
        return Input.gyro.attitude * Vector3.right;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        transform.rotation = Input.gyro.attitude;
    }
    void LateUpdate() {
        // transform.rotation = Quaternion.AngleAxis(-90f, transform.forward);
    }
}
