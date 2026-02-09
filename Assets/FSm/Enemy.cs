using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public FSM<EnemyStates,Enemy> _fsm;  // cuando refencio para usar la fsm , defino quien lo usa.
    public Team team;

    public GameObject LeaderA;
    public GameObject LeaderB;

    
    public float speedLeader;
    public float MaxspeedLeader;
    public float AcelerationLeader;

    public float speedBase;
    public float speedchase;
    public float speedDirection; // velocidad de rotacion hacia la direccion deseada
    

    public float DetectionRange;
    public float _energy;


    //variables attack
    public Enemy EnemyNpc;
    public float AttackForce;
    public float AttackVelocity;
    
    
    public float EnergyReduceAttack;
    public float EnergyReduceChase;
    public float RadiusAttack;
    public float RadiusSeparation;
    public float SeparationWeight;
    public float SeparationWeightLeader;

    public LayerMask ObjectLiveLayer;
    public float viewangle;

    
    public LayerMask _obstacleLayer;
    [Range(0f,10f)] public  float obstacleWeight;


    public GameObject BaseA;
    public GameObject BaseB;

    public List<Nodes> _path = new List<Nodes>();

    Vector3 dirA;
    Vector3 dirB;

    void Awake()
    {
        _fsm = new FSM<EnemyStates,Enemy>();    //creamos una nueva FSM

        _fsm._possibleStates.Add(EnemyStates.RecoverState, new RecoverState().SetUp(_fsm).SetAvatar(this)); //creo el primer estado idle con el diccionario que esta en la FSM. y lo seteo con la funcion de Basestate.
        _fsm._possibleStates.Add(EnemyStates.AttackState, new AttackState().SetUp(_fsm).SetAvatar(this));
        _fsm._possibleStates.Add(EnemyStates.GoToBaseState, new GoToBaseState().SetUp(_fsm).SetAvatar(this));
        _fsm._possibleStates.Add(EnemyStates.FollowLeaderState, new FollowLeaderState().SetUp(_fsm).SetAvatar(this));
        _fsm._possibleStates.Add(EnemyStates.BeingAttackedState, new BeingAttackedState().SetUp(_fsm).SetAvatar(this));


        _fsm.ChangeState(EnemyStates.FollowLeaderState);

    }
    
    
    public void ReduceEnergy(float Reduce)
    {
        _energy -= Time.deltaTime * Reduce;
        _energy = Mathf.Clamp(_energy, 0, 100);
    }
        
       


    public void RecoveryEnergy(float Recovery)
    {
        _energy += Time.deltaTime * Recovery;
        _energy = Mathf.Clamp(_energy, 0, 100);
    }
    public void Update()
    {
        _fsm._actualState.OnUpdate();

    }

       



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, RadiusAttack);

        Gizmos.color = Color.blue;
       

        var dirA =  DirFromAngle(viewangle / 2 + transform.eulerAngles.z);
        var dirB =  DirFromAngle(-viewangle / 2 + transform.eulerAngles.z);

        Gizmos.DrawLine(transform.position, transform.position + dirA * RadiusAttack);
        Gizmos.DrawLine(transform.position, transform.position + dirB * RadiusAttack);
    }

    private Vector3 DirFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }
}


