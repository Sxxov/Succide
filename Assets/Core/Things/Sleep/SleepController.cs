using Succide.Core.Behavioural;
using Succide.Core.Player.Measuring;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Sleep
{
	public class SleepController : MonoBehaviour
	{
		void Awake()
		{
			PlayerMeasurableProvider
				.Get(PlayerMeasurableKinds.Health)
				.transformer += OnTick;
		}

		void OnDestroy()
		{
			PlayerMeasurableProvider
				.Get(PlayerMeasurableKinds.Health)
				.transformer -= OnTick;
		}

		private float OnTick(float v) => v + Measurable.ToPerTick(1f);
	}
}
