using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Succide.Core.Animation;
using Succide.Core.Behavioural;

namespace Succide.Core.Things
{
	[RequireComponent(typeof(Aniwaitor))]
	[RequireComponent(typeof(Collider2D))]
	public class DoorController : MonoBehaviour
	{
		[SerializeField]
		private GameObject? gemKey;
		private CollectableBehaviour? gemKeyCollectableBehaviour;

		private Aniwaitor anim = null!;

		void Awake()
		{
			anim = GetComponent<Aniwaitor>()!;

			if (gemKey is not null)
			{
				gemKeyCollectableBehaviour =
					gemKey.GetComponent<CollectableBehaviour>();
			}
		}

		void Update() { }

		async void OnCollisionEnter2D(Collision2D collision)
		{
			if (
				collision.gameObject.CompareTag("Player")
				&& (
					gemKeyCollectableBehaviour is null
					|| gemKeyCollectableBehaviour.hasBeenCollected
				)
			)
			{
				await anim!.Play("Door, Open");

				Destroy(gameObject);

				VirtualCameraManager.instance!.activeCamera = 1;
			}
		}
	}
}
