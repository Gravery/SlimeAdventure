using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    public Sprite standardSlime;
    public Sprite upSlime;
    private Rigidbody2D rb;
    private Vector2 move;
    private SpriteRenderer sprite;
    private DetectPlayerAction detect;
    private PlayerAttack playerAttack;
    private PlayerDash playerDash;
    public float debuff;
    private Animator animator;
    public bool isMoving;
    public LayerMask interactableLayer;
    float horizontal, vertical;

    [SerializeField] GameObject dialogBox;
    bool interacting;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        detect = GetComponent<DetectPlayerAction>();
        playerAttack = GetComponent<PlayerAttack>();
        playerDash = GetComponent<PlayerDash>();
        debuff = 0f;
        horizontal = 0;
        vertical = 0;
        interacting = false;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");  
        vertical = Input.GetAxisRaw("Vertical");
        move = new Vector2(horizontal, vertical);
        
        if(horizontal == 0 && vertical == 0 && (isMoving || !detect.IsInAction())) StopMovement();
        if(horizontal != 0 || vertical != 0 && !isMoving) isMoving = true;

        if(interacting == true)
            CheckInteraction();


        Flip(horizontal);

        if (vertical > 0){
            GetComponent<SpriteRenderer>().sprite = upSlime;
        }
        else{
            if ((vertical < 0) || (horizontal != 0)){
            GetComponent<SpriteRenderer>().sprite = standardSlime;
            }
        }

        if ((!detect.IsInAction()) && (!Input.GetKey(KeyCode.Space) && (!Input.GetKey(KeyCode.Z))) && isMoving){
            if (horizontal != 0 && vertical != 0){
                animator.SetFloat("moveX", horizontal);
                animator.SetFloat("moveY", vertical);
                rb.velocity = move * (speed / 1.4f) * ((100-debuff)/100);
            }
            else{
                animator.SetFloat("moveX", horizontal);
                animator.SetFloat("moveY", vertical);
                rb.velocity = move * speed * ((100-debuff)/100);
            }
        }
        if(detect.IsInAction() && !playerAttack.IsAttacking() && !playerDash.IsDashing()){
            StopMovement();
        }
        if ((!detect.IsInAction()) && (Input.GetKeyDown(KeyCode.C)) && !interacting){
            interacting = true;
            Interact();
        }
        animator.SetBool("isMoving", isMoving);
    }

    void Interact(){
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        if (collider != null){
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    void Flip(float horizontal)
    {
        if (horizontal  > 0){
            sprite.flipX = false;
        }
        else if (horizontal < 0){
            sprite.flipX = true;
        }
    }

    public void StopMovement(){
        rb.velocity = move * 0;
        isMoving = false;
        animator.SetBool("isMoving", isMoving);
    }

    public void CheckInteraction(){
        if(dialogBox.activeSelf == false){
            interacting = false;
        }
    }
    
}
