using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SandWhirlpool : MonoBehaviour
{
    [SerializeField]
    private Transform center;
    [SerializeField]
    private float debuff;

    public int undergroundScene;

    private Vector3 direction;
    private Rigidbody2D rb;
    public float force;
    private PlayerMovement pm;

    public float maxLoadTeleport;
    private float load = 0;

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            direction = center.position - other.transform.position;
            Vector2 velocity = new Vector2(direction.x,direction.y).normalized;

            rb = other.GetComponent<Rigidbody2D>();
            rb.AddForce(velocity * force);

            pm = other.GetComponent<PlayerMovement>();
            pm.debuff = debuff;

            //Teleport(direction);
            Damage(direction, other.GetComponent<Life>());
        }    
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            pm = other.GetComponent<PlayerMovement>();
            pm.debuff = 0f;
        }    
    }

    void Teleport(Vector3 distance){
        if(Math.Abs(distance.x)< 0.05 && Math.Abs(distance.y) < 0.05){
            if(load < maxLoadTeleport) load += Time.deltaTime;
            else SceneManager.LoadScene(undergroundScene);
        }
        else load = 0;
    }

    void Damage(Vector3 distance, Life player){
        if(Math.Abs(distance.x)< 0.05 && Math.Abs(distance.y) < 0.05){
            if(load < maxLoadTeleport) load += Time.deltaTime;
            else {player.TakeDamage(1); load = 0; Debug.Log("Dano");}
        }
        else load = 0;
    }
}
