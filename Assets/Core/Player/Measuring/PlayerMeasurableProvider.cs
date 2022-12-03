using System;
using Succide.Core.Behavioural;
using UnityEngine;
using Succide.Core.Common;
using Succide.Core.Player.Measuring;
using System.Linq;

namespace Succide.Core.Player.Measuring
{
	public class PlayerMeasurableProvider
	{
		private static readonly PlayerMeasurable[] measurables =
			new PlayerMeasurable[]
			{
				new PlayerMeasurable(
					kind: PlayerMeasurableKinds.Mood,
					initial: 100,
					min: 0,
					max: 100
				),
				new PlayerMeasurable(
					kind: PlayerMeasurableKinds.Wealth,
					initial: 100,
					min: 0,
					max: 100
				),
				new PlayerMeasurable(
					kind: PlayerMeasurableKinds.Health,
					initial: 100,
					min: 0,
					max: 100
				),
				new PlayerMeasurable(
					kind: PlayerMeasurableKinds.Time,
					initial: 0,
					min: 0,
					max: float.PositiveInfinity
				),
				new PlayerMeasurable(
					kind: PlayerMeasurableKinds.MoodGrossGain,
					initial: 0,
					min: 0,
					max: float.PositiveInfinity
				),
			};

		public static PlayerMeasurable Get(PlayerMeasurableKinds kind) =>
			(PlayerMeasurable?)Array.Find(measurables, (v) => v.kind == kind)
			?? throw new NotSupportedException();
	}
}
