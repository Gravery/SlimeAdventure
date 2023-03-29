using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sandstorm : MonoBehaviour
{
    public GameObject filter;
    private GameObject player;
    private bool startFade, endFade;

    private GameObject particles;

    [SerializeField]
    private List<DetectPlayerPosition> quadrants;

    [SerializeField]
    private List<Transform> positions;

    // Start is called before the first frame update
    private Image img;

   
    private int previusQuad, currentQuad, antepenultQuad;

    private GameObject up, down, right, left;
    private GameObject sandstormDirection;

    private bool resolvingMaze;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startFade = false; 
        endFade = false;
        resolvingMaze = false; 

        
        img = filter.GetComponent<Image>();

        
        up = filter.transform.GetChild(0).gameObject;
        down = filter.transform.GetChild(1).gameObject;
        right = filter.transform.GetChild(2).gameObject;
        left = filter.transform.GetChild(3).gameObject;
        sandstormDirection = up;
    }

    // Update is called once per frame
    void Update()
    {
        SandstormMaze();

        // TRANSIÇÃO DE QUADRANTE
        if(startFade) StartCoroutine(Fade(true, 2f));
        if(endFade) StartCoroutine(Fade(false, 2f));

        if(img.color.a<0.6) img.color = new Color(img.color.r,img.color.g,img.color.b, 0.6f);
    }

    void SandstormMaze(){
        if(player){
            for(int i = 0; i<quadrants.Count; i++){
                if( i>1 && quadrants[i].IsHere()){
                    if(!filter.activeSelf) filter.SetActive(true);
                    currentQuad = i-1;
                    if(previusQuad != currentQuad) Maze(currentQuad);
                }
                else if(i<=1 && quadrants[i].IsHere()){
                    if(i == 1 && previusQuad != 6){ player.transform.position =  new Vector3(-46.9f,3.56f,0); resolvingMaze = false;}
                    filter.SetActive(false);
                }
            }
        }
    }

    void Maze(int quad){
        StartCoroutine(QuadTransition());

        
        if(quad == 1){
            if(resolvingMaze){
                if(previusQuad == 4) player.transform.position = new Vector3(-22.4f, 14.8f,0);
                else if(previusQuad == 2) player.transform.position = new Vector3(-13.31f, 22.21f,0);
                else{ player.transform.position =  new Vector3(-46.9f,3.56f,0); resolvingMaze = false;}
            }
            else{
                resolvingMaze = true;
                if(sandstormDirection.activeSelf) sandstormDirection.SetActive(false);
                sandstormDirection = up;
                sandstormDirection.SetActive(true);
            }
        }
        if(quad == 2){
            if(previusQuad == 5) {player.transform.position = new Vector3(-8.63f,15.16f,0);}
            else if(previusQuad == 1 && antepenultQuad !=4){ player.transform.position =  new Vector3(-46.9f,3.56f,0); resolvingMaze = false;}

            if(sandstormDirection.activeSelf) sandstormDirection.SetActive(false);
            sandstormDirection = left;
            sandstormDirection.SetActive(true);
        }
        if(quad == 3){
            if((previusQuad == 2 && antepenultQuad!=5)|| previusQuad == 6 ) { player.transform.position =  new Vector3(-46.9f,3.56f,0); resolvingMaze = false;}
   

            if(sandstormDirection.activeSelf) sandstormDirection.SetActive(false);
            sandstormDirection = up;
            sandstormDirection.SetActive(true);
        }
        if(quad == 4){
            if(previusQuad == 1) {player.transform.position = new Vector3(2.37f,17.38f,0);}
            else if(previusQuad != 5){ player.transform.position =  new Vector3(-46.9f,3.56f,0); resolvingMaze = false;}

            if(sandstormDirection.activeSelf) sandstormDirection.SetActive(false);
            sandstormDirection = down;
            sandstormDirection.SetActive(true);
        }
        if(quad == 5){
            if(previusQuad == 6) {player.transform.position = new Vector3(-35.43f,25.73f,0);}
            else if(previusQuad != 1) { player.transform.position =  new Vector3(-46.9f,3.56f,0); resolvingMaze = false;}

            if(sandstormDirection.activeSelf) sandstormDirection.SetActive(false);
            sandstormDirection = down;
            sandstormDirection.SetActive(true);
        }
        if(quad == 6){
            if(previusQuad == 3) { player.transform.position =  new Vector3(-44.95f,29.66f,0); resolvingMaze = false;}
            else if(previusQuad != 4) { player.transform.position =  new Vector3(-46.9f,3.56f,0); resolvingMaze = false;}

            if(sandstormDirection.activeSelf) sandstormDirection.SetActive(false);
            sandstormDirection = left;
            sandstormDirection.SetActive(true);
        }

        antepenultQuad = previusQuad;
        previusQuad = quad;
    }


    private IEnumerator Fade(bool fadeToBlack = true, float fadeSpeed = 5f){
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
            while(img.color.a>0.65){
                fadeAmount = objectColor.a - (fadeSpeed*Time.deltaTime);
                    
                objectColor = new Color(objectColor.r,objectColor.g,objectColor.b, fadeAmount);
                img.color = objectColor;
                yield return null;
            }
        }
    }

    private IEnumerator QuadTransition(){
        startFade=true;
        endFade = false;
        yield return new WaitForSeconds(2f);
        startFade=false;
        endFade = true;
        yield return new WaitForSeconds(2f);
        startFade=false;
        endFade = false;
    }
}


