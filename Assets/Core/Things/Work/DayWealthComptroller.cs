using System;
using Succide.Core.Behavioural;
using Succide.Core.Player;
using Succide.Core.Player.Measuring;
using Succide.Core.Things.Common;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Work
{
	public class DayWealthComptroller : SingletonBehaviour<DayWealthComptroller>
	{
		public Measurable dayWealth = new(0, 0, 100);

		void OnDestroy()
		{
			PlayerController.player!
				.GetComponent<PlayerMeasurableController>()!
				.wealth.value += dayWealth.value;
		}
	}
}
