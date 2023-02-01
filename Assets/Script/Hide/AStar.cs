using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum E_Node_Type
    {
        None,
        Platform,
        LeftEdge,
        RightEdge,
        Solo
    }

    public class NodeId
    {
        public int x, y;
        public int typeNum;
        public NodeId(int _x, int _y, int _typeNum)
        {
            x = _x;
            y = _y;
            typeNum = _typeNum;
        }
    }

    public class AStarNode
    {
        public int x, y;

        public float f, g, h;
        public NodeId father;

        public E_Node_Type type;
        public List<NodeId> linkTarget = new List<NodeId>();

        public AStarNode(int _x, int _y)
        {
            x = _x;
            y = _y;
            type = E_Node_Type.None;
        }

        public void AddLink(int x, int y, int type)
        {
            linkTarget.Add(new NodeId(x, y, type));
        }
    }
}
