using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerAction : MonoBehaviour
{
    private PlayerAttack basicAttack;
    private PlayerDash dash;
    private PlayerCollision collision;

    private PlayerSkills skill;
    private Interact interaction;

    
    // Start is called before the first frame update
    void Start()
    {
        basicAttack = GetComponent<PlayerAttack>();
        dash = GetComponent<PlayerDash>();
        collision = GetComponent<PlayerCollision>();

        skill = GetComponent<PlayerSkills>();
        interaction = GetComponent<Interact>();
    }

    public bool IsInAction(){
        /*
        if((dash.IsDashing())) Debug.Log("DASH");
        if(basicAttack.IsAttacking()) Debug.Log("ATQ BASICO");
        if(collision.IsTakingDamage()) Debug.Log("DANO");
        if((skill.IsUsingSkill())) Debug.Log("Skill");
        if((basicAttack.ChargingAttack())) Debug.Log("CHARGING ATTACK");
        if((interaction.isInteracting())) Debug.Log("INTERACTION");
        */


        return ((dash.IsDashing()) || (basicAttack.IsAttacking()) || (collision.IsTakingDamage()) ||
                (skill.IsUsingSkill()) || (basicAttack.ChargingAttack()) || (interaction.IsInteracting()) ||
                (basicAttack.ChargingAttack()));
    }
}
