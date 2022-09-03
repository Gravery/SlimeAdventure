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

    private void OnTriggerEnter2D(Collider2D other)
    {   
        
        if(other.gameObject.CompareTag("Player") && d < 0)
        {
            if(Vector3.Distance(other.gameObject.transform.position, transform.position)<1f)
            {
                Debug.Log(Vector3.Distance(other.gameObject.transform.position, transform.position));
                Destroy(gameObject);
            }
        }
        
    }
}
