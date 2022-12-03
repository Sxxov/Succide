using System.Collections.Generic;
using Succide.Core.Behavioural;
using Succide.Core.Player;
using UnityEngine;

namespace Succide.Core.Things.Tutorial
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class TutorialController : MonoBehaviour
	{
		private static readonly List<int> dismissedSceneIndices = new();

		private PlayerFireController playerFire = null!;
		private SelfDestructBehaviour selfDestruct = null!;

		void Awake()
		{
			var hasBeenDismissed = dismissedSceneIndices.Contains(
				gameObject.scene.buildIndex
			);

			gameObject.SetActive(!hasBeenDismissed);

			playerFire =
				PlayerController.player!.GetComponent<PlayerFireController>();
			playerFire.isFiring.OnChanged += OnIsFiringChanged;

			selfDestruct = GetComponent<SelfDestructBehaviour>();
			selfDestruct.OnSelfDestruct += OnSelfDestruct;

			if (hasBeenDismissed)
			{
				Destroy(gameObject);
			}
		}

		void Start()
		{
			Time.timeScale = 0;
		}

		void OnDestroy()
		{
			playerFire.isFiring.OnChanged -= OnIsFiringChanged;
			selfDestruct.OnSelfDestruct -= OnSelfDestruct;
		}

		private void OnSelfDestruct()
		{
			dismissedSceneIndices.Add(gameObject.scene.buildIndex);
		}

		private void OnIsFiringChanged(bool v)
		{
			if (v)
			{
				Time.timeScale = 1;
				selfDestruct.SelfDestruct();
			}
		}
	}
}
