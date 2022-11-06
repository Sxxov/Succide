using UnityEngine;

namespace Succide.Core.Player
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(PlayerMovementController))]
	[RequireComponent(typeof(PlayerFireController))]
	public class PlayerAnimationController : MonoBehaviour
	{
		private PlayerMovementController movementController = null!;
		private PlayerFireController fireController = null!;
		private Animator anim = null!;

		private bool isOnGround => movementController!.isOnGround;
		private bool isOnWall => movementController!.isOnWall;
		private bool isFiring => fireController!.isFiring.value;
		private float horizontalAxis => movementController!.horizontalAxis;
		private float verticalAxis => movementController!.verticalAxis;

		void Awake()
		{
			movementController = GetComponent<PlayerMovementController>()!;
			fireController = GetComponent<PlayerFireController>()!;
			anim = GetComponent<Animator>()!;
		}

		void Update()
		{
			if (isOnGround)
			{
				if (horizontalAxis == 0)
				{
					if (isFiring)
					{
						// idling firing
						anim!.Play("Character, Shoot, Left");
					}
					else
					{
						// idling
						anim!.Play("Character, Idle");
					}
				}
				else
				{
					// walking
					anim!.Play("Character, Walk, Left");
				}
			}
			else
			{
				if (verticalAxis < 0)
				{
					if (isFiring)
					{
						// fly crouching firing
						anim!.Play("Character, Shoot, Left");
					}
					else
					{ // fly crouching
						anim!.Play("Character, Idle");
					}
				}
				else
				{
					// jumping
					anim!.Play("Character, Jump, Left");
				}
			}
		}
	}
}
