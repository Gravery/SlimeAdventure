using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    public KeyCode chargeAttack;
    public float attackSpeed;
    //public Sprite attackImage;
    private float attackChargeTimer;
    private Vector2 attackMove;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        attackChargeTimer = 0;
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

        if ((Input.GetKeyUp(chargeAttack)) && (attackChargeTimer >= 1)){
            attackMove = new Vector2(horizontal, vertical);
            //GetComponent<SpriteRenderer>().sprite = attackImage;
            attackChargeTimer = 0;
            rb.velocity = attackMove * attackSpeed;
        }

        if ((Input.GetKeyUp(chargeAttack)) && attackChargeTimer < 1){
            attackChargeTimer = 0;
        }
    }
}
