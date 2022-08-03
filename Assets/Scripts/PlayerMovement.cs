using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    
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
        Flip(horizontal);
        float vertical = Input.GetAxisRaw("Vertical");  //* deltaTime;
        _movement = new Vector2(horizontal, vertical);
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _movement * speed;
    }

    private void Flip(float horizontal)
    {
        if (horizontal  > 0){
            sprite.flipX = false;
        }
        else if (horizontal < 0){
            sprite.flipX = true;
        }
    }
}
