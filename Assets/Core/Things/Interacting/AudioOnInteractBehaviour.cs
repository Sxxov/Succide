using UnityEngine;

namespace Succide.Core.Things.Interacting
{
	[RequireComponent(typeof(InteractableComptroller))]
	public class AudioOnInteractBehaviour : MonoBehaviour
	{
		[SerializeField]
		private AudioClip? clip;

		[SerializeField]
		private bool shouldPlayAtWorldZero;

		private InteractableComptroller interactable = null!;

		void Awake()
		{
			interactable = GetComponent<InteractableComptroller>();
			interactable!.OnInteract += OnInteract;
		}

		void OnDestroy()
		{
			interactable.OnInteract -= OnInteract;
		}

		private void OnInteract()
		{
			AudioSource.PlayClipAtPoint(
				clip!,
				shouldPlayAtWorldZero ? Vector3.zero : transform.position
			);
		}
	}
}
