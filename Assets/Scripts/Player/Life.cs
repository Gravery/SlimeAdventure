using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private IntSO healthSO;

    private void Awake() {
        maxHealth = 10;
        health = healthSO.Value;
        //Debug.Log(health);
    }

    public void TakeDamage(int damage)
    {
        healthSO.Value -= damage;
        health = healthSO.Value;
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
    }
}
