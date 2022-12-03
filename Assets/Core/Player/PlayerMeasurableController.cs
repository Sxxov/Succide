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

		public readonly Measurable moodGrossGain = PlayerMeasurableProvider.Get(
			PlayerMeasurableKinds.MoodGrossGain
		);

		async void Awake()
		{
			// wait until the player has finished intro
			await RouteComptroller.instance!.AwaitAway(RouteKinds.Intro);

			// track time
			time.transformer += (v) => v + Measurable.ToPerTick(1);

			// track moodGrossGain
			mood.OnChanging += (v) =>
				moodGrossGain.value += Mathf.Max(v - mood.value, 0);

			// if health is <= 0, go to death route
			var hasDied = false;
			health.OnChanged += async (v) =>
			{
				if (!hasDied && v <= 0 && RouteComptroller.instance)
				{
					hasDied = true;
					await RouteComptroller.instance.Goto(RouteKinds.Death);
				}
			};

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

		void OnDestroy()
		{
			StopCoroutine(time);
			StopCoroutine(mood);
			StopCoroutine(wealth);
			StopCoroutine(health);

			// reset all measurables to their initial values
			mood.Reset();
			wealth.Reset();
			health.Reset();
			time.Reset();
			moodGrossGain.Reset();
		}
	}
}
