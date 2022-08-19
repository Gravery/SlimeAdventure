using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    public KeyCode chargeAttack;
    public float attackSpeed;
    //public Sprite attackImage;
    private float attackChargeTimer;
    private float attackDuration;
    private bool isAttacking;
    private Vector2 attackMove;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        attackChargeTimer = 0f;
        attackDuration =  0.5f;
        isAttacking = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");  
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(chargeAttack)){
            attackChargeTimer += Time.deltaTime;
        }

        if ((Input.GetKeyUp(chargeAttack)) && (attackChargeTimer >= 1) && (isAttacking == false)){
            attackMove = new Vector2(horizontal, vertical);
            //GetComponent<SpriteRenderer>().sprite = attackImage;
            isAttacking = true;
        }

        if ((Input.GetKeyUp(chargeAttack)) && attackChargeTimer < 0.8){
            attackChargeTimer = 0;
        }

        if (isAttacking == true){
            Attack();
            Count();
            if (attackDuration <= 0){
                Reset();
            } 
        }
    }

    void Attack(){
        rb.velocity = attackMove * attackSpeed;
    }

    void Count(){
        attackDuration -= Time.deltaTime;
    }

    void Reset(){
        attackChargeTimer = 0;
        attackDuration = 0.5f;
        isAttacking = false;
    }

    public bool IsAttacking(){
        return isAttacking;
    }

    public void CollidedEnemy(){
        attackMove = -attackMove;
        attackDuration += 0.3f;
    }
}
