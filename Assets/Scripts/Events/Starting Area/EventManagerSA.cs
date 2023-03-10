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
    public PlayerInfo pInfo;

    private bool activeEvent;
    private EnterTheForest etf;
    private SlimeFainting sf;

    void Start()
    {
        InitEvents();
        player = GameObject.FindWithTag("Player");

        // Se o jogador estiver iniciando um novo jogo
        if(sa.newGame){
            activeEvent = true;
            player.transform.position = new Vector3(-0.49f,1.95f,0f); // Na casa do slime
            StartCoroutine(NewGameMovement());
        }

        if(!sa.dashEnabled){
            sf.SpawnDashItem();
        }
        if(sa.slimeVillageDestroyed){
            DestroyVillage();
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
        else{
            if(sa.dashEnabled && !sa.wakeUp){
                DisablePlayerMovements(true);
                sf.Faint();
            }
            if(sa.wakeUp && !sa.slimeVillageDestroyed){
                DestroyVillage();
                DisablePlayerMovements(false);
            }
        }

        
    }

    void DestroyVillage(){
        GameObject slimeVillage =  GameObject.FindWithTag("SlimeVillage");

        slimeVillage.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        slimeVillage.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        slimeVillage.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        slimeVillage.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        slimeVillage.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        slimeVillage.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);

        sa.slimeVillageDestroyed = true;
    }

    private IEnumerator NewGameMovement(){
        // ANIMÇÃO SLIME SAINDO DA CASA
        // Desativa toda colisão do player e todos os controles
        // e movimenta ele um pouco para frente
        DisablePlayerMovements(true);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.5f);
        yield return new WaitForSeconds(2f);
        DisablePlayerMovements(false);
        sa.newGame = false;
        activeEvent = false;
    }

    void InitEvents(){
        // Aqui carrega todos os scripts contendo eventos 
        etf = GetComponent<EnterTheForest>();
        sf = GetComponent<SlimeFainting>();
    }

    public void EventFinished(){
        // Para mudar a condição do evento a partir de outro script
        activeEvent = false;
    }

    public void DisablePlayerMovements(bool condition){
        if(condition){
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            player.GetComponent<PlayerSkills>().enabled = false;
            player.GetComponent<PlayerDash>().enabled = false;
        }
        else{
            player.GetComponent<BoxCollider2D>().isTrigger = false;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerAttack>().enabled = true;
            player.GetComponent<PlayerSkills>().enabled = true;
            player.GetComponent<PlayerDash>().enabled = true;
        }
    }
}
