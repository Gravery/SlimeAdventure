using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageTaken = 3;

    private GameObject player;
    private Life life;
    
    private void Awake()
    {
        life = GetComponent<Life>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {

    }
    
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (player)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                player.transform.position, speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            life.TakeDamage(damageTaken);
        }
    }
}
