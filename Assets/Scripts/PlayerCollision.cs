using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int damageTaken = 1;
    
    private Life life;
    private PlayerAttack basicAttack;
    private PlayerMovement pm;
    private PlayerDash dash;
    private Rigidbody2D rb;
    private bool isTakingDamage;
    private float damageTimer;
    Vector2 direction;

    private void Awake()
    {
        life = GetComponent<Life>();
        basicAttack = GetComponent<PlayerAttack>();
        pm = GetComponent<PlayerMovement>();
        dash = GetComponent<PlayerDash>();
        rb = GetComponent<Rigidbody2D>();
        isTakingDamage = false;
        damageTimer = 0.15f;
    }

    private void Update(){
        if (isTakingDamage){
            HitMove();
            damageTimer -= Time.deltaTime;
        }
        if (damageTimer <= 0){
            isTakingDamage = false;
            damageTimer = 0.15f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Trap"))
        {
            if (!basicAttack.IsAttacking()){
                direction = collision.GetContact(0).normal;
                life.TakeDamage(damageTaken);
                GetComponent<PlayerDash>().Reset();
                isTakingDamage = true;
                return;
            }
            if (collision.gameObject.CompareTag("Enemy"))
                basicAttack.CollidedEnemy();
            else{
                direction = collision.GetContact(0).normal;
                life.TakeDamage(damageTaken);
                GetComponent<PlayerAttack>().Reset();
                isTakingDamage = true;
            }
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

    private void HitMove(){
        rb.velocity = direction * 20f;
    }

    public bool IsTakingDamage(){
        return isTakingDamage;
    }
}
