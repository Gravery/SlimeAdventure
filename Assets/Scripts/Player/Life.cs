using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private PlayerInfo playerInfo;
    private Rigidbody2D rb;
    private bool knockback;
    private float kForce;
    Vector2 knockbackDirection;
    private float timeKnockback, maxTimeKnockback;

    private void Awake() {
        maxHealth = 10;
        health = playerInfo.life;
        maxTimeKnockback =0.25f;
        timeKnockback = 0;
        rb=GetComponent<Rigidbody2D>();
        //Debug.Log(health);
    }

    public void TakeDamage(int damage)
    {
        playerInfo.life -= damage;
        health = playerInfo.life;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public int GetHealth(){
        return health;
    }

    public int GetMaxHealth(){
        return maxHealth;
    }


    private void Update() {
        // PARA TESTES COM A VIDA
        if(Input.GetKeyDown("p"))
        {
            TakeDamage(1);
        }

        KnockbackMov();
    }

    public void Knockback(Vector2 attackDirection, float force = 25f){
        attackDirection.Normalize();
        knockbackDirection = attackDirection;
        knockback = true;
        kForce = force;
    }

    private void KnockbackMov(){
        if(!knockback) return;

        rb.velocity = new Vector2(0,0);
        rb.AddForce(knockbackDirection*kForce, ForceMode2D.Impulse);

        PlayerMovement pm = GetComponent<PlayerMovement>();
        pm.debuff = 90;

        if(timeKnockback > maxTimeKnockback) {knockback = false; timeKnockback=0; pm.debuff = 0;} 
        else timeKnockback += Time.deltaTime;
    }

   
}
