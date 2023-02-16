using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject Camera;
    private Vector2 coordinates;

    void Start () {

    Camera = GameObject.Find("Main Camera");
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player")) {
            coordinates = transform.position;
            Camera.transform.position = new Vector3(coordinates.x, coordinates.y, Camera.transform.position.z);
        }
    }
}
