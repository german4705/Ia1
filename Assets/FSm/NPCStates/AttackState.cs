using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseStates<EnemyStates,Enemy>
{

    public override void OnEnter()
    {
        Debug.Log("Entre a Atack");
    }

    public override void OnUpdate()
    {
        // pensa que tiene que seguir a uno solo de todos los que estan dentro de la esfera

        //cuando un enemigo se acerca lo ataca hasta que lo mata o su vida sea menor a 10.
        //pasa a patrol si no hay mas enemigos cerca o pasa a iddle si su vida es inferior a 10
        
       
        
        


        

        var chaseVector = avatar.EnemyNpc.transform.position - avatar.transform.position;
        avatar.transform.right = chaseVector;
        

        if (chaseVector.magnitude < 1f)
        {
            avatar.EnemyNpc.ReduceEnergy(avatar.AttackForce);
            
        }
        else if(chaseVector.magnitude > 1f && chaseVector.magnitude < 5f)
        {
            avatar.transform.position += chaseVector.normalized * Time.deltaTime * avatar.AttackVelocity;
        }else
        {
            fsm.ChangeState(EnemyStates.FollowLeaderState);
        }

        if(avatar._energy<20)
        {
            fsm.ChangeState(EnemyStates.FollowLeaderState);
        }

        
    }    

    public override void OnExit()
    {
        Debug.Log("Sali del Atack");
    }
}
