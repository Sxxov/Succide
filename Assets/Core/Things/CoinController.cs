using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Succide.Core.Player;
using Succide.Core.Animation;
using Succide.Core.Behavioural;

namespace Succide.Core.Things
{
	[RequireComponent(typeof(Aniwaitor))]
	[RequireComponent(typeof(CollectableBehaviour))]
	public class CoinController : MonoBehaviour
	{
		[SerializeField]
		private GameObject? explosion;
		private Aniwaitor anim = null!;
		private CollectableBehaviour collectableBehaviour = null!;

		void Start()
		{
			anim = GetComponent<Aniwaitor>()!;
			collectableBehaviour = GetComponent<CollectableBehaviour>()!;
		}

		void Update() { }

		async void OnTriggerEnter2D(Collider2D collision)
		{
			if (
				collision.gameObject.CompareTag("Player")
				&& !collectableBehaviour!.hasBeenCollected
			)
			{
				collectableBehaviour.Collect();

				// var coinMeasurable =
				// 	collision.gameObject.GetComponent<CoinMeasurable>();
				// if (coinMeasurable is not null)
				// {
				// 	coinMeasurable.measurable.value += 1;
				// }

				Instantiate(explosion, transform.position, Quaternion.identity);
				await anim!.Play("Coin, Collect");
				Destroy(gameObject);
			}
		}
	}
}
