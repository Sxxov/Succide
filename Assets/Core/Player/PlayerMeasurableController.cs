using System;
using Succide.Core.Behavioural;
using UnityEngine;
using Succide.Core.Common;
using Succide.Core.Player.Measuring;
using Succide.Core.Routing;

namespace Succide.Core.Player
{
	public class PlayerMeasurableController : MonoBehaviour
	{
		public readonly Measurable mood = PlayerMeasurableProvider.Get(
			PlayerMeasurableKinds.Mood
		);
		public readonly Measurable wealth = PlayerMeasurableProvider.Get(
			PlayerMeasurableKinds.Wealth
		);
		public readonly Measurable health = PlayerMeasurableProvider.Get(
			PlayerMeasurableKinds.Health
		);
		public readonly Measurable time = PlayerMeasurableProvider.Get(
			PlayerMeasurableKinds.Time
		);

		async void Awake()
		{
			await RouteComptroller.instance!.AwaitAway(RouteKinds.Intro);

			time.transformer += (v) => v + Measurable.ToPerTick(1);

			// ambient withering
			mood.transformer += (v) => v - Measurable.ToPerTick(.5f);
			wealth.transformer += (v) => v - Measurable.ToPerTick(.5f);
			health.transformer += (v) => v - Measurable.ToPerTick(.5f);

			// accelerated withering
			mood.transformer += (v) =>
				v
				- Measurable.ToPerTick(
					wealth.value switch
					{
						var w when w < 50 => .5f,
						var w when w < 25 => 1f,
						var w when w < 12.5f => 1.5f,
						var w when w < 6.25f => 2f,
						var w when w < 3.175f => 2.5f,
						_ => 0,
					}
				);
			health.transformer += (v) =>
				v
				- Measurable.ToPerTick(
					mood.value switch
					{
						var w when w < 25 => 1f,
						var w when w < 12.5f => 1.5f,
						var w when w < 6.25f => 2f,
						var w when w < 3.175f => 2.5f,
						_ => 0,
					}
				);

			StartCoroutine(time);
			StartCoroutine(mood);
			StartCoroutine(wealth);
			StartCoroutine(health);
		}
	}
}
