using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    private Rigidbody2D rb;
    private Vector2 move;
    private DetectPlayerAction detect;
    public float debuff;
    float horizontal, vertical;

    private Animator animator;
    private const string WALK_UP = "walk_up";
    private const string WALK_DOWN = "walk_down";
    private const string IDLE = "idle";
    private string currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        detect = GetComponent<DetectPlayerAction>();
    }

    private void Update()
    {
        if(detect.IsInAction()) return;
           
        horizontal = Input.GetAxisRaw("Horizontal");  
        vertical = Input.GetAxisRaw("Vertical");
        move = new Vector2(horizontal, vertical);

        Animation(move);

        move.Normalize();
        rb.velocity = move * speed * (100-debuff)/100;
    }


    void Animation(Vector2 d)
    {

        if(d == new Vector2(0,0)){
            ChangeAnimationState(IDLE);
            //gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else if(d.x > -1 && d.y<=0){
            ChangeAnimationState(WALK_DOWN);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else if(d.x == -1 && d.y <= 0){
            ChangeAnimationState(WALK_DOWN);
            gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
        else if(d.x > -1 && d.y>0){
            ChangeAnimationState(WALK_UP);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else if(d.x == -1 && d.y > 0){
            ChangeAnimationState(WALK_UP);
            gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
    }


    void ChangeAnimationState(string newState)
    {
        if(newState == currentState) return;

        animator.Play(newState);
        currentState = newState;
    }
}
