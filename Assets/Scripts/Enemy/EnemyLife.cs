using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int health;

    private void Start() {
        //Debug.Log(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
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
}