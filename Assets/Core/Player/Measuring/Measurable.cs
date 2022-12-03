using System.Collections.Generic;
using System;
using System.Collections;
using Succide.Core.Common;
using UnityEngine;

namespace Succide.Core.Player.Measuring
{
	public class Measurable : Store<float>, IEnumerator
	{
		public const float ticksPerSecond = 60;
		private readonly float initial;
		public readonly float min;
		public readonly float max;
		public MeasurableTransformer? transformer;
		private readonly Ticker ticker = new(ticksPerSecond);

		public Measurable(float initial, float min, float max) : base(initial)
		{
			this.initial = initial;
			this.max = max;
			this.min = min;

			ticker.OnTick += () =>
				value = Mathf.Clamp(
					transformer?.Transform(value) ?? value,
					min,
					max
				);
		}

		public bool MoveNext() => ticker.MoveNext();

		public void Reset()
		{
			Reset(initial);
			ticker.Reset();
			transformer = null;
		}

		object IEnumerator.Current => Current;

#pragma warning disable IDE1006
		public float Current => value;
#pragma warning restore

		public static float ToPerTick(float perSecond)
		{
			return perSecond / ticksPerSecond;
		}

		public static float ToPerSecond(float perTick)
		{
			return perTick * ticksPerSecond;
		}
	}
}
