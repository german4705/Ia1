using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderAtack : BaseStates<LeaderStates,Leader>
{
    
    public override void OnEnter()
    {
        Debug.Log("entre a leaderAtack");
    }
    public override void OnUpdate()
    {
        Debug.Log("update leaderAtack");
    }

    public override void OnExit()
    {
        Debug.Log("sali de  leaderAtack"); ;
    }
}

