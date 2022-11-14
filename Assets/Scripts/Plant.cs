using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    Rigidbody2D rb;
    public int d = 1;
    public bool touchSomething;

    void Start()
    {
        touchSomething = false;
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 12 * d;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.gameObject.CompareTag("Player") && d < 0)
        {
            Destroy(gameObject,0.1f);
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            touchSomething = true;
        }
    }
}
