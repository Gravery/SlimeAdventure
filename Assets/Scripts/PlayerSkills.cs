using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSkills : MonoBehaviour
{
    // Variaveis para desbloqueio dos poderes
    public bool unlockFireball;
    public bool unlockIce;
    public bool unlockPlant;
    private Quaternion skillDirection;
    private bool isUsingSkill; 

    // VARIÁVEIS PARA FIREBALL
    public GameObject goFireball;
    public Transform playerTransform;
    public float velFireball;
    public float maxLoadindFireball;
    private float loadFireball = 0;
    public cooldownFireball cf; // BARRA DE CARREGAMENTO
    private Quaternion directionFireball;

    // VARIÁVEIS PARA ICE
    public Tile water;
    public Tile ice;
    private Vector3Int position; 
    public Tilemap tilemap;
    int iceDistance = 8; // Distância em TILES que o poder de gelo alcança 


    // PLANTA
    [SerializeField] float distanceBetween = 0.2f;
    List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> plantBody = new List<GameObject>();
    public GameObject plantPrefab;
    private float countUp;
    private bool shooting;
    private bool touchSomething;
    private Quaternion plantRotation;
    private bool correcao; // CRIADO UNICAMENTE PARA CORREÇÃO DE BUG

    // Start is called before the first frame update
    void Start()
    {
        skillDirection.eulerAngles = new Vector3(0,0,0);
        cf.SetMaxTime(maxLoadindFireball);
        cf.SetLoading(0f);

        shooting = false;   
        isUsingSkill = false;
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
        /*
        Descrição: Apertando espaço existe um carregamento de 1.5 seg para poder atirar
        a bola de fogo, a direção dela depende das teclas WASD apertadas 
        durante a soltura do espaço.
        */
        if(Input.GetButton("Jump"))
        {
            if(loadFireball < 1.5f)
            {
                loadFireball += Time.deltaTime;
                cf.SetLoading(loadFireball);
            }

            if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") )
            {
                skillDirection = FireballDirection();
            }
        }
        else
        {
            // ATIRAR
            if(loadFireball>= maxLoadindFireball)
            {
                GameObject newFireball = Instantiate(goFireball, playerTransform.position, skillDirection);
                //newFireball.GetComponent<Rigidbody2D>().velocity = transform.right * velFireball;
            }
                

            loadFireball = 0f;
            cf.SetLoading(loadFireball);
            //Debug.Log(loadFireball);
        }

        // Ativa ou desativa a barra de carregamento    
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
        if(Input.GetButton("Jump"))
        {
            if(loadFireball < 1.5f)
            {
                loadFireball += Time.deltaTime;
                cf.SetLoading(loadFireball);
            }

            if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") )
            {
                skillDirection = FireballDirection();
                Debug.Log(skillDirection.eulerAngles);
            }
        }
        else
        {
            position = Vector3Int.FloorToInt(playerTransform.position);
            // ATIRAR
            if(loadFireball>= maxLoadindFireball)
            {
                // DIREITA - D
                if(skillDirection.eulerAngles == new Vector3(0, 0, 0))
                {
                    for(int i = 0; i < iceDistance; i++)
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
                        position.y += 1;
                    }
                }

                // CIMA - W
                if(skillDirection.eulerAngles == new Vector3(0, 0, 90))
                {
                    for(int i = 0; i < iceDistance; i++)
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
                        position.x += 1;
                    }
                }

                // ESQUERDA - A
                if(skillDirection.eulerAngles == new Vector3(0, 0, 180))
                {
                    for(int i = 0; i < iceDistance; i++)
                    {
                        position.x -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.y -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.y += 2;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.y -= 1;
                    }
                }

                // BAIXO - S
                if(skillDirection.eulerAngles == new Vector3(0, 0, 90))
                {
                    for(int i = 0; i < iceDistance; i++)
                    {
                        position.y -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x += 2;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x -= 1;
                    }
                }

                // DIAGONAL SUP DIREITA - W+D  
                if(skillDirection.eulerAngles == new Vector3(0, 0, 45))
                {
                    for(int i = 0; i < iceDistance; i++)
                    {
                        position.x += 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.y += 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x += 1;
                    }
                }

                // DIAGONAL SUP ESQUERDA - W+A  
                if(skillDirection.eulerAngles == new Vector3(0, 0, 135))
                {
                    for(int i = 0; i < iceDistance; i++)
                    {
                        position.x -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.y += 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x += 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x -= 1;
                    }
                }

                // DIAGONAL INF ESQUERDA - S+A  
                if(skillDirection.eulerAngles == new Vector3(0, 0, 225))
                {
                    for(int i = 0; i < iceDistance; i++)
                    {
                        position.x -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.y -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x += 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x -= 1;
                    }
                }

                // DIAGONAL INF DIREITA - S+D  
                if(skillDirection.eulerAngles == new Vector3(0, 0, 315))
                {
                    for(int i = 0; i < iceDistance; i++)
                    {
                        position.x += 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.y -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x -= 1;
                        if(tilemap.GetTile(position) == water)
                            tilemap.SetTile(position, ice);
                        position.x += 1;
                    }
                }
            }
                


            loadFireball = 0f;
            cf.SetLoading(loadFireball);
            //Debug.Log(loadFireball);
        }

        // Ativa ou desativa a barra de carregamento    
        if(loadFireball == 0)
        {
            cf.gameObject.SetActive(false);
        }
        else
        {
            cf.gameObject.SetActive(true);
        }
    }

    void Plant()
    {
        if(Input.GetButton("Jump") && !shooting)
        {
            if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") )
            {
                skillDirection = FireballDirection();
            }

            
            while(bodyParts.Count <20)
            {
                bodyParts.Insert(0, plantPrefab);
            }
            touchSomething = false;
        }

        // Inicia o lançamento da vinha 
        if(Input.GetButtonUp("Jump"))
        {
            shooting = true;
        }
            
        // Caso o player lance toda a vinha ou ela toque em alguma coisa
        if(bodyParts.Count == 0 || touchSomething)
        {   
            //É necessário eliminar as últimas 2 vinhas invocadas 
            // para elas não saírem voando infinitamente
            if(correcao){
                if(plantBody[0].GetComponent<Plant>().d == 1){
                    Destroy(plantBody[plantBody.Count-1]);
                    Destroy(plantBody[plantBody.Count-2]);
                    correcao = false;
                }
            }

            // Mudando o sentido de movimento da vinha
            for(int i = 0; i < plantBody.Count; i++)
            {
                if(plantBody[i]) 
                    plantBody[i].GetComponent<Plant>().d = -1;
            }
        }

        // Quando o player parar de lançar completamente a vinha
        if(plantBody.Count != 0){
            if(!plantBody[0]){
                shooting = false;
                isUsingSkill = false;
                plantBody.Clear();
            }
            else{
                touchSomething = plantBody[0].GetComponent<Plant>().touchSomething;
            }
        }
    }

    // INSTÂNCIA CADA PARTE DAS PLANTAS
    void FixedUpdate() {
        //Precisa ser instanciadas em um FixedUpdate() ou elas saem com
        //espaçamentos diferentes
        if(bodyParts.Count > 0 && shooting && !touchSomething)
        {
            correcao = true;
            isUsingSkill = true;
            countUp += Time.deltaTime;
            if(countUp >= distanceBetween)
            {
                GameObject temp = Instantiate(bodyParts[0], playerTransform.position, skillDirection);
                plantBody.Add(temp);
                bodyParts.RemoveAt(0);
                countUp = 0;
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

    public bool IsUsingSkill(){
        return isUsingSkill;
    }
}
