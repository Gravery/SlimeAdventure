using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAbilities : MonoBehaviour
{
    // Variaveis para desbloqueio dos poderes
    public bool unlockFireball;
    public bool unlockIce;
    public bool unlockPlant;
    
    // VARIÁVEIS PARA FIREBALL
    public GameObject goFireball;
    public Transform playerTransform;
    public float velFireball;
    public float maxLoadindFireball;
    private float loadFireball = 0;
    public cooldownFireball cf;
    private Quaternion directionFireball;

    // VARIÁVEIS PARA ICE
    public Tile water;
    public Tile ice;
    private Vector3Int position; 
    public Tilemap tilemap;


    // PLANTA
    [SerializeField] float distanceBetween = 0.2f;
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> plantBody = new List<GameObject>();
    public GameObject plantPrefab;
    float countUp;
    bool shooting;
    Quaternion plantRotation;


    // Start is called before the first frame update
    void Start()
    {
        directionFireball.eulerAngles = new Vector3(0,0,0);
        cf.SetMaxTime(maxLoadindFireball);
        cf.SetLoading(0f);

        shooting = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(unlockFireball)
            Fireball();
        
        if(unlockIce)
            Ice();

        if(unlockPlant)
            Plant();
    }

    
    void Fireball()
    {
        if(Input.GetButton("Jump"))
        {
            if(loadFireball < 1.5f)
            {
                loadFireball += Time.deltaTime;
                cf.SetLoading(loadFireball);
            }
        }
        else
        {
            // ATIRAR
            if(loadFireball>= maxLoadindFireball)
            {
                GameObject newFireball = Instantiate(goFireball, playerTransform.position, FireballDirection());
                //newFireball.GetComponent<Rigidbody2D>().velocity = transform.right * velFireball;
            }
                


            loadFireball = 0f;
            cf.SetLoading(loadFireball);
            //Debug.Log(loadFireball);
        }
            
        if(loadFireball == 0)
        {
            cf.gameObject.SetActive(false);
        }
        else
        {
            cf.gameObject.SetActive(true);
        }
            
    }

    void Ice()
    {
        position = Vector3Int.FloorToInt(playerTransform.position);
        if(Input.GetKey("d"))
        {
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

        if(Input.GetKey("a"))
        {
            position.x -= 1;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
            position.y += 1;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
            position.y -= 2;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
        }

        if(Input.GetKey("w"))
        {
            position.y += 1;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
            position.x += 1;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
            position.x -= 2;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
        }

        if(Input.GetKey("s"))
        {
            position.y -= 1;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
            position.x += 1;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
            position.x -= 2;
            if(tilemap.GetTile(position) == water)
                tilemap.SetTile(position, ice);
        }
    }

    void Plant()
    {
        if(Input.GetButton("Jump") && !shooting)
        {
            if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") )
            {
                plantRotation = FireballDirection();
                //Debug.Log(plantRotation);
            }
        }

        if(Input.GetButtonUp("Jump"))
            shooting = true;

        if(bodyParts.Count > 0 && shooting)
        {
            countUp += Time.deltaTime;
            if(countUp >= distanceBetween)
            {
                GameObject temp = Instantiate(bodyParts[0], playerTransform.position, plantRotation);
                plantBody.Add(temp);
                bodyParts.RemoveAt(0);
                //temp.GetComponent<Rigidbody2D>().velocity = plantBody[0].transform.right * speed;
                countUp = 0;
            }
        }
        else
        {
            shooting = false;
            for(int i = 0; i < plantBody.Count; i++)
            {
                if(plantBody[i])
                {
                    //plantBody[i].GetComponent<Rigidbody2D>().velocity = plantBody[i].transform.right * -speed;
                    plantBody[i].GetComponent<Plant>().d = -1;
                    if(Vector3.Distance(playerTransform.position, plantBody[i].transform.position)<0.1f)
                    {
                        bodyParts.Insert(0,plantPrefab);
                        Destroy(plantBody[i]);
                        plantBody.Remove(plantBody[i]);
                    }
                }
            }
        }
    }
    

    private Quaternion FireballDirection()
    {
        // Determina a direção em que a bola de fogo será atirada
        if(Input.GetKey("w"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,90);
        }
        if(Input.GetKey("a"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,180);
        }
        if(Input.GetKey("s"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,270);
        }
        if(Input.GetKey("d"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,0);
        }
        if(Input.GetKey("w") && Input.GetKey("d"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,45);
        }
        if(Input.GetKey("w") && Input.GetKey("a"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,135);
        }
        if(Input.GetKey("a") && Input.GetKey("s"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,225);
        }
        if(Input.GetKey("s") && Input.GetKey("d"))
        {
            directionFireball.eulerAngles = new Vector3(0,0,315);
        }

        return directionFireball;
    }
}
