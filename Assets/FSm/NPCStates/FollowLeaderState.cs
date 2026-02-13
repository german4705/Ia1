using UnityEngine;

public class FollowLeaderState : BaseStates<EnemyStates,Enemy>
{

    public bool targetpath;
    
    public override void OnUpdate()
    {
        //Aceleracion para seguir al lider
        avatar.speedLeader = Mathf.Clamp(avatar.speedLeader, 0, avatar.MaxspeedLeader);


        //un operador ternario para definir el leader
        Vector3 targetLeader = avatar.team == Team.TeamA ? avatar.LeaderA.transform.position : avatar.LeaderB.transform.position;



        Vector3 directionToLeader = targetLeader - avatar.transform.position;

        //Direccion deseada sumando todos los vectores.
        directionToLeader += Separation().normalized * avatar.SeparationWeight + SeparationLeader().normalized * avatar.SeparationWeightLeader + ObstacleAvoindance() * avatar.obstacleWeight; //ACA SAQUE EL .Normalized que tenia el ObstacleAvoindance()

        //Lerpeo entre mi direccion y la direccion deseada.
        var newDirection = Vector3.Lerp(avatar.transform.right, directionToLeader, avatar.speedDirection);

        avatar.transform.right = newDirection;
        newDirection.z = 0;

        targetpath = GameManager.Instance.Onsigth(avatar.transform.position, targetLeader); //true si no hay ninguna pared entre el enemy el leader del mismo equipo.

        if(targetpath)
        {
            avatar.transform.position += newDirection.normalized * Time.deltaTime * avatar.speedLeader;


        }else
        {
            pathFinding();
        }
        



        

        //Arrive velocidad acelerando o desacelerando 
        if(directionToLeader.magnitude<3)
        {
            float slowingRadius = 3f;
            float distance = directionToLeader.magnitude;
            float rampedSpeed = avatar.MaxspeedLeader * (distance / slowingRadius);
            float clippedSpeed = Mathf.Min(rampedSpeed, avatar.MaxspeedLeader);
            avatar.speedLeader = clippedSpeed; // desacelera con la distancia
        }
        else
        {
            avatar.speedLeader += avatar.AcelerationLeader * Time.deltaTime; //acelera con el tiempo.
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////




        //Condicion para atacar // solo ataca el primero que esta en FOV ese es el que gana. el otro no ataca // podria haberlo hecho que se alerte pero me parecio mejor asi. 
        var Attack = Physics.OverlapSphere(avatar.transform.position, avatar.RadiusAttack, avatar.ObjectLiveLayer);
        if (Attack.Length > 0 ) // por que Attack me devuelve un array de colliders, osea si hay una por lo menos.
        {
            foreach (Collider col in Attack)
            {
                Enemy enemy = col.GetComponent<Enemy>(); //el componete enemy

                if (enemy.team!= avatar.team) //si el enemy no es de mi equipo.
                {
                    

                    if (GameManager.Instance.FielfOdView(avatar.transform.position, avatar.transform.right, enemy.transform.position, avatar.viewangle))
                    {
                        avatar.EnemyNpc = enemy;//lo guardo en EnemyNPC
                        fsm.ChangeState(EnemyStates.AttackState);
                        enemy._fsm.ChangeState(EnemyStates.BeingAttackedState);
                    }
                    
                    
                    
                    
                }
               
            }
        }

        
       

    }


    public Vector3 Separation()
    {
        var NpcFriends = Physics.OverlapSphere(avatar.transform.position, avatar.RadiusSeparation, avatar.ObjectLiveLayer);
        Vector3 separation = Vector3.zero;

        if (NpcFriends.Length>0)
        {
            
            foreach (var Npc in NpcFriends)
            {
                separation += avatar.transform.position - Npc.transform.position;

            }
        }

        return separation;
    }

    public Vector3 SeparationLeader()
    {
        Vector3 separation = Vector3.zero;

        Vector3 targetLeader = avatar.team == Team.TeamA ? avatar.LeaderA.transform.position : avatar.LeaderB.transform.position;

        Vector3 DistanceLeader = targetLeader - avatar.transform.position;
        if(DistanceLeader.magnitude<4)
        {
            separation += avatar.transform.position - targetLeader; //le sumo la direccion contraria si estoy cerca
            return separation;
            
        }
        else
        {
            return separation;
        }
        

        // Mejorar el separation con el lider
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
        Vector3 targetLeader = avatar.team == Team.TeamA ? avatar.LeaderA.transform.position : avatar.LeaderB.transform.position;

       

        if (avatar._path.Count <= 0)
        {



            Nodes start = GameManager.Instance.GetNearestNode(avatar.transform.position, GameManager.Instance.allNodes);
            Nodes target = GameManager.Instance.GetNearestNode(targetLeader, GameManager.Instance.allNodes);

            if (start == null || target == null)
            {
                Debug.LogError("start o target node es NULL"); //AGREGE ESTE DEBUG ERROR POR SI HAY QUE ARREGLAR ESTO DESPUES
                return;
            }

            avatar._path = PathFinding.Instace.GetPath(start, target);


            
            if (avatar._path.Count <= 0) return;


        }


        var dir = (avatar._path[0].transform.position - avatar.transform.position);
        //avatar.transform.position += dir.normalized * avatar.speedLeader * Time.deltaTime; //ESTA LINEA ESTABA ANTES, LAS DE ABAJO SON LAS QUE REEMPLAZAN A ESTA

        Vector3 desired = dir.normalized + ObstacleAvoindance() * avatar.obstacleWeight;
        avatar.transform.position += desired.normalized * avatar.speedLeader * Time.deltaTime;


        if (dir.magnitude < 0.8f)
        {
            avatar._path.RemoveAt(0);
        }




    }
    
   
}
