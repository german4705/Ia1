using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public FSM<LeaderStates, Leader> fSMLeader;

    public float energy;
    public float SpeedMove;public float SpeedBase;
    public Vector3 targetMove;
    public Camera cam;
    public Team team;
    public GameObject BaseA;
    public GameObject BaseB;


    //variables Path
    public List<Nodes> _path = new List<Nodes>();
   


    

    [SerializeField] LayerMask wallLayer;

    public Transform target;
    public Nodes currenteNode;

    public float radius;
    public LayerMask obstacleLayer;
    public float obstacleWeight;

    private void Awake()
    {
        targetMove = transform.position;
        cam = Camera.main;
        fSMLeader = new FSM<LeaderStates, Leader>();

        
        fSMLeader._possibleStates.Add(LeaderStates.LeaderMove, new LeaderMove().SetUp(fSMLeader).SetAvatar(this));
        fSMLeader._possibleStates.Add(LeaderStates.LeaderGoToBase, new LeaderGoToBase().SetUp(fSMLeader).SetAvatar(this));
        fSMLeader._possibleStates.Add(LeaderStates.LeaderRecovery, new LeaderRecovery().SetUp(fSMLeader).SetAvatar(this));

        fSMLeader.ChangeState(LeaderStates.LeaderMove);

    }
    void Start()
    {
        targetMove = transform.position;
    }

    
    void Update()
    {
        fSMLeader._actualState.OnUpdate();
    }


    private void OnDrawGizmos()
    {
        foreach (var item in _path)
        {

            Gizmos.DrawWireSphere(item.transform.position, 4f);
        }


        Gizmos.DrawWireSphere(transform.position, radius);
        //if (GameManager.Instance.Onsigth(transform.position, targetMove))

        //{
        //    Gizmos.color = Color.green;


        //}
        //else
        //{
        //    Gizmos.color = Color.red;

        //}

        //Gizmos.DrawLine(transform.position, targetMove);
    }


    public Vector3 ObstacleAvoindance()
    {
        Vector3 _avoindaceDir = Vector3.zero;

        var obstacles = Physics.OverlapSphere(transform.position, radius, obstacleLayer);

        if (obstacles.Length > 0)
        {
            foreach (var obstacle in obstacles)
            {
                var dir = transform.position - obstacle.transform.position;
                _avoindaceDir += dir.normalized * (radius - dir.magnitude); //entre mas cerca del objeto mas peso para esquivar. el numero es mas grande siendo el maximo el radio.

            }

        }

        _avoindaceDir.z = 0;
        return _avoindaceDir;
    }

}
