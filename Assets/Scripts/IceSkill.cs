using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class IceSkill : MonoBehaviour
{
    Rigidbody2D rb;

    public Tile water;
    public Tile ice;
    private Vector3Int position; 
    private Tilemap tilemap;
    private Transform tr;
    private float lifeTime =0.4f;

    void Start(){
        tr = GetComponent<Transform>();
        if(GameObject.Find("WaterIce")){ // Verifica se há tiles com água na cena 
            tilemap = GameObject.Find("WaterIce").GetComponent<Tilemap>();
        }
    }
    
    void Update()
    {
        if(tilemap) Freeze(); 
        Move();

        Destroy(gameObject, lifeTime); // Destrói depois de certo tempo
    }

    void Move(){
        gameObject.GetComponent<Rigidbody2D>().velocity = tr.right * 20;
    }

    void Freeze(){
        // Responsável por congelar os tiles com água
        // A área de efeito é um quadrado 3x3 
        position = Vector3Int.FloorToInt(tr.position);

        position.x -= 1;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y += 1;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y -= 2;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y += 1;


        position.x += 1;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y += 1;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y -= 2;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y += 1;


        position.x += 1;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y += 1;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
        position.y -= 2;
        if(tilemap.GetTile(position) == water)
            tilemap.SetTile(position, ice);
    }
}
