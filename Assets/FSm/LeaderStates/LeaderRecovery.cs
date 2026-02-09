using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderRecovery : BaseStates<LeaderStates, Leader>
{
    public override void OnEnter()
    {
        Debug.Log("entre a LeaderRecovery");
    }
    public override void OnUpdate()
    {
        avatar.energy += Time.deltaTime*10;
        if(avatar.energy>=100)
        {
            fsm.ChangeState(LeaderStates.LeaderMove);
        }
    }

    public override void OnExit()
    {
        Debug.Log("sali de  LeaderRecovery"); ;
    }
}
