using Succide.Core.Exceptions;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Interacting
{
	[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
	public class InteractableHintController : MonoBehaviour
	{
		public bool isActiveStateInverted = false;

		private InteractableComptroller interactableComptroller = null!;
		private TMPro.TextMeshProUGUI text = null!;

		void Awake()
		{
			interactableComptroller =
				this.ForceGetComponentInParent<InteractableComptroller>();
			interactableComptroller.isInteracting.OnChanged +=
				OnIsInteractingChanged;

			text = GetComponent<TMPro.TextMeshProUGUI>();
		}

		private void OnIsInteractingChanged(bool v)
		{
			if (v)
			{
				text!.fontStyle = TMPro.FontStyles.Bold;
			}
			else
			{
				text!.fontStyle = TMPro.FontStyles.Normal;
			}
		}
	}
}
