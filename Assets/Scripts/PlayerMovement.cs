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

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        detect = GetComponent<DetectPlayerAction>();
    }

    private void Update()
    {
        //float deltaTime = Time.deltaTime;
        float horizontal = Input.GetAxisRaw("Horizontal");  //* deltaTime;
        float vertical = Input.GetAxisRaw("Vertical");  //* deltaTime;
        move = new Vector2(horizontal, vertical);
        
        Flip(horizontal);

        if (vertical > 0){
            GetComponent<SpriteRenderer>().sprite = upSlime;
        }
        else{
            if ((vertical < 0) || (horizontal != 0)){
            GetComponent<SpriteRenderer>().sprite = standardSlime;
            }
        }

        if ((!detect.IsInAction()) && (!Input.GetKey(KeyCode.Space) && (!Input.GetKey(KeyCode.Z)))){
            if (horizontal != 0 && vertical != 0){
                rb.velocity = move * (speed / 1.4f);
            }
            else{
                rb.velocity = move * speed;
            }
        }
        if ((!detect.IsInAction() && (Input.GetKey(KeyCode.Space))) || (Input.GetKey(KeyCode.Z)) ){
            StopMovement();
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
    }

    
}
