using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class SpawnOnSelfDestructBehaviour : MonoBehaviour
	{
		[SerializeField]
		private GameObject? gameObjectToSpawn;
		private SelfDestructBehaviour selfDestruct = null!;

		void Awake()
		{
			selfDestruct = GetComponent<SelfDestructBehaviour>();
			selfDestruct.OnSelfDestruct += OnSelfDestruct;
		}

		void OnDestroy()
		{
			selfDestruct.OnSelfDestruct -= OnSelfDestruct;
		}

		void OnSelfDestruct()
		{
			Instantiate(
				gameObjectToSpawn,
				transform.position,
				Quaternion.identity
			);
		}
	}
}
