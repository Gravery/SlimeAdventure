using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    public Sprite standardSlime;
    public Sprite upSlime;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movement;
    private SpriteRenderer sprite;

    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //float deltaTime = Time.deltaTime;
        float horizontal = Input.GetAxisRaw("Horizontal");  //* deltaTime;
        GetComponent<SpriteRenderer>().sprite = standardSlime;
        Flip(horizontal);
        float vertical = Input.GetAxisRaw("Vertical");  //* deltaTime;
        _movement = new Vector2(horizontal, vertical);


        if (vertical > 0){
            GetComponent<SpriteRenderer>().sprite = upSlime;
        }

        if (!Input.GetKey(KeyCode.Space)){
            _rigidbody2D.velocity = _movement * speed;
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
        _rigidbody2D.velocity = _movement * -1;
        _rigidbody2D.velocity = _movement * 0;
    }

    
}
