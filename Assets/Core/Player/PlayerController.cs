using UnityEngine;
using Succide.Core.Behavioural;

namespace Succide.Core.Player
{
	[RequireComponent(typeof(PlayerAnimationController))]
	[RequireComponent(typeof(PlayerFireController))]
	[RequireComponent(typeof(PlayerMeasurableController))]
	[RequireComponent(typeof(PlayerMovementController))]
	public class PlayerController : SingletonBehaviour<PlayerController>
	{
		public static GameObject? player =>
			instance ? instance!.gameObject : null;
	}
}
