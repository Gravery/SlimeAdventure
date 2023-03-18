using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public StartingAreaSO sa;
    public EventManagerSA eventManager;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            if(!sa.talkWithOldSlime){
                StartCoroutine(PathBlocked(other.gameObject.GetComponent<Rigidbody2D>()));
            }
        }
    }

    private IEnumerator PathBlocked(Rigidbody2D rb){
        // ANIMÇÃO SLIME SAINDO DA CASA
        // Desativa toda colisão do player e todos os controles
        // e movimenta ele um pouco para frente
        eventManager.DisablePlayerMovements(true);
        rb.velocity =new Vector2(0,1f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<NPCController>().Interact();
        rb.velocity =new Vector2(0,0);
        eventManager.DisablePlayerMovements(false);
    }
}
