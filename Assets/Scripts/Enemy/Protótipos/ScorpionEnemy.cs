using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionEnemy : MonoBehaviour
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

    private GameObject drill;
    private string currentState;
    private Animator animator;
    private EnemyLife life;
    private GameObject stunned;

    private GameObject area;

    private float timeCooldown, maxTimeCooldown;
    private bool inCooldown;

    private bool knockback;
    Vector2 knockbackDirection;
    private float timeKnockback, maxTimeKnockback;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        life = GetComponent<EnemyLife>();
        isMoving = false;
        isAttacking = false;
        transfPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        actDetected= false;
        previusDistance = -1;
        stoppingAttack = false;
        drill = transform.GetChild(0).gameObject;
        stunned = transform.GetChild(2).gameObject;

        timeCooldown = 0;
        maxTimeCooldown = 2f;
        inCooldown= false;
        knockback=false;
        //SetLimit();
    }

    // Update is called once per frame
    void Update()
    {
        if(Cooldown()) return;

        KnockbackMov();
        Movement();
        Animation();

        
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
        else if(DetectPlayer() && !isAttacking && EPDistance() <4f) StartCoroutine(attack());
        else if(DetectPlayer() && !isAttacking && EPDistance() >=4f){
            StartCoroutine(detectAnim());
            Vector2 direction = new Vector2(transfPlayer.position.x - transform.position.x, transfPlayer.position.y - transform.position.y);
            direction.Normalize();
            rb.velocity = movSpeed*direction;
        }

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(direction),9f,layer);
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
        Vector2 direction = new Vector2(transfPlayer.position.x - transform.position.x, transfPlayer.position.y - transform.position.y);
        rb.velocity = direction.normalized * atkSpeed;
        drill.SetActive(true);
        yield return null;
        /*
        yield return new WaitForSeconds(1.5f);
        rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        */
    }

    private IEnumerator detectAnim(){
        if(!actDetected){
            actDetected =true;
            detected.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            detected.SetActive(false);
        }
    }

    private IEnumerator StopAttack(){
        stoppingAttack = true;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(0,0);
        drill.SetActive(false);
        inCooldown = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        stoppingAttack = false;
    }


    private void SetLimit(Vector3 camPosition){
        var spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * .5f; 
 
        var cam = Camera.main;// Camera component to get their size, if t$$anonymous$$s change in runtime make sure to update values
        var camHeight = cam.orthographicSize;
        var camWidth = cam.orthographicSize * cam.aspect;
 
        yMin = camPosition.y - camHeight + spriteSize; // lower bound
        yMax = camPosition.y + camHeight - spriteSize; // upper bound
         
        xMin = camPosition.x - camWidth + spriteSize; // left bound
        xMax = camPosition.x + camWidth - spriteSize; // right bound 
    }
    


    //ANIMAÇÃO
    void Animation()
    {
        //if(stunned) return;
        if(rb.velocity.x == 0 && rb.velocity.x == 0){
            ChangeAnimationState("scorpion_idle");
        }
        else if(rb.velocity.x > 0 && !isAttacking){
            ChangeAnimationState("scorpion_walk");
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rb.velocity.x < 0 && !isAttacking){
            ChangeAnimationState("scorpion_walk");
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(rb.velocity.x > 0 && isAttacking){
            ChangeAnimationState("scorpion_attack");
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rb.velocity.x < 0 && isAttacking){
            ChangeAnimationState("scorpion_attack");
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private bool Cooldown(){
        if(!inCooldown) return false;

        Animation();    
        stunned.SetActive(true);
        timeCooldown += Time.deltaTime;
        if(timeCooldown<maxTimeCooldown) return true;
        
        stunned.SetActive(false);
        inCooldown = false;
        timeCooldown = 0;
        return false;
    }

    void ChangeAnimationState(string newState)
    {
        if(newState == currentState) return;

        animator.Play(newState);
        currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerAttack>().IsAttacking()){
                life.TakeDamage(collision.gameObject.GetComponent<PlayerAttack>().DamageDone());
                Vector2 direction = new Vector2(transfPlayer.position.x - transform.position.x, transfPlayer.position.y - transform.position.y);
                collision.gameObject.GetComponent<Life>().Knockback(direction,15f);
            }
            else{
                collision.gameObject.GetComponent<Life>().TakeDamage(1);
            }
        }

        if (collision.transform.CompareTag("Fireball"))
        {
            life.TakeDamage(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            if(other.gameObject.GetComponent<PlayerAttack>().IsAttacking()) return;
            other.gameObject.GetComponent<Life>().TakeDamage(3);
            Vector2 direction = new Vector2(transfPlayer.position.x - transform.position.x, transfPlayer.position.y - transform.position.y);
            other.gameObject.GetComponent<Life>().Knockback(direction);

            actualDistance = -1;
            stoppingAttack = true;
            rb.velocity = new Vector2(0,0);
            drill.SetActive(false);
            isAttacking = false;
            stoppingAttack = false;
            inCooldown = true;
        }


        if(other.gameObject.GetComponent<DetectPlayerPosition>() != null){
            SetLimit(other.transform.position);
        }
    }

    public void Knockback(Vector2 attackDirection){
        attackDirection.Normalize();
        knockbackDirection = attackDirection;
        knockback = true;
    }

    private void KnockbackMov(){
        if(!knockback) return;

        rb.AddForce(knockbackDirection*25, ForceMode2D.Impulse);

        if(timeKnockback > maxTimeKnockback) {knockback = false; timeKnockback=0;} 
        else timeKnockback += Time.deltaTime;
    }
}
