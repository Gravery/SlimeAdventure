using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageTaken = 2;

    private GameObject player;
    private EnemyLife life;
    private PlayerAttack basicAttack;
    Rigidbody2D rb;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    
    private void Awake()
    {
        life = GetComponent<EnemyLife>();
        player = GameObject.FindGameObjectWithTag("Player");
        basicAttack = player.GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rb.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;;
        }
        
        rb.MovePosition(position);
    }
    private void Movement()
    {
        if (player)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                player.transform.position, speed * Time.deltaTime);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && basicAttack.IsAttacking())
        {
            life.TakeDamage(damageTaken);
        }
    }
}
