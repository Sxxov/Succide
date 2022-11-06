using Succide.Core.Behavioural;
using Succide.Core.Player.Measuring;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Fun
{
	public class FunController : MonoBehaviour
	{
		void Awake()
		{
			PlayerMeasurableProvider
				.Get(PlayerMeasurableKinds.Wealth)
				.transformer += OnTick;
		}

		void OnDestroy()
		{
			PlayerMeasurableProvider
				.Get(PlayerMeasurableKinds.Wealth)
				.transformer -= OnTick;
		}

		private float OnTick(float v) => v - Measurable.ToPerTick(.5f);
	}
}
