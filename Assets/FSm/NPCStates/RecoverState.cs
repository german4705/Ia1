using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverState : BaseStates<EnemyStates,Enemy>
{
    
    public override void OnEnter()
    {
        Debug.Log("Entre a Recovery");
        


    }
    public override void OnExit()
    {
        Debug.Log("Sali de Recovery");
    }

    public override void OnUpdate()
    {
        avatar.RecoveryEnergy(20);

        if(avatar._energy==100)
        {
            fsm.ChangeState(EnemyStates.FollowLeaderState);
        }

    }


}
