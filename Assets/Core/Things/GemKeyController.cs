using UnityEngine;
using Succide.Core.Behavioural;

namespace Succide.Core.Things
{
	[RequireComponent(typeof(CollectableBehaviour))]
	[RequireComponent(typeof(Collider2D))]
	public class GemKeyController : MonoBehaviour
	{
		private CollectableBehaviour collectableBehaviour = null!;

		void Start()
		{
			collectableBehaviour = GetComponent<CollectableBehaviour>()!;
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			if (
				collider.gameObject.CompareTag("Player")
				&& !collectableBehaviour!.hasBeenCollected
			)
			{
				collectableBehaviour.Collect();
				Destroy(gameObject);
				// gameObject.SetActive(false);
			}
		}
	}
}
