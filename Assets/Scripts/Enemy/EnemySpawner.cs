using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (player)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
