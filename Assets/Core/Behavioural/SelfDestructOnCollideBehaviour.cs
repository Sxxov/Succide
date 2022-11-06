using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class SelfDestructOnCollideBehaviour : MonoBehaviour
	{
		void OnCollisionEnter2D(Collision2D collision)
		{
			GetComponent<SelfDestructBehaviour>().SelfDestruct();
		}
	}
}
