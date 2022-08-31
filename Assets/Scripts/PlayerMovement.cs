using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    public Sprite standardSlime;
    public Sprite upSlime;
    private Rigidbody2D rb;
    private Vector2 move;
    private SpriteRenderer sprite;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //float deltaTime = Time.deltaTime;
        float horizontal = Input.GetAxisRaw("Horizontal");  //* deltaTime;
        GetComponent<SpriteRenderer>().sprite = standardSlime;
        Flip(horizontal);
        float vertical = Input.GetAxisRaw("Vertical");  //* deltaTime;
        move = new Vector2(horizontal, vertical);


        if (vertical > 0){
            GetComponent<SpriteRenderer>().sprite = upSlime;
        }

        if (!Input.GetKey(KeyCode.Space)){
            rb.velocity = move * speed;
        }
        else{
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
