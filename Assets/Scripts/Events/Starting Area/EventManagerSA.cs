using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerSA : MonoBehaviour
{
    /*
    SCRIPT para gerenciar os eventos que ocorrem na área inicial do jogo (Starting Area)
    alguns eventos podem ser escritos aqui ou em outro script para organizar. 
    A intenção é aqui ser ativado esses eventos quando as condições para eles sejam
    atendidas.
    */
    public StartingAreaSO sa;

    private GameObject player;

    private bool activeEvent;
    private EnterTheForest etf;

    void Start()
    {
        InitEvents();
        player = GameObject.FindWithTag("Player");

        // Se o jogador estiver iniciando um novo jogo
        if(sa.newGame){
            activeEvent = true;
            StartCoroutine(NewGameMovement());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // A ser usado quando o player falar com o velho slime 
        if(!sa.dashMissionStarted){
            if(player.transform.position.x >= 24.5 && !activeEvent){
                etf.SetPlayer(player);
                etf.SetNPC();
                StartCoroutine(etf.PathBlocked());
                activeEvent = true;
            }
        }
    }

    private IEnumerator NewGameMovement(){
        // ANIMÇÃO SLIME SAINDO DA CASA
        // Desativa toda colisão do player e todos os controles
        // e movimenta ele um pouco para frente
        player.GetComponent<BoxCollider2D>().isTrigger = true;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAttack>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.5f);
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;
        player.GetComponent<BoxCollider2D>().isTrigger = false;
        sa.newGame = false;
        activeEvent = false;
    }

    void InitEvents(){
        // Aqui carrega todos os scripts contendo eventos 
        etf = GetComponent<EnterTheForest>();
    }

    public void EventFinished(){
        // Para mudar a condição do evento a partir de outro script
        activeEvent = false;
    }
}
