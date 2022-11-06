using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class SelfDestructOnInvisibleBehaviour : MonoBehaviour
	{
		void OnBecameInvisible()
		{
			GetComponent<SelfDestructBehaviour>().SelfDestruct();
		}
	}
}
