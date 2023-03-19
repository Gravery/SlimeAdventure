using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject dialogBox;
    public bool isInteracting;
    private DetectPlayerAction detect;
    public LayerMask interactableLayer;
    // Start is called before the first frame update
    void Start()
    {
        detect = GetComponent<DetectPlayerAction>();
    }

    // Update is called once per frame
    void Update(){
        CheckInteraction();

        if ((!detect.IsInAction()) && (Input.GetKeyDown(KeyCode.C))){
            isInteracting = true;
            Interaction();
        }
    }

    void Interaction(){
        var collider = Physics2D.OverlapCircle(transform.position, 0.3f, interactableLayer);
        if (collider != null){
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    private void CheckInteraction(){
        if(!dialogBox) return; 

        if(dialogBox.activeSelf == false){
            isInteracting = false;
        }
        if(dialogBox.activeSelf == true && isInteracting == false){
            isInteracting = true;
        }
    }

    public bool IsInteracting(){
        return isInteracting;
    }
}
