using UnityEngine;

namespace Succide.Core.Ui
{
	public class MeasurableTimeTextController : MonoBehaviour
	{
		private TMPro.TextMeshProUGUI? text;

		void Start()
		{
			text = GetComponent<TMPro.TextMeshProUGUI>();

			this.ForceGetComponentInParent<MeasurableBehaviour>().measurable!.OnChanged +=
				(v) =>
					text!.text =
						$"{Mathf.Round(v / 60).ToString().PadLeft(2, '0')}:{Mathf.Round(v % 60).ToString().PadLeft(2, '0')}";
		}
	}
}
