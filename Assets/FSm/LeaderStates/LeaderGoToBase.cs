using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderGoToBase : BaseStates<LeaderStates, Leader>
{
    public override void OnEnter()
    {
        Debug.Log("entre a LeaderGoToBase");
    }
    public override void OnUpdate()
    {
        Vector3 Base = avatar.team == Team.TeamA ? avatar.BaseA.transform.position : avatar.BaseB.transform.position;
        //Vector3 directionToBase = Base - avatar.transform.position;
        //avatar.transform.position = Vector3.MoveTowards(avatar.transform.position, Base, avatar.SpeedBase * Time.deltaTime);

        Vector3 BaseDistance = Base - avatar.transform.position;
        if (BaseDistance.magnitude < 0.5)
        {
            fsm.ChangeState(LeaderStates.LeaderRecovery);
        }

        pathFinding();
    }
    public void pathFinding()
    {
        Vector3 bases = avatar.team == Team.TeamA ? avatar.BaseA.transform.position : avatar.BaseB.transform.position;

        

        if (avatar._path.Count <= 0)
        {



            Nodes start = GameManager.Instance.GetNearestNode(avatar.transform.position, GameManager.Instance.allNodes);
            Nodes target = GameManager.Instance.GetNearestNode(bases, GameManager.Instance.allNodes);



            avatar._path = PathFinding.Instace.GetPath(start, target);



            if (avatar._path.Count <= 0) return;


        }


        var dir = (avatar._path[0].transform.position - avatar.transform.position);
        //dir+=ObstacleAvoindance() * avatar.obstacleWeight;
        avatar.transform.position += dir.normalized * avatar.SpeedBase * Time.deltaTime;



        if (dir.magnitude < 0.8f)
        {
            avatar._path.RemoveAt(0);
        }

    }
    public override void OnExit()
    {
        Debug.Log("sali de  LeaderGoToBase"); ;
    }
}
