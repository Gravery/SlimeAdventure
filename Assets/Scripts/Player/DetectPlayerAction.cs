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
        return ((dash.IsDashing()) || (basicAttack.IsAttacking()) || (collision.IsTakingDamage()) ||
                (skill.IsUsingSkill()) || (basicAttack.ChargingAttack()));
    }
}
