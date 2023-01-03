using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 dashVector;
    private DetectPlayerAction detect;
    public float dashSpeed;
    private bool isDashEnabled;
    public float startCooldown;
    private float cooldown;
    public float dashStartTimer;
    private float dashTimer;
    public bool onCooldown;
    private bool isDashing;
    float horizontal, vertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        detect = GetComponent<DetectPlayerAction>();
        cooldown = startCooldown;
        isDashEnabled = true;
        isDashing = false;
        dashTimer = dashStartTimer;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");  
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            dashVector = new Vector2(horizontal, vertical);
        }

        if (!isDashEnabled) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !onCooldown && !detect.IsInAction()){
        isDashing = true;
        onCooldown = true;
        }


        if (isDashing){
            DoDash();
            Count();
            if (dashTimer <= 0){
                Reset();
            }
        }

        if (onCooldown){
            cooldown -= Time.deltaTime;
            if (cooldown <= 0){
                onCooldown = false;
                cooldown = startCooldown;
            }
        }
    }

    public void EnableDash(){
        isDashEnabled = true;
    }

    public bool IsDashing(){
        return isDashing;
    }    

    void DoDash(){
        if (horizontal != 0 && vertical != 0)
        {
            rb.velocity = dashVector * (dashSpeed /1.4f);
        }
        else
        {
            rb.velocity = dashVector * dashSpeed;
        }
    }

    void Count(){
        dashTimer -= Time.deltaTime;
    }

    public void Reset(){
        dashTimer = dashStartTimer;
        isDashing = false;
    }
}
