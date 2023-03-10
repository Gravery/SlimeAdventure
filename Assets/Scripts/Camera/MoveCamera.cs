using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject Camera;
    private Vector2 coordinates;
    private bool isPlayerHere;

    void Start () {
        isPlayerHere = false;
        Camera = GameObject.Find("Main Camera");
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player")) {
            coordinates = transform.position;
            isPlayerHere = true;
            Camera.transform.position = new Vector3(coordinates.x, coordinates.y, Camera.transform.position.z);
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        if(other.CompareTag("Player")) {
            isPlayerHere = false;
        }
    }

    public bool IsPlayerHere(){
        return isPlayerHere;
    }
}
