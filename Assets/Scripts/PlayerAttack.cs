using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public KeyCode chargeAttack;
    public float attackSpeed;
    //public Sprite attackImage;
    private float attackChargeTimer;
    private float attackDuration;
    private bool isAttacking;
    private Vector2 attackMove;
    private Rigidbody2D rb;
    float horizontal;
    float vertical;
    public cooldownFireball charging;


    // Start is called before the first frame update
    void Start()
    {
        attackChargeTimer = 0f;
        attackDuration =  0.5f;
        isAttacking = false;
        charging.SetMaxTime(0.8f);
        charging.SetLoading(attackChargeTimer);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");  
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(chargeAttack)){
            attackChargeTimer += Time.deltaTime;
            charging.SetLoading(attackChargeTimer);
        }

        if ((Input.GetKeyUp(chargeAttack)) && (attackChargeTimer >= 0.8) && (isAttacking == false)){
            attackMove = new Vector2(horizontal, vertical);
            //GetComponent<SpriteRenderer>().sprite = attackImage;
            isAttacking = true;
        }

        if ((Input.GetKeyUp(chargeAttack)) && attackChargeTimer < 0.8){
            attackChargeTimer = 0;
            charging.SetLoading(attackChargeTimer);
        }

        if (isAttacking == true){
            Attack();
            Count();
            if (attackDuration <= 0){
                Reset();
            } 
        }

        if(attackChargeTimer != 0){
            charging.gameObject.SetActive(true);
        }
    }

    void Attack(){
        if (horizontal != 0 && vertical != 0){
            rb.velocity = attackMove * (attackSpeed / 1.4f);
        }
        else{
            rb.velocity = attackMove * attackSpeed;
        }
    }

    void Count(){
        attackDuration -= Time.deltaTime;
    }

    public void Reset(){
        attackChargeTimer = 0;
        attackDuration = 0.5f;
        isAttacking = false;
        charging.SetLoading(0f);
        charging.gameObject.SetActive(false);
    }

    public bool IsAttacking(){
        return isAttacking;
    }

    public void CollidedEnemy(){
        attackMove = -attackMove;
        attackDuration += 0.2f;
    }
}
