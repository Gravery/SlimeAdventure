using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerAction : MonoBehaviour
{
    private BasicAttack basicAttack;
    private Dash dash;
    private PlayerCollision collision;

    
    // Start is called before the first frame update
    void Awake()
    {
        basicAttack = GetComponent<BasicAttack>();
        dash = GetComponent<Dash>();
        collision = GetComponent<PlayerCollision>();
    }

    public bool IsInAction(){
        return ((dash.IsDashing()) || (basicAttack.IsAttacking()) || collision.IsTakingDamage());
    }
}
