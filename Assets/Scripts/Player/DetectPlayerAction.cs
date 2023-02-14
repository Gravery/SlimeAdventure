using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerAction : MonoBehaviour
{
    private PlayerAttack basicAttack;
    private PlayerDash dash;
    private PlayerCollision collision;

    private PlayerSkills skill;

    
    // Start is called before the first frame update
    void Start()
    {
        basicAttack = GetComponent<PlayerAttack>();
        dash = GetComponent<PlayerDash>();
        collision = GetComponent<PlayerCollision>();

        skill = GetComponent<PlayerSkills>();
    }

    public bool IsInAction(){
        /*
        if((dash.IsDashing())) Debug.Log("DASH");
        if(basicAttack.IsAttacking()) Debug.Log("ATQ BASICO");
        if(collision.IsTakingDamage()) Debug.Log("DANO");
        if((skill.IsUsingSkill())) Debug.Log("Skill");
        if((basicAttack.ChargingAttack())) Debug.Log("CHARGING ATTACK");
        */


        return ((dash.IsDashing()) || (basicAttack.IsAttacking()) || (collision.IsTakingDamage()) ||
                (skill.IsUsingSkill()) || (basicAttack.ChargingAttack()));
    }
}
