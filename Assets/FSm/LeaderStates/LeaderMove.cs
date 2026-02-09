using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMove : BaseStates<LeaderStates,Leader>
{
    public Vector3 worldPoint2d;
    public bool targetpath = true; //camino bloqueado o libre.
    public override void OnEnter()
    {
        Debug.Log("Entré al estado LeaderMove para: " + avatar.name);
    }
    public override void OnUpdate()
    {
        if(Input.GetMouseButtonDown(avatar.team==Team.TeamA?0:1))
        {
            
            Vector3 worldPoint = avatar.cam.ScreenToWorldPoint(Input.mousePosition);

            
            worldPoint.z = 0;

            worldPoint2d = worldPoint;

            avatar.targetMove = worldPoint;

            /*var nodeclose = GameManager.Instance.GetNearestNode(avatar.targetMove, GameManager.Instance.allNodes).transform.position;*/ //calculo el nodo mas cercano para saber si esta en vision.
            targetpath = GameManager.Instance.Onsigth(avatar.transform.position, avatar.targetMove); //si no choca con la pared true

            
        }


        if (targetpath)
        {
            var dir = (avatar.targetMove - avatar.transform.position).normalized;
            dir += avatar.ObstacleAvoindance().normalized * avatar.obstacleWeight;
            avatar.transform.position += dir * avatar.SpeedMove * Time.deltaTime;
            


        }
        else
        {
            pathFinding();
        }


        avatar.energy -= Time.deltaTime;






        if (avatar.energy<=10)
        {
            fsm.ChangeState(LeaderStates.LeaderGoToBase);
        }
    }

    public override void OnExit()
    {
        Debug.Log("sali de  leaderMove"); ;
    }

    

    public void pathFinding()
    {

        if (Vector3.Distance(avatar.transform.position, avatar.targetMove) < 0.5f)
        {
            return;
        }

        if (avatar._path.Count <= 0)
        {



            Nodes start = GameManager.Instance.GetNearestNode(avatar.transform.position, GameManager.Instance.allNodes);
            Nodes target = GameManager.Instance.GetNearestNode(avatar.targetMove, GameManager.Instance.allNodes);



            avatar._path = PathFinding.Instace.GetPath(start,target);


           
            if (avatar._path.Count <= 0) return;


        }


        var dir = (avatar._path[0].transform.position - avatar.transform.position);
        dir += avatar.ObstacleAvoindance().normalized * avatar.obstacleWeight; // aunque le sume la fuerzas en path no esquiva.
        avatar.transform.position += dir.normalized * avatar.SpeedMove * Time.deltaTime;

       

        if (dir.magnitude < 0.8f)
        {
            avatar._path.RemoveAt(0);
        }


    }

    

}
