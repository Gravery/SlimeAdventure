using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeA : MonoBehaviour
{
    public float movSpeed;
    public float atkSpeed;
    private float previusDistance;
    private float actualDistance;

    private Rigidbody2D rb;

    private Vector3 origin; // Onde o inimigo começa

    private bool isMoving;
    private bool isAttacking;
    private bool stoppingAttack;
    private bool actDetected;


    public LayerMask layer;

    public GameObject detected;
    private GameObject player;

    private Transform transfPlayer;

    private float xMax,yMax,xMin,yMin;


    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
        isMoving = false;
        isAttacking = false;
        transfPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        actDetected= false;
        previusDistance = -1;
        stoppingAttack = false;

        SetLimit();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        // Parar o ataque quando estiver se afastando do player
        if(isAttacking){
            actualDistance=EPDistance();
            if(previusDistance != -1){
                if(actualDistance > previusDistance && !stoppingAttack){
                    StartCoroutine(StopAttack());
                    actualDistance = -1;
                }
            }
            previusDistance = actualDistance;
        }
    }

    void Movement(){
        if(!isMoving && !DetectPlayer()) StartCoroutine(movement());
        else if(DetectPlayer() && !isAttacking) StartCoroutine(attack());

        if(isMoving){
            if(transform.position.x >= xMax) rb.velocity = new Vector2(-Mathf.Abs(rb.velocity.x), rb.velocity.y);
            if(transform.position.x <= xMin) rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x), rb.velocity.y);
            if(transform.position.y >= yMax) rb.velocity = new Vector2(rb.velocity.x, -Mathf.Abs(rb.velocity.y));
            if(transform.position.y <= yMin) rb.velocity = new Vector2(rb.velocity.x, Mathf.Abs(rb.velocity.y));
        }
        if(isAttacking){
            if(transform.position.x >= xMax) rb.velocity = new Vector2(-1f, 0);
            if(transform.position.x <= xMin) rb.velocity = new Vector2(1f, 0);
            if(transform.position.y >= yMax) rb.velocity = new Vector2(0, -1f);
            if(transform.position.y <= yMin) rb.velocity = new Vector2(0, 1f);
        }
    }


    private bool DetectPlayer(){
        /*
        Função: verificar se o player está próximo, retorna um boolean
        */
        Vector2 direction = new Vector2(transfPlayer.position.x - transform.position.x, transfPlayer.position.y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(direction),10f,layer);
        bool detected = false;
       // Debug.DrawRay(transform.position, transform.TransformDirection(direction)*10f, Color.red);
        if(hit) detected = true;
        
        return detected;
    }

    private float EPDistance(){
        /*
        Função: calcular a distância entre o player e o inimigo
        */
        float distance = Vector3.Distance(transfPlayer.position, transform.position);
        return distance;
    }


    /*
        CORROTINAS
        - movement
        - attack
        - StopAttack
    */
    private IEnumerator movement(){
        if(actDetected) actDetected = false;
        isMoving = true;
        float distance = Mathf.Sqrt((origin.x-transform.position.x)*(origin.x-transform.position.x)+(origin.y-transform.position.y)*(origin.y-transform.position.y));
        Vector2 mv;

        if(distance>2f) mv = new Vector2((origin.x-transform.position.x),(origin.y-transform.position.y));
        else mv = new Vector2(Random.Range(-1f,1f)*movSpeed,Random.Range(-1f,1f)*movSpeed);
            
        rb.velocity = mv.normalized * movSpeed; 
        yield return new WaitForSeconds(Random.Range(0.5f,1f));
        if(!DetectPlayer() && !isAttacking){
            rb.velocity = new Vector2(0,0);
            yield return new WaitForSeconds(Random.Range(1,3));
        }

        isMoving = false;
    }

    private IEnumerator attack(){
        isAttacking = true;
        if(!actDetected){
            actDetected =true;
            detected.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            detected.SetActive(false);
        }
        Vector2 direction = new Vector2(transfPlayer.position.x - transform.position.x, transfPlayer.position.y - transform.position.y);
        rb.velocity = direction.normalized * atkSpeed;
        /*
        yield return new WaitForSeconds(1.5f);
        rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        */
    }

    private IEnumerator StopAttack(){
        stoppingAttack = true;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        stoppingAttack = false;
    }


    private void SetLimit(){
        var spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * .5f; 
 
        var cam = Camera.main;// Camera component to get their size, if t$$anonymous$$s change in runtime make sure to update values
        var camHeight = cam.orthographicSize;
        var camWidth = cam.orthographicSize * cam.aspect;
 
        yMin = -camHeight + spriteSize; // lower bound
        yMax = camHeight - spriteSize; // upper bound
         
        xMin = -camWidth + spriteSize; // left bound
        xMax = camWidth - spriteSize; // right bound 
    }



    /*
    private void OnCollisionEnter2D(Collision2D other) {
        if(!other.transform.CompareTag("Player") && isAttacking){
            isAttacking = false;
            //StartCoroutine(Stun());
        }
    }


    private IEnumerator Stun(){
        stunned = true;
        stunnedAnim.SetActive(true);
        rb.velocity = new Vector2(-attackDirection.x,-attackDirection.y);
        yield return new WaitForSeconds(0.05f);
        rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(0.5f);
        stunned = false;
        stunnedAnim.SetActive(false);
    }
    */
}