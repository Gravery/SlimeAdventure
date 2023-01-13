using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Camera;
    public float x;
    public float y;


    private float targetX, targetY;

    void Start () {

    Camera = GameObject.Find("Main Camera");
    }

    void OnTriggerEnter2D(Collider2D other) {

    if(other.CompareTag("Player")) {
        targetX = Camera.transform.position.x + x;
        targetY = Camera.transform.position.y + y;
        Camera.transform.position = new Vector3(targetX, targetY, Camera.transform.position.z);
        transform.position -= new Vector3(x/10, y/10, transform.position.z);
        x = -x;
        y = -y;
    
    }
    }
}
