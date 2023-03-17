using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerPosition : MonoBehaviour
{
    private bool isHere;

    public bool IsHere(){
        return isHere;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            isHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            isHere = false;
        }
    }
}
