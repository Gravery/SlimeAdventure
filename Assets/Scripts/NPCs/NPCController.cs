using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] public Dialog dialog;
    [SerializeField] public Dialog dialog1;
    private int cont = 0;

    public void Interact(){
            if (cont == 0){
                cont = 1;
                StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
            }
            else{
                StartCoroutine(DialogManager.Instance.ShowDialog(dialog1));
            }
        }

    public void SetInteraction(int x){
        cont = x;
    }
    public int GetInteraction(){
        return cont;
    }
    
}
