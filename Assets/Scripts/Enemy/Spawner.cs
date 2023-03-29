using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*
    Função Spawn de inimigos quando o player chega no quadrante
    */
    public List<GameObject> enemiesToSpawn;
    public List<GameObject> enemiesSpawned;

    [SerializeField]
    private DetectPlayerPosition quad;

    bool spawn = false;

    private void Update() {
        if(!quad) return;

        if(quad.IsHere()) Spawn();
        else Despawn();
    }

    void Spawn(){
        if(spawn) return;
        spawn = true;

        for(int i=0; i<enemiesToSpawn.Count ; i++){
            GameObject clone = Instantiate(enemiesToSpawn[i],transform.position, Quaternion.identity);
            enemiesSpawned.Add(clone);
        }

    }

    void Despawn(){
        if(!spawn) return;
        spawn = false;
        
        for(int i=0; i<enemiesSpawned.Count ; i++){
            Destroy(enemiesSpawned[0].gameObject);
            enemiesSpawned.Remove(enemiesSpawned[0]);
        }
    }
}