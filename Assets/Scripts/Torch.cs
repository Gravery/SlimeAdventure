using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject flame;
    public bool isActive;

    void Start(){
        isActive = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other){   
        if(other.gameObject.CompareTag("Fireball")){
            flame.SetActive(true);
            isActive = true;
        }
    }
}
