using System;
using Succide.Core.Behavioural;
using Succide.Core.Player;
using Succide.Core.Things.Common;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Work
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	[RequireComponent(typeof(HealthfulBehaviour))]
	public class KeyController : MonoBehaviour
	{
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
			if (DayWealthComptroller.instance)
			{
				DayWealthComptroller.instance!.dayWealth.value += 1;
			}
		}
	}
}
