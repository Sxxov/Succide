using Succide.Core.Behavioural;
using UnityEngine;

namespace Succide.Core.Things.Interacting
{
	[RequireComponent(typeof(InteractableComptroller))]
	[RequireComponent(typeof(HealthfulBehaviour))]
	public class DamageOnInteractBehaviour : MonoBehaviour
	{
		private HealthfulBehaviour healthful = null!;
		private InteractableComptroller interactable = null!;

		void Awake()
		{
			healthful = GetComponent<HealthfulBehaviour>();

			interactable = GetComponent<InteractableComptroller>();
			interactable!.OnInteract += OnInteract;
		}

		void OnDestroy()
		{
			interactable.OnInteract -= OnInteract;
		}

		private void OnInteract()
		{
			healthful!.health -= 1;
		}
	}
}
