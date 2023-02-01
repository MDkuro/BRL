using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckIfPlayerWatch : Conditional
{
	public SharedGameObject player;
	private FinalMovement hidePlayer;
	public override void OnStart()
	{
		var playerObject = GetDefaultGameObject(player.Value);

		hidePlayer = playerObject.GetComponent<FinalMovement>();
	}

	public override TaskStatus OnUpdate()
	{
		var watchState = hidePlayer.canAttack;
		if(watchState == true)
        {
			return TaskStatus.Success;
        }
        else
        {
			return TaskStatus.Failure;
		}
	}

}