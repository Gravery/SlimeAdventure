using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 15;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Bateu em: " + other);
        }
    }
}
