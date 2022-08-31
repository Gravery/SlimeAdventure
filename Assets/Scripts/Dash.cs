using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 dashVector;
    public float dashSpeed;
    public bool isDashEnabled;
    public float startCooldown;
    private float cooldown;
    public bool onCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cooldown = startCooldown;
        isDashEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  //* deltaTime;
        float vertical = Input.GetAxisRaw("Vertical");  //* deltaTime;
        dashVector = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.LeftShift) && onCooldown == false && isDashEnabled){
        transform.position += (Vector3)dashVector * dashSpeed;
        onCooldown = true;
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
}
