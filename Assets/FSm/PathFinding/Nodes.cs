using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Nodes : MonoBehaviour
{
    public List<Nodes> neighbors = new List<Nodes>();

    [SerializeField] private float heuristic;
    public float Heuristic
    { set { heuristic = value; } 
      get { return heuristic; }
    }

    public Nodes previusNode;

    private void Awake()
    {
        heuristic = Mathf.Infinity;
    }
    private void Start()
    {
        PathFinding.Instace.nodes.Add(this);
    }

    public void OnReset()
    {
        heuristic = Mathf.Infinity;
        previusNode = null;
    }
    public void SetHeuristic(Vector3 from, Vector3 target, float previousHeuristic)
    {
        //valores de la heuristica: Heuristica del paso anterior + diatancia o costo a paso a actual + distancia o costo a objetivo
        heuristic = previousHeuristic + Vector3.Distance(from, transform.position) + Vector3.Distance(target, transform.position);

        foreach (var item in neighbors)
        {
            Debug.Log(heuristic,item);
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Nodes neighbor in neighbors)
        {

            Debug.DrawLine(transform.position, neighbor.transform.position, Color.green);

        }
    }

}
