using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerAction : MonoBehaviour
{
    private BasicAttack basicAttack;
    private Dash dash;

    
    // Start is called before the first frame update
    void Awake()
    {
        basicAttack = GetComponent<BasicAttack>();
        dash = GetComponent<Dash>();
    }

    public bool IsInAction(){
        return ((dash.IsDashing()) || (basicAttack.IsAttacking()));
    }
}
