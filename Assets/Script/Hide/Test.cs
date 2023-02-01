using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Test : MonoBehaviour
{
    
    public int startId;
    public int endId;
    private RootManager rootManager;



    List<CheckPoint> list = new List<CheckPoint>();

    private void Start()
    {
        rootManager = GetComponent<RootManager>();
    }

    private void Update()
    {
        if(startId == null || endId == null)
        {
            return;
        }

    }

    

    private void OnDrawGizmos()
    {

        if (list.Count < 2)
        {
            return;
        }


        for (int i = 0; i < list.Count - 1; i++)
        {
            Gizmos.DrawLine(list[i].gameObject.transform.position, list[i + 1].gameObject.transform.position);
        }



    }
}
