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

    [SerializeField]
    private List<int> path;
    // Start is called before the first frame update
    private Image img;

    private int rightPath;

    private int previusQuad, currentQuad;

    private GameObject up, down, right, left;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startFade = false; 
        endFade = false;
        
        img = filter.GetComponent<Image>();
        rightPath = 0;
        
        up = filter.transform.GetChild(0).gameObject;
        down = filter.transform.GetChild(1).gameObject;
        right = filter.transform.GetChild(2).gameObject;
        left = filter.transform.GetChild(3).gameObject;
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
                    currentQuad = i+1;
                    if(previusQuad != currentQuad) Maze(currentQuad);
                }
                else if(i<=1 && quadrants[i].IsHere()){
                    filter.SetActive(false);
                }
            }
        }
    }

    void Maze(int quad){
        StartCoroutine(QuadTransition());

        path.Add(quad);
        if(path.Count == 6){
            
            if(rightPath == 6){
                //teleporte player para o templo
            }
            else{
                //teleporte player para o começo
            }
            
            rightPath = 0;
            return;
        }
        
        if(quad == 1){
            
        }
        if(quad == 2){

        }
        if(quad == 3){

        }
        if(quad == 4){

        }
        if(quad == 5){

        }
        if(quad == 6){

        }

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


