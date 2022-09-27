using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private IntSO healthSO;

    private void Start() {
        health = healthSO.Value;
        Debug.Log(health);
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

    public int getHealth(){
        return health;
    }


    private void Update() {
        // PARA TESTES COM A VIDA
        if(Input.GetKeyDown("p"))
        {
            TakeDamage(1);
        }
    }
}
