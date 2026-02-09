using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] LayerMask wallLayer;
    

    

    public Transform target;
    public Nodes currentNodes;


    public List<Nodes> allNodes;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        


    }


    public Nodes GetNearestNode(Vector3 position, List<Nodes> nodes)
    {
        Nodes nearestNode = null;
        float shortestDistance = Mathf.Infinity;//para que entre el primero.

        foreach (Nodes node in nodes)
        {

            float distance = Vector3.Distance(position, node.transform.position);
            if (distance < shortestDistance && Onsigth(position,node.transform.position)) //verifica si esta en vision
            {
                shortestDistance = distance; // para hacer el chequeo arriba
                nearestNode = node;
            }
           
        }

        return nearestNode;
    }

    //line of sight
    public bool Onsigth(Vector3 start, Vector3 end) 
    {
        var dir = end - start;
        return !Physics.Raycast(start,dir, dir.magnitude,wallLayer); //true cuando no choca. 

        
    }

    public bool FielfOdView(Vector3 from, Vector3 forward, Vector3 target, float viewangle)
    {
        var dir = target - from; //vector direccion hacia el enemigo
        if (Vector3.Angle(forward, dir) < viewangle /2  && GameManager.Instance.Onsigth(from, target)) // viewangle vale 90, si el resultado de verctor.angle es menor a 45 esta en rango de vision , ademas si no hay una pared
        {
            return true;
            
        }

        return false;

    }


}
