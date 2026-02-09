using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBaseState : BaseStates<EnemyStates, Enemy>
{
    public override void OnEnter()
    {
        Debug.Log("Entre a GoToBaseState");

    }

    public override void OnUpdate()
    {
        Vector3 Base = avatar.team == Team.TeamA ? avatar.BaseA.transform.position : avatar.BaseB.transform.position;
        Vector3 directionToBase = Base - avatar.transform.position; // busco la direccion de la base

        directionToBase += ObstacleAvoindance() * avatar.obstacleWeight; // le sumo el vector de obsctaculo

        var newDirection = Vector3.Lerp(avatar.transform.right, directionToBase, avatar.speedDirection); //lerp para que no se gire instantaneo

        avatar.transform.right = newDirection;
        newDirection.z = 0;


        pathFinding();
        //if (Vector3.Distance(avatar.transform.position, directionToBase) < 1f)
        //{
        //    avatar.transform.position += newDirection.normalized * Time.deltaTime * avatar.speedBase;
        //}
        //else
        //{
        //    pathFinding();
        //}


        if (directionToBase.magnitude < 0.5)
        {
            fsm.ChangeState(EnemyStates.RecoverState);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Sali del GoToBaseState");
    }

    public Vector3 ObstacleAvoindance()
    {
        Vector3 _avoindaceDir = Vector3.zero;

        var obstacles = Physics.OverlapSphere(avatar.transform.position, avatar.RadiusAttack, avatar._obstacleLayer);

        if (obstacles.Length > 0)
        {
            foreach (var obstacle in obstacles)
            {
                var dir = avatar.transform.position - obstacle.transform.position;
                _avoindaceDir += dir.normalized * (avatar.RadiusAttack - dir.magnitude); //entre mas cerca del objeto mas peso para esquivar. el numero es mas grande siendo el maximo el radio.

            }

        }

        _avoindaceDir.z = 0;
        return _avoindaceDir;
    }

    public void pathFinding()
    {
        Vector3 bases = avatar.team == Team.TeamA ? avatar.BaseA.transform.position : avatar.BaseB.transform.position;

        if (Vector3.Distance(avatar.transform.position, bases) < 0.5f)
        {
            return;
        }

        if (avatar._path.Count <= 0)
        {



            Nodes start = GameManager.Instance.GetNearestNode(avatar.transform.position, GameManager.Instance.allNodes);
            Nodes target = GameManager.Instance.GetNearestNode(bases, GameManager.Instance.allNodes);



            avatar._path = PathFinding.Instace.GetPath(start, target);



            if (avatar._path.Count <= 0) return;


        }


        var dir = (avatar._path[0].transform.position - avatar.transform.position);
        //dir+=ObstacleAvoindance() * avatar.obstacleWeight;
        avatar.transform.position += dir.normalized * avatar.speedBase * Time.deltaTime;



        if (dir.magnitude < 0.8f)
        {
            avatar._path.RemoveAt(0);
        }

    }
}
