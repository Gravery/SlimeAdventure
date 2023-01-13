using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    private Life life;
    public Text lifetxt;
    private int fullHearts;
    private int halfHeart;
    private int emptyHearts;



    void Awake(){
        life = GetComponent<Life>();
        lifetxt.text = life.GetHealth().ToString(); 
    }

    void Update(){
        life = GetComponent<Life>();
        lifetxt.text = life.GetHealth().ToString(); 
    }

}