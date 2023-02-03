using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectProjectile : MonoBehaviour
{
    private Vector2 projectileDirection;
    public bool detected = false;
    private Vector2 position;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Fireball")){
            Rigidbody2D rb;
            rb = other.GetComponent<Rigidbody2D>();
            if(rb){
                detected = true;
                projectileDirection = new Vector2(rb.velocity.x, rb.velocity.y).normalized;
                position = new Vector2(other.transform.position.x, other.transform.position.y);
            }
        }
    }

    public Vector2 GetDirection(){
        return projectileDirection;
    }

    public Vector2 GetPosition(){
        return position;
    }
}
