using System;
using System.Runtime.CompilerServices;
using Succide.Core.Behavioural;
using Succide.Core.Common;
using Succide.Core.Player;
using Succide.Core.Player.Measuring;
using Succide.Core.Things.Common;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Pharmacy
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	[RequireComponent(typeof(HealthfulBehaviour))]
	public class PainkillerController : MonoBehaviour
	{
		private const float duration = 120f;
		private const float toleranceMin = 0.2f;
		private const float toleranceMax = 10f;
		private const float moodPerSecondMax = 9f;
		private const float moodPerSecondMin = -.5f;

		private static readonly ConditionalWeakTable<
			PlayerController,
			Store<float>
		> playerToTolerance = new();
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
			var tolerance = playerToTolerance.GetValue(
				PlayerController.instance!,
				(_) => new Store<float>(toleranceMin)
			);
			var tickCount = 0;

			float OnTick(float v)
			{
				if (tickCount >= duration * Measurable.ticksPerSecond)
				{
					PlayerMeasurableProvider
						.Get(PlayerMeasurableKinds.Mood)
						.transformer -= OnTick;

					return v;
				}

				++tickCount;

				return v
					+ Measurable.ToPerTick(
						Mathf.Clamp(
							moodPerSecondMax - tolerance!.value,
							moodPerSecondMin,
							moodPerSecondMax
						)
					);
			}

			PlayerMeasurableProvider
				.Get(PlayerMeasurableKinds.Mood)
				.transformer += OnTick;
			PlayerMeasurableProvider.Get(PlayerMeasurableKinds.Health).value -=
				Mathf.Ceil(tolerance.value);
			PlayerMeasurableProvider.Get(PlayerMeasurableKinds.Wealth).value -=
				1;

			tolerance.value = Mathf.Clamp(
				tolerance.value * 2,
				toleranceMin,
				toleranceMax
			);
		}
	}
}
