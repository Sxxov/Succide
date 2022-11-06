using Succide.Core.Behavioural;
using Succide.Core.Player.Measuring;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Fun
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	[RequireComponent(typeof(HealthfulBehaviour))]
	public class BalloonController : MonoBehaviour
	{
		private SelfDestructBehaviour selfDestruct = null!;

		void Awake()
		{
			selfDestruct = GetComponent<SelfDestructBehaviour>();
			selfDestruct!.OnSelfDestruct += OnSelfDestruct;
		}

		void OnDestroy()
		{
			selfDestruct!.OnSelfDestruct -= OnSelfDestruct;
		}

		void OnSelfDestruct()
		{
			PlayerMeasurableProvider.Get(PlayerMeasurableKinds.Mood).value += 1;
		}
	}
}
