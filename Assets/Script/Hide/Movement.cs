using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class Movement : MonoBehaviour
{
    private Rabbit rabbit;
    private Rigidbody2D rigidbody;
    public Player player;
    public float speed;
    public float move;
    public Vector3 front = new Vector3(-1, 0, 0);
    public RootManager rootTarget;
    public int startId;
    public int endId;
    List<CheckPoint> thePath = new List<CheckPoint>();

    private void Start()
    {
        rabbit = GetComponent<Rabbit>();
        rigidbody = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        
        Vector3 beg = transform.position + new Vector3(0, 2.0f, 0);
        Vector3 bottom = transform.position + new Vector3(0, 0.7f, 0);
        Vector3 down = new Vector3(0, -1, 0);
        Vector3 up = new Vector3(0, 1, 0);
        
        bool jump = false;

        endId = player.playerId;
        if (startId == null || endId == null)
        {
            return;
        }
        
        
        if(startId != endId)
        {
            thePath = rootTarget.FindPath(startId, endId);
            var point1 = thePath[0];
            var point2 = thePath[1];
            var saveThePath = rootTarget.oneWayRoots.First(getPath => point1.Id == getPath.startPointId && point2.Id == getPath.endPointId);
            var direction = saveThePath.direction;
            if(direction == OneWayDirection.Left || direction == OneWayDirection.LeftJump)
            {
                front.x = -1;
            }
            else if (direction == OneWayDirection.Right || direction == OneWayDirection.RightJump)
            {
                front.x = 1;
            }
            if(direction == OneWayDirection.LeftJump || direction == OneWayDirection.RightJump)
            {
                jump = true;
            }
            else if (direction != OneWayDirection.LeftJump || direction != OneWayDirection.RightJump)
            {
                jump = false;
            }
            move = front.x * speed;
            rabbit.Move(move, jump);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            var getCheckPoint = other.gameObject.GetComponent<CheckPoint>();
            startId = getCheckPoint.Id;
        }

    }








}
