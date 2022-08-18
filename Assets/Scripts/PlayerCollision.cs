using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int damageTaken = 1;
    
    private Life life;
    private BasicAttack basicAttack;

    private void Awake()
    {
        life = GetComponent<Life>();
        basicAttack = GetComponent<BasicAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (basicAttack.IsAttacking()){
                print("No damage taken");
            }
            else{
                life.TakeDamage(damageTaken);
            }
            basicAttack.CollidedEnemy();
        }
    }
}
