using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int damageTaken = 1;
    
    private Life life;

    private void Awake()
    {
        life = GetComponent<Life>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            life.TakeDamage(damageTaken);
        }
    }
}
