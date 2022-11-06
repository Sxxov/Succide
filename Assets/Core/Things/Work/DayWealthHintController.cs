using System;
using Succide.Core.Behavioural;
using Succide.Core.Player;
using Succide.Core.Things.Common;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Work
{
	[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
	public class DayWealthHintController : MonoBehaviour
	{
		private DayWealthComptroller dayWealthComptroller = null!;
		private TMPro.TextMeshProUGUI text = null!;

		void Awake()
		{
			dayWealthComptroller =
				this.ForceGetComponentInParent<DayWealthComptroller>();

			text = GetComponent<TMPro.TextMeshProUGUI>();

			dayWealthComptroller!.dayWealth.OnChanged += OnDayWealthChanged;
		}

		void OnDestroy()
		{
			dayWealthComptroller!.dayWealth.OnChanged -= OnDayWealthChanged;
		}

		void OnDayWealthChanged(float v)
		{
			text!.text = $"${v.ToString().PadLeft(3, '0')}";
		}
	}
}
