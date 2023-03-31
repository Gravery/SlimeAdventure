using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialWindow : MonoBehaviour
{
    public GameObject window;
    private bool canMove = false;


    private void Update() {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
            canMove = true;
        }

        if(canMove){
            window.transform.position = new Vector3(window.transform.position.x+0.1f,window.transform.position.y,window.transform.position.z);
        }
    }
}
