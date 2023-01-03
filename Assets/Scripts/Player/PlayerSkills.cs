using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSkills : MonoBehaviour
{
    // Variaveis gerais
    public bool unlockFireball;
    public bool unlockIce;
    public bool unlockPlant;
    private Quaternion skillDirection;
    private bool isUsingSkill; 
    private int skillCount; // PARA USO NOS TESTES

    // VARIÁVEIS PARA FIREBALL
    public GameObject goFireball;
    public Transform playerTransform;
    public float maxLoadFireball;
    private float loadFireball = 0;
    public cooldownFireball cf; // BARRA DE CARREGAMENTO
    private Quaternion directionFireball;

    // VARIÁVEIS PARA ICE
    public GameObject icePower;
    private float loadIce = 0;
    public float maxLoadIce;

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
        cf.SetMaxTime(maxLoadFireball);
        cf.SetLoading(0f);

        shooting = false;   
        isUsingSkill = false;
        skillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("x") && !isUsingSkill){
            skillCount++;
            if(skillCount==3)
                skillCount=0;
        }

        if(skillCount==0 && unlockFireball)
            Fireball();
        
        if(skillCount==1 && unlockIce)
            Ice();

        if(skillCount==2 && unlockPlant)
            Plant();

    }
    
    void Fireball()
    {
        /*
        Descrição: Apertando espaço existe um carregamento de 1.5 seg para poder atirar
        a bola de fogo, a direção dela depende das teclas WASD apertadas 
        durante a soltura do espaço.
        */
        cf.SetMaxTime(maxLoadFireball); // Tempo máximo para disparar

        if(Input.GetKey(KeyCode.Z))
        {
            isUsingSkill = true;
            if(loadFireball < maxLoadFireball)
            {
                loadFireball += Time.deltaTime;
                cf.SetLoading(loadFireball);
            }

            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || 
               Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)){
                skillDirection = FireballDirection();
            }
        }
        else
        {
            isUsingSkill = false;
            // ATIRAR
            if(loadFireball>= maxLoadFireball)
            {
                GameObject newFireball = Instantiate(goFireball, playerTransform.position, skillDirection);
            }
                

            loadFireball = 0f;
            cf.SetLoading(loadFireball);
        }

        // Ativa ou desativa a barra de carregamento    
        if(loadFireball == 0){
            cf.gameObject.SetActive(false);
        }
        else{
            cf.gameObject.SetActive(true);
        }
            
    }

    void Ice()
    {
        cf.SetMaxTime(maxLoadIce);

        if(Input.GetKey(KeyCode.Z))
        {
            isUsingSkill = true;
            if(loadIce < maxLoadIce){
                loadIce += Time.deltaTime;
                cf.SetLoading(loadIce);
            }

            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || 
               Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
                skillDirection = FireballDirection();
            
        }
        else
        {
            isUsingSkill = false;
            // ATIRAR
            if(loadIce>= maxLoadIce)
            {
                GameObject newIce = Instantiate(icePower, playerTransform.position, skillDirection);
            }
                

            loadIce = 0f;
            cf.SetLoading(loadIce);
        }

        // Ativa ou desativa a barra de carregamento    
        if(loadIce == 0){
            cf.gameObject.SetActive(false);
        }
        else{
            cf.gameObject.SetActive(true);
        }
    }

    void Plant()
    {
        if(Input.GetKey(KeyCode.Z) && !shooting)
        {
            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || 
               Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)){
                skillDirection = FireballDirection();
            }

            
            while(bodyParts.Count <20){
                bodyParts.Insert(0, plantPrefab);
            }
            touchSomething = false;
            isUsingSkill = true;
        }

        // Inicia o lançamento da vinha 
        if(Input.GetKeyUp(KeyCode.Z))
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
        if(Input.GetKey(KeyCode.UpArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,90);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,180);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,270);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,0);
        }
        if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,45);
        }
        if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,135);
        }
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,225);
        }
        if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            directionFireball.eulerAngles = new Vector3(0,0,315);
        }

        return directionFireball;
    }

    public bool IsUsingSkill(){
        return isUsingSkill;
    }
}
