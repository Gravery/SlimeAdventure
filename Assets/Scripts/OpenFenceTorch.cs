using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenFenceTorch : MonoBehaviour
{
    // Lista com os enimigos/tochas que influenciam na abertura da cerca
    public List<GameObject> torchs;
    public List<GameObject> enemies;

    // Os tiles da borda da cerca
    [SerializeField] private Tile leftFence;
    [SerializeField] private Tile rightFence;

    private Tilemap tilemap; // Tilemap contendo a cerca
    public int numFences; // Numero de cercas que deverão ser alteradas
 
    private Transform tr; // posição do gameObject, onde começa a cerca 
    private Vector3Int position; // posição no grid

    // Para escolher se a cerca vai abrir matando inimigos ou por acender tochas
    public bool byEnemies;
    public bool byTorch;

    private bool isTorchActive;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        if(GameObject.Find("Bloqueio")){ // Verifica se há o tilemap na cena
            tilemap = GameObject.Find("Bloqueio").GetComponent<Tilemap>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((TorchDetect() && byTorch) || (EnemyDetect() && byEnemies)){
            tilemap.SetTile(position, null);
            position = Vector3Int.FloorToInt(tr.position);

            tilemap.SetTile(position, leftFence);
            for(int i=0; i<numFences-2; i++){
                position.x +=1;
                tilemap.SetTile(position, null);
            }
            position.x +=1;
            tilemap.SetTile(position, null);
            tilemap.SetTile(position, rightFence);

            Destroy(gameObject);
        }
    }

    bool TorchDetect(){
        for(int i=0; i<torchs.Count; i++){
            isTorchActive = torchs[i].GetComponent<Torch>().isActive;
            if(isTorchActive){
                torchs.Remove(torchs[i]);
                break;
            }
        }

        if(torchs.Count > 0) return false;
        else return true;
    }

    bool EnemyDetect(){
        for(int i=0; i<enemies.Count; i++){
            if(enemies[i]){
                return false;
            }
        }


        return true;
    }
}
