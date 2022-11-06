using System;
using System.Collections.Generic;
using System.Linq;
using Succide.Core.Common;
using Succide.Core.Routing;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Spawning
{
	public class SpawnerComptroller : MonoBehaviour
	{
		[SerializeField]
		private float spawnsPerSecond = 1f;

		[SerializeField]
		private GameObject? prefab;

		[SerializeField]
		private int seed = Guid.NewGuid().GetHashCode();

		private readonly List<GameObject> anchors = new();
		private Ticker ticker = null!;
		private System.Random random = null!;

		void Awake()
		{
			random = new(seed);

			ticker = new(spawnsPerSecond);
			ticker.OnTick += OnTick;

			anchors.AddRange(
				GetComponentsInChildren<SpawnerAnchorBehaviour>()
					.Select(x => x.gameObject)
			);

			StartCoroutine(ticker);
		}

		void OnDestroy()
		{
			ticker.OnTick -= OnTick;
		}

		private void OnTick()
		{
			var anchor = PickRandomAnchor();

			if (!anchor)
			{
				Debug.LogWarning(
					$"Failed to find suitable anchor for {gameObject.name}, giving up for this tick"
				);

				return;
			}

			Instantiate(prefab, anchor!.transform, false);
		}

		private GameObject? PickRandomAnchor()
		{
			GameObject? anchor;
			var tryCount = 0;

			do
			{
				anchor = anchors[
					(int)(
						Mathf.InverseLerp(0, int.MaxValue, random.Next())
						* anchors.Count
					)
				];
				++tryCount;

				if (tryCount >= anchors.Count)
				{
					return null;
				}
			} while (anchor.transform.childCount > 0);

			return anchor;
		}
	}
}
