using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int damageTaken = 1;
    
    private Life life;
    private PlayerAttack basicAttack;
    private PlayerMovement pm;
    private PlayerDash dash;
    private PlayerSkills ps;
    private Rigidbody2D rb;
    private bool isTakingDamage;
    private float damageTimer;
    Vector2 direction;
    public HealthBar drawlife;

    private void Awake()
    {
        life = GetComponent<Life>();
        basicAttack = GetComponent<PlayerAttack>();
        pm = GetComponent<PlayerMovement>();
        dash = GetComponent<PlayerDash>();
        ps = GetComponent<PlayerSkills>();
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
                drawlife.DrawHearts();
                GetComponent<PlayerDash>().Reset();
                isTakingDamage = true;
                return;
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Vector2 normal = collision.contacts[0].normal;
                basicAttack.CollidedEnemy(normal);
            }
            else{
                direction = collision.GetContact(0).normal;
                life.TakeDamage(damageTaken);
                drawlife.DrawHearts();
                GetComponent<PlayerAttack>().Reset();
                isTakingDamage = true;
                return;
            }
            return;
        }
        else if (collision.gameObject.CompareTag("Wall")){
            if (basicAttack.IsAttacking())
            {
                Vector2 normal = collision.contacts[0].normal;
                basicAttack.CollidedEnemy(normal);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("EnableDash")) {dash.EnableDash(); Destroy(other.gameObject);}
        else if(other.gameObject.CompareTag("EnableFireball")){ ps.UnlockFireball(); Destroy(other.gameObject);}
        else if(other.gameObject.CompareTag("EnableIce")){ ps.UnlockIce(); Destroy(other.gameObject);}
        else if(other.gameObject.CompareTag("EnableVine")){ ps.UnlockPlant(); Destroy(other.gameObject);}
        else if(other.gameObject.CompareTag("EnableJump")){ ps.UnlockJump(); Destroy(other.gameObject);}
        else if(other.gameObject.CompareTag("ForestNPC")){ other.GetComponent<Interactable>()?.Interact();}
    }


    private void HitMove(){
        rb.velocity = direction * 20f;
    }

    public bool IsTakingDamage(){
        return isTakingDamage;
    }
}
