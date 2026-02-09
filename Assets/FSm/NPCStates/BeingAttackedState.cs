using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAttackedState: BaseStates<EnemyStates, Enemy>
{

    public override void OnEnter()
    {
        Debug.Log("estoy siendo atacado");
    }
    public override void OnUpdate()
    {
        if (avatar._energy < 10)
        {
            fsm.ChangeState(EnemyStates.GoToBaseState);
        }
    }

    public override void OnExit()
    {

    }
}
