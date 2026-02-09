
using System.Collections.Generic;


public class PriorityQueve //Lista que se ordena por numero 
{
    //Almacenamiento de datos , Nodos y Heuristica. Para seleccionar el Nodo de menor Heuristica
    Dictionary<Nodes, float> priorityQueve = new Dictionary<Nodes, float>();


    public void Enqueve(Nodes node, float heuristic) //agregar 
    {
        if(priorityQueve.ContainsKey(node))
        {
            priorityQueve[node] = heuristic;
        }
        else
        {
            priorityQueve.Add(node, heuristic);
        }
    }

    public Nodes Dequeve() //Quitar de la Queve
    {
        if (priorityQueve.Count == 0) return null;

        Nodes minNode = null;

        foreach (var item in priorityQueve)
        {
            if(minNode==null)
            {
                minNode = item.Key; // si es nulo lo agragamos 
            }
            else if(item.Value < priorityQueve[minNode]) // evalua si el nodo es menor al de menor valor.
            {
                minNode = item.Key; // setear al de menor heuristica
            }

        }

        priorityQueve.Remove(minNode);
        return minNode;
    }

    public int Count ()
    {
        return priorityQueve.Count;
    }


    public void Claer()
    {
        priorityQueve.Clear();
    }

    public bool Contains(Nodes node)
    {
        return priorityQueve.ContainsKey(node);
    }

  
    
}
