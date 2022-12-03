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

		private bool isOnGround => movementController!.isOnGround.value;
		private bool isOnWall => movementController!.isOnWall.value;
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
						anim!.Play("Jump, Left");
					}
					else
					{
						// idling
						anim!.Play("Idle, Bop");
					}
				}
				else
				{
					// walking
					anim!.Play("Walk, Left");
				}
			}
			else
			{
				if (verticalAxis < 0)
				{
					if (isFiring)
					{
						// fly crouching firing
						anim!.Play("Jump, Left");
					}
					else
					{ // fly crouching
						anim!.Play("Crouch");
					}
				}
				else
				{
					// jumping
					anim!.Play("Jump, Left");
				}
			}
		}
	}
}
