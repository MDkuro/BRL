using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Linq;

public class Flee : Action
{
	public SharedGameObject rootManager;
	public SharedGameObject player;
	public SharedGameObject boss;
	private RootManager rootTarget;
	private Player hidePlayer;
	private Movement movement;
	private Rabbit rabbit;

	public override void OnStart()
	{
		var currentGameObject = GetDefaultGameObject(rootManager.Value);
		var playerObject = GetDefaultGameObject(player.Value);
		var movemontObject = GetDefaultGameObject(boss.Value);

		rootTarget = currentGameObject.GetComponent<RootManager>();
		hidePlayer = playerObject.GetComponent<Player>();
		movement = movemontObject.GetComponent<Movement>();
		rabbit = movemontObject.GetComponent<Rabbit>();
	}

	public override TaskStatus OnUpdate()
	{
        //找出离玩家最远的点
        //寻路
        //如果BOSS在路上，返回task.running，如果走到了，返回success

        var endId = 1;
        var startId = movement.startId;
        var front = movement.front;
        bool jump = false;
        if(hidePlayer.playerId == 17)
        {
            endId = 38;
        }else if(hidePlayer.playerId == 1)
        {
            endId = 50;
        }
        else if (hidePlayer.playerId == 50)
        {
            endId = 3;
        }

        if (startId != endId)
        {
            var thePath = rootTarget.FindPath(startId, endId);
            var point1 = thePath[0];
            var point2 = thePath[1];
            var saveThePath = rootTarget.oneWayRoots.First(getPath => point1.Id == getPath.startPointId && point2.Id == getPath.endPointId);
            var direction = saveThePath.direction;
            if (direction == OneWayDirection.Left || direction == OneWayDirection.LeftJump)
            {
                front.x = -1;
            }
            else if (direction == OneWayDirection.Right || direction == OneWayDirection.RightJump)
            {
                front.x = 1;
            }
            if (direction == OneWayDirection.LeftJump || direction == OneWayDirection.RightJump)
            {
                jump = true;
            }
            else if (direction != OneWayDirection.LeftJump || direction != OneWayDirection.RightJump)
            {
                jump = false;
            }
            var move = front.x * movement.speed;
            rabbit.Move(move, jump);
        }
        return TaskStatus.Success;
	}
}