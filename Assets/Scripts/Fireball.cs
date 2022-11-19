using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    private float lifeTime = 1f;
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 15;


        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Torch") )
        {
            Destroy(gameObject);
            //Debug.Log("Bateu em: " + other);
        }
    }
}
