using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    Rigidbody2D rb;
    public int d = 1;

    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 15 * d;
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Bateu em: " + other);
        }
    }
    */
}
