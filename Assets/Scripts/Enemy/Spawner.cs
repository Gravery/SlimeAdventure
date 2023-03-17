using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*
    Função Spawn de inimigos quando o player chega no quadrante
    */
    public List<GameObject> enemies;

    [SerializeField]
    private DetectPlayerPosition quad;

    private void Update() {
        if(quad.IsHere()){
            for(int i=0; i<enemies.Count ; i++){
                Instantiate(enemies[0],transform.position, Quaternion.identity);
                enemies.Remove(enemies[0]);
            }
        }
    }
}
