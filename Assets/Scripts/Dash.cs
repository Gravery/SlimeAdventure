using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        detect = GetComponent<DetectPlayerAction>();
        cooldown = startCooldown;
        isDashEnabled = false;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  
        float vertical = Input.GetAxisRaw("Vertical");
        dashVector = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.LeftShift) && onCooldown == false && isDashEnabled && !detect.IsInAction()){
        isDashing = true;
        onCooldown = true;
        }

        if (onCooldown){
            cooldown -= Time.deltaTime;
            if (cooldown <= 0){
                onCooldown = false;
                cooldown = startCooldown;
            }
        }

        if (isDashing){
            DoDash();
            Count();
            if (dashTimer <= 0){
                Reset();
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
        rb.velocity = dashVector * dashSpeed;
    }

    void Count(){
        dashTimer -= Time.deltaTime;
    }

    void Reset(){
        dashTimer = dashStartTimer;
        isDashing = false;
    }
}
