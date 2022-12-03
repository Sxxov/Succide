using System.Collections.Generic;
using System.Threading.Tasks;
using Succide.Core.Behavioural;
using Succide.Core.Player;
using Succide.Core.Routing;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Succide.Core.Things.Death
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class ScoreController : MonoBehaviour
	{
		private PlayerMeasurableController playerMeasurable = null!;
		private TextMeshProUGUI tmp = null!;

		void Awake()
		{
			playerMeasurable =
				PlayerController.player!.GetComponent<PlayerMeasurableController>();

			var lifetime = decimal.Round(
				(decimal)playerMeasurable.time.value,
				2
			);
			var fulfillingness = decimal.Round(
				(decimal)playerMeasurable.moodGrossGain.value / 10 + 1,
				2
			);

			tmp = GetComponent<TextMeshProUGUI>();
			tmp.text = @$"
whoops! you died.


<color=#FD7272><b>lifetime</b></color>
{lifetime:0.00}secs

<color=#FEA47F><b>fulfillingness</b></color>
{fulfillingness:0.00}x


<b>score</b>
{lifetime * fulfillingness * 3 / 1000:0.00}


well done?
".Trim();
		}

		void Start()
		{
			Time.timeScale = 0;
		}

		void OnDestroy()
		{
			Time.timeScale = 1;
		}
	}
}
