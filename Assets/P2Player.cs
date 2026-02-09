using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Player : MonoBehaviour
{
    [SerializeField] private float _obstacleDistance;
    [SerializeField] private LayerMask _obstacleLayer;
    private Vector3 _avoindaceDir;
    public Vector3 ObstacleAvoindance()
    {
        _avoindaceDir = Vector3.zero;
        var obstacles = Physics.OverlapSphere(transform.position, _obstacleDistance, _obstacleLayer);
        
        if(obstacles.Length>0)
        {
            foreach (var obstacle in obstacles)
            {
                var dir = transform.position - obstacle.transform.position;
                _avoindaceDir += dir.normalized * (_obstacleDistance - dir.magnitude); //entre mas cerca del objeto mas peso para esquivar. normalizado a 1 dentro del rango de esquive va a valer 

            }

        }

        _avoindaceDir.z = 0;
        return _avoindaceDir;
    }


    private void Update()
    {
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _obstacleDistance);
    }
}
