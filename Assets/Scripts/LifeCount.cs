using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    private Life life;
    public Text lifetxt;

    void Awake(){
        life = GetComponent<Life>();
        lifetxt.text = life.getHealth().ToString(); 
    }

    void Update(){
        life = GetComponent<Life>();
        lifetxt.text = life.getHealth().ToString(); 
    }

}