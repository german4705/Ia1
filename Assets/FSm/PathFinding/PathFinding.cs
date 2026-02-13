using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    //Guarda nodos sin repetir
    private HashSet<Nodes> _closedNodes = new HashSet<Nodes>(); 

    //cerrado hace refenrecia a nodo que ya chequeo a los vecinos

    private PriorityQueve _openNodes = new PriorityQueve();
    //En abiertos uso la PriorityQueve para ver saber cual va a ser el de menor valor. Nodos por visitar

    public static PathFinding Instace;

    public List<Nodes> nodes = new List<Nodes>(); // se agregan todos los nodos(En el awake de Nodes).

    
    
    private void Awake()
    {
        Instace = this;
    }
    public List<Nodes> GetPath(Nodes starNode, Nodes endNode)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance es NULL");
            return null;
        }

        if (GameManager.Instance.allNodes == null)
        {
            Debug.LogError("allNodes es NULL");
            return null;
        }


        foreach (var node in GameManager.Instance.allNodes)
        {
            node.OnReset(); //limpiamos la heuristica y el nodo previo
        }

        _closedNodes.Clear();
        _openNodes.Claer();

        starNode.Heuristic = 0;

        var actualNode = starNode;
          
       

        while (actualNode != null && actualNode != endNode)
        {
            foreach (var neighbor in actualNode.neighbors) // revisamos los vecinos de actial node
            {
                if (_closedNodes.Contains(neighbor)) continue; // si ya esta revisado, pasa al siguiente

                var heuritic = actualNode.Heuristic + 1 + Vector3.Distance (neighbor.transform.position, endNode.transform.position);

                if (neighbor.Heuristic > heuritic) //si la heuristica actual es mayor a la que calculamos , cambiala asi me quedo con la menor si es menor se queda con la anterior.
                {
                    neighbor.Heuristic = heuritic; //seteamos el primero con esa heuristica
                    neighbor.previusNode = actualNode;
                }

                _openNodes.Enqueve(neighbor, neighbor.Heuristic); //agregamos a posibles nodos a visitar

            }

            _closedNodes.Add(actualNode); //ya se revisaron todos los caminos , nodo cerrado.

            actualNode = _openNodes.Dequeve();
        }

        var finalPath = new List<Nodes>();

        actualNode = endNode;
        var actualpreviusNode = actualNode.previusNode;
        finalPath.Add(actualNode);

        while(actualNode!=null && actualNode !=starNode && actualNode.previusNode!=null && actualpreviusNode.previusNode !=null)
        {
            bool choco = GameManager.Instance.Onsigth(actualpreviusNode.transform.position, actualNode.transform.position);
            

            if (GameManager.Instance.Onsigth(actualNode.transform.position, actualpreviusNode.previusNode.transform.position))
            {
                actualpreviusNode = actualpreviusNode.previusNode;
                Debug.DrawLine(actualpreviusNode.transform.position, actualNode.transform.position, Color.blue, 5f);
            }
            else
            {
                Debug.DrawLine(actualpreviusNode.transform.position, actualNode.transform.position, Color.red, 5f);
                finalPath.Add(actualpreviusNode);
                actualNode = actualpreviusNode;
                actualpreviusNode = actualpreviusNode.previusNode;
            }
             
        }

        finalPath.Reverse();
        return finalPath;
    }

}
