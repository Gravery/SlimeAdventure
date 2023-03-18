using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheForest : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D pRigidbody;
    private BoxCollider2D pBoxCollider;
    private PlayerMovement pMov;
    private PlayerAttack pAttack;
    private Transform pTransf;

    private Vector3 toNPC = new Vector3(22.49f,-0.5f,0f);
    private GameObject NPC;

    [SerializeField]
    private GameObject NPC_old;

    private bool blocked = false;
    private float distance;
    private EventManagerSA em;

    private bool executed = false;

    private void Start() {
        SetNPC();
        em = GetComponent<EventManagerSA>();
    }

    private void Update(){
        // Movimentando o player até o slime bloqueador de passagem
        if(!NPC_old) return;

        if(NPC_old.transform.GetChild(0).GetComponent<NPCController>().GetInteraction() == 1 && executed == false){
            em.sa.dashMissionStarted = true;
            executed = true;
            NPC.GetComponent<NPCController>().SetInteraction(0);
            NPC.GetComponent<NPCController>().dialog.lines = new List<string>() {
                "O Chefe me avisou sobre você. Pode passar.", 
                "Só tome cuidado. A floresta não é mais o que era, coisas estranhas têm acontecido…"};
            NPC.GetComponent<NPCController>().dialog1.lines = new List<string>() {
                "Outros slimes falaram sobre monstros do outro lado do rio, por isso fechamos o caminho.",
                "Cuidado."};
        }

        if(blocked){
            Vector3 movDirection = (toNPC - pTransf.position).normalized;
            pRigidbody.velocity = movDirection * 2f;
            distance= Mathf.Sqrt((toNPC.x-pTransf.position.x)*(toNPC.x-pTransf.position.x)+(toNPC.y-pTransf.position.y)*(toNPC.y-pTransf.position.y));
            if(distance < 0.1){
                blocked = false;
                pRigidbody.velocity = new Vector2(0,0);
                // ESPAÇO PARA ATIVAR A INTERAÇÃO
                NPC.GetComponent<Interactable>()?.Interact();
                //
                pBoxCollider.isTrigger = false;
                pMov.enabled = true;
                pAttack.enabled = true;
                em.EventFinished();
            }

        }

    }

    public IEnumerator PathBlocked(){
        // Impede o que o player prossiga para a floresta
        // sem ter falado com o velho slime
        pRigidbody.velocity = new Vector2(0,0);
        pBoxCollider.isTrigger = true;
        pMov.enabled = false;
        pAttack.enabled = false;

        // Animação do ponto de exclamação 
        NPC.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        NPC.transform.GetChild(0).gameObject.SetActive(false);
        
        blocked = true;
    }

    public void SetPlayer(GameObject p){
        // Pegar as informações do player já coletadas no EventManager, sendo usado no EventManagerSA.cs
        player = p;
        pRigidbody = p.GetComponent<Rigidbody2D>();
        pBoxCollider = p.GetComponent<BoxCollider2D>();
        pMov = p.GetComponent<PlayerMovement>();
        pAttack = p.GetComponent<PlayerAttack>();
        pTransf = p.GetComponent<Transform>();
    }

    public void SetNPC(){
        // Pega o GameObject do NPC
        NPC = GameObject.FindWithTag("ForestNPC");
        NPC_old = GameObject.FindWithTag("OldNPC");
    }
}
