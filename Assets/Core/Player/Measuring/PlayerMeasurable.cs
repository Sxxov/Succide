using System.Collections.Generic;
using System;
using System.Collections;
using Succide.Core.Common;
using UnityEngine;

namespace Succide.Core.Player.Measuring
{
	public class PlayerMeasurable : Measurable
	{
		public readonly PlayerMeasurableKinds kind;

		public PlayerMeasurable(
			PlayerMeasurableKinds kind,
			float initial,
			float min,
			float max
		) : base(initial, min, max)
		{
			this.kind = kind;
		}
	}
}
