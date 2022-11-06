using System;
using System.Collections;

namespace Succide.Core.Common
{
	public class Ticker : IEnumerator
	{
		private readonly float ticksPerSecond;
		public event Action? OnTick;
		private long prevTime = long.MinValue;

		public Ticker(float ticksPerSecond)
		{
			this.ticksPerSecond = ticksPerSecond;
		}

		public bool MoveNext()
		{
			var currTime =
				DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond / 1000;

			if (prevTime == long.MinValue)
			{
				prevTime = currTime;
			}

			var timeDelta = currTime - prevTime;
			var rawTickCount = timeDelta * ticksPerSecond;
			var handledTickCount = (int)rawTickCount;
			var remainingTickCount = rawTickCount - handledTickCount;
			var nextTime =
				currTime - (long)(remainingTickCount / ticksPerSecond);

			for (var i = 0; i < handledTickCount; ++i)
			{
				OnTick?.Invoke();
			}

			prevTime = nextTime;

			return true;
		}

		public void Reset()
		{
			prevTime = long.MinValue;
		}

		object? IEnumerator.Current => Current;

#pragma warning disable IDE1006
		public object? Current => null;
#pragma warning restore

		public float ToPerTick(float perSecond)
		{
			return perSecond / ticksPerSecond;
		}

		public float ToPerSecond(float perTick)
		{
			return perTick * ticksPerSecond;
		}
	}
}
