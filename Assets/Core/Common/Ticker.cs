using System;
using System.Collections;
using UnityEngine;

namespace Succide.Core.Common
{
	public class Ticker : IEnumerator
	{
		private readonly double ticksPerSecond_;
		private double ticksPerSecond => ticksPerSecond_ * Time.timeScale;
		public event Action? OnTick;
		private double prevTime = double.MinValue;

		public Ticker(double ticksPerSecond)
		{
			ticksPerSecond_ = ticksPerSecond;
		}

		public bool MoveNext()
		{
			var currTime = (double)Time.time;

			if (prevTime == double.MinValue)
			{
				prevTime = currTime;
			}

			if (ticksPerSecond == 0)
			{
				prevTime = currTime;

				return true;
			}

			var timeDelta = currTime - prevTime;
			var rawTickCount = timeDelta * ticksPerSecond;
			var handledTickCount = (int)rawTickCount;
			var remainingTickCount = rawTickCount - handledTickCount;
			var nextTime = currTime - (remainingTickCount / ticksPerSecond);

			for (var i = 0; i < handledTickCount; ++i)
			{
				OnTick?.Invoke();
			}

			prevTime = nextTime;

			return true;
		}

		public void Reset()
		{
			prevTime = double.MinValue;
		}

		object? IEnumerator.Current => Current;

#pragma warning disable IDE1006
		public object? Current => null;
#pragma warning restore

		public double ToPerTick(double perSecond) => perSecond / ticksPerSecond;

		public double ToPerSecond(double perTick) => perTick * ticksPerSecond;
	}
}
