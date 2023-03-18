using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlimeFainting : MonoBehaviour
{
    public GameObject dashItem;
    public GameObject blackScreenUI;
    public StartingAreaSO saSO;
    private bool detectPlayer;
    private bool hasFaint;
    private bool fadeIn;
    private bool fadeOut;    
    private bool canWakeUp;
    
    void Start(){
        if(saSO.dashEnabled){
            hasFaint = true;
        }
        else hasFaint = false;

        fadeIn = false;
        fadeOut = false;
    }

    void Update() {
        if(fadeIn) StartCoroutine(FadeBlack());
        if(fadeOut) StartCoroutine(FadeBlack(false,1.5f));

        if(canWakeUp)
            if(Input.GetKeyDown(KeyCode.Space)){fadeIn = false; fadeOut=true; saSO.wakeUp = true;}
            
    }


    public void Faint(){
        if(!hasFaint){
            hasFaint = true;
            blackScreenUI.SetActive(true);
            StartCoroutine(Fainting());
        }
    }

    private IEnumerator Fainting(){
        fadeIn = true;
        fadeOut = false;
        yield return new WaitForSeconds(1f);
        fadeIn = false;
        fadeOut = true;
        yield return new WaitForSeconds(1f);
        fadeIn = true;
        fadeOut = false;
        yield return new WaitForSeconds(1f);
        fadeIn = false;
        fadeOut = true;
        yield return new WaitForSeconds(1f);
        fadeIn = true;
        fadeOut = false;


        // INTERAÇÃO
        //GameObject.FindWithTag("Desmaio").GetComponent<Interactable>()?.Interact();
        //

        canWakeUp = true;
    }

    private IEnumerator FadeBlack(bool fadeToBlack = true, float fadeSpeed = 1.5f){
        Image img = blackScreenUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        Color objectColor = img.color;
        float fadeAmount;

        if(fadeToBlack){
            while(img.color.a < 1){
                fadeAmount = objectColor.a + (fadeSpeed*Time.deltaTime);

                objectColor = new Color(objectColor.r,objectColor.g,objectColor.b, fadeAmount);
                img.color = objectColor;
                yield return null;
            }
        }
        else{
            while(img.color.a>0){
                fadeAmount = objectColor.a - (fadeSpeed*Time.deltaTime);

                objectColor = new Color(objectColor.r,objectColor.g,objectColor.b, fadeAmount);
                img.color = objectColor;
                yield return null;
            }
        }
    }


    public void SpawnDashItem(){
        Instantiate(dashItem, new Vector3(60.47f,12.47f,0f), Quaternion.identity);
    }
}
