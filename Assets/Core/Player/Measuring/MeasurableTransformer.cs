using System;
using System.Linq;
using UnityEngine;

namespace Succide.Core.Player.Measuring
{
	public class MeasurableTransformer
	{
		private readonly Func<float, float>[] pipes;

		public MeasurableTransformer(params Func<float, float>[] pipes)
		{
			this.pipes = pipes;
		}

		public MeasurableTransformer(
			params MeasurableTransformer[] transformers
		)
		{
			pipes = transformers.Aggregate(
				new Func<float, float>[] { },
				(prev, curr) => prev.Concat(curr.pipes).ToArray()
			);
		}

		public float Transform(float value)
		{
			foreach (var pipe in pipes)
			{
				value = pipe(value);
			}

			return value;
		}

		public static MeasurableTransformer operator +(
			MeasurableTransformer? a,
			MeasurableTransformer b
		)
		{
			if (a is null)
			{
				return new(b.pipes);
			}

			return new(a.pipes.Concat(b.pipes).ToArray());
		}

		public static MeasurableTransformer operator +(
			MeasurableTransformer? a,
			Func<float, float> b
		)
		{
			if (a is null)
			{
				return new(b);
			}

			var pipes = new Func<float, float>[a.pipes.Length + 1];
			a.pipes.CopyTo(pipes, 0);
			pipes[a.pipes.Length] = b;

			return new(pipes);
		}

		public static MeasurableTransformer operator -(
			MeasurableTransformer? a,
			MeasurableTransformer b
		)
		{
			if (a is null)
			{
				return new(new Func<float, float>[] { });
			}

			var pipes = a.pipes.ToList();

			foreach (var pipe in b.pipes)
			{
				pipes.Remove(pipe);
			}

			return new(pipes.ToArray());
		}

		public static MeasurableTransformer operator -(
			MeasurableTransformer? a,
			Func<float, float> b
		)
		{
			if (a is null)
			{
				return new(new Func<float, float>[] { });
			}

			if (!a.pipes.Contains(b))
			{
				return new(a.pipes);
			}

			var pipes = a.pipes.ToList();

			pipes.Remove(b);

			return new(pipes.ToArray());
		}
	}
}
