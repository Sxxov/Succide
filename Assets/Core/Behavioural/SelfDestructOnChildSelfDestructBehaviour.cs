using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class SelfDestructOnChildSelfDestructBehaviour : MonoBehaviour
	{
		private SelfDestructBehaviour selfDestruct = null!;

		void Awake()
		{
			selfDestruct = GetComponent<SelfDestructBehaviour>();

			foreach (
				var childSelfDestruct in GetComponentsInChildren<SelfDestructBehaviour>()
			)
			{
				if (childSelfDestruct == selfDestruct)
				{
					continue;
				}

				childSelfDestruct.OnSelfDestruct += OnChildSelfDestruct;
			}
		}

		void OnDestroy()
		{
			foreach (
				var childSelfDestruct in GetComponentsInChildren<SelfDestructBehaviour>()
			)
			{
				if (childSelfDestruct == selfDestruct)
				{
					continue;
				}

				childSelfDestruct.OnSelfDestruct -= OnChildSelfDestruct;
			}
		}

		void OnChildSelfDestruct()
		{
			selfDestruct.SelfDestruct();
		}
	}
}
