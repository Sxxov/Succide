using Succide.Core.Common;
using Succide.Core.Exceptions;
using Succide.Core.Player;
using Succide.Core.Player.Measuring;
using UnityEngine;

namespace Succide.Core.Ui
{
	public class MeasurablePercentTextController : MonoBehaviour
	{
		private TMPro.TextMeshProUGUI? text;

		void Start()
		{
			text = GetComponent<TMPro.TextMeshProUGUI>();

			this.ForceGetComponentInParent<MeasurableBehaviour>().progress!.OnChanged +=
				(v) => text!.text = $"{Mathf.Ceil(v * 100)}%";
		}
	}
}
