using UnityEngine;

namespace Succide.Core.Behavioural
{
	public class SelfDestructOnStartBehaviour : MonoBehaviour
	{
		void Start()
		{
			GetComponent<SelfDestructBehaviour>().SelfDestruct();
		}
	}
}
