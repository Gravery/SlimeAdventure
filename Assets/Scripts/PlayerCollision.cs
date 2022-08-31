using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int damageTaken = 1;
    
    private Life life;
    private BasicAttack basicAttack;
    private PlayerMovement pm;
    private Dash dash;

    private void Awake()
    {
        life = GetComponent<Life>();
        basicAttack = GetComponent<BasicAttack>();
        pm = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (basicAttack.IsAttacking()){
                Debug.Log("No damage taken");
            }
            else{
                life.TakeDamage(damageTaken);
            }
            basicAttack.CollidedEnemy();
        }

        if (collision.gameObject.CompareTag("Wall")){
            
            if (basicAttack.IsAttacking()){
                basicAttack.CollidedEnemy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("EnableDash")){
            dash.EnableDash();
        }
    }
}
