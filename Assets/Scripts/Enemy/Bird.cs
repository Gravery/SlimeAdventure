using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Transform player;
    //[SerializeField]
    //private GameObject effect;

    private float rotateSpeed;
    private float speed=4f;

    private Rigidbody2D rb;

    private Animator animator;
    private bool movement;

    private Quaternion rotation;
    private EnemyLife life;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotateSpeed=100f;
        player=GameObject.FindWithTag("Player").transform;
        life = GetComponent<EnemyLife>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(EPDistance()>6f) movement = true;
        if(player == null) return;
        if(!movement) {transform.rotation=rotation; return;}

        Vector2 direction = (Vector2)player.position - rb.position;

        direction.Normalize();
        //Debug.Log( Vector3.Cross(direction, transform.up).z);
        float rotateAmount = Vector3.Cross(direction, transform.up-transform.right).z;
        
        rb.angularVelocity = -rotateAmount * rotateSpeed;


        rb.velocity = (transform.up-transform.right) * speed;

    }



    public void SetPlayer(Transform p){
        player = p;
    }

    private float EPDistance(){
        /*
        Função: calcular a distância entre o player e o inimigo
        */
        float distance = Vector3.Distance(player.position, transform.position);
        return distance;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player")){
            if(other.gameObject.GetComponent<PlayerAttack>().IsAttacking()){
                Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
                other.gameObject.GetComponent<Life>().Knockback(direction,other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude*0.6f);
                life.TakeDamage(other.gameObject.GetComponent<PlayerAttack>().DamageDone());
            }
            else{
                other.gameObject.GetComponent<Life>().TakeDamage(2);
                rotation = transform.rotation;
                movement = false;
            }
        }
    }
}
