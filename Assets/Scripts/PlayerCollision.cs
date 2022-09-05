using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int damageTaken = 1;
    
    private Life life;
    private BasicAttack basicAttack;
    private PlayerMovement pm;
    private Dash dash;
    private Rigidbody2D rb;
    private bool isTakingDamage;
    private float damageTimer;
    Vector2 direction;

    private void Awake()
    {
        life = GetComponent<Life>();
        basicAttack = GetComponent<BasicAttack>();
        pm = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
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
                GetComponent<Dash>().Reset();
                isTakingDamage = true;
            }
            if (collision.gameObject.CompareTag("Enemy"))
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

    private void HitMove(){
        rb.velocity = direction * 20f;
    }

    public bool IsTakingDamage(){
        return isTakingDamage;
    }
}
