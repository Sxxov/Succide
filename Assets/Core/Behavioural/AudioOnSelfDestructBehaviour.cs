using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class AudioOnSelfDestructBehaviour : MonoBehaviour
	{
		[SerializeField]
		private AudioClip? clip;

		[SerializeField]
		private bool shouldPlayAtWorldZero;

		private SelfDestructBehaviour selfDestructBehaviour = null!;

		void Awake()
		{
			selfDestructBehaviour = GetComponent<SelfDestructBehaviour>();
			selfDestructBehaviour.OnSelfDestruct += OnSelfDestruct;
		}

		private void OnSelfDestruct()
		{
			AudioSource.PlayClipAtPoint(
				clip!,
				shouldPlayAtWorldZero ? Vector3.zero : transform.position
			);
		}
	}
}
