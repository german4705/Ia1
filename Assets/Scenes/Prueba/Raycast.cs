using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public LayerMask layer;
    


    void Update()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction*100f, Color.red);

        if (Physics.Raycast(origin, direction, out hit, 100, layer))
        {
            Debug.Log(hit.collider.gameObject.name);
        }

    }

    
}
