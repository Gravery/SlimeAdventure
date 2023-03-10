using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public StartingAreaSO sa;
    public PlayerInfo pInfo;
    private GameObject movTutorial;
    private GameObject dashTutorial;

    private GameObject player;
    
    private bool moveWindow;

    private bool completeDashTutorial;
    // Start is called before the first frame update
    void Start()
    {
        movTutorial = transform.GetChild(0).gameObject;
        dashTutorial = transform.GetChild(1).gameObject;
        moveWindow = false;
        completeDashTutorial = sa.wakeUp;
        player = GameObject.FindWithTag("Player");

        if(sa.newGame){
            movTutorial.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Dash();

        if(sa.wakeUp && !completeDashTutorial){
            dashTutorial.SetActive(true);
        }
    }

    private void MoveTutorialWindow(GameObject t){
        if(t.transform.position.x <= -1170){
            t.SetActive(false);
            moveWindow = false;
        }

        Vector3 tutoPos = t.transform.position;
        tutoPos.y = tutoPos.z = 0;
        tutoPos.x = 400 * Time.deltaTime;
        t.transform.position -= tutoPos;
    }

    private void Movimento(){
        if(!movTutorial.activeSelf) return;


        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            moveWindow = true;
        }
        if(moveWindow){
            MoveTutorialWindow(movTutorial);
        }
        
    }

    private void Dash(){
        if(!dashTutorial.activeSelf) return;

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            moveWindow = true;
        }
        if(moveWindow){
            MoveTutorialWindow(dashTutorial);
            completeDashTutorial=true;
        }
    }
}
