using System;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class SelfDestructOnTimeoutBehaviour : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("In seconds")]
		private float timeout;

		[SerializeField]
		[Tooltip(
			"Instead of calling SelfDestruct, call Destroy on timeout to bypass any events that SelfDestruct would trigger"
		)]
		private bool shouldDestroyInsteadOfSelfDestruct;
		private long startTime = long.MinValue;

		void Update()
		{
			var currTime =
				DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond / 1000;

			if (startTime == long.MinValue)
			{
				startTime = currTime;
			}

			if (currTime - startTime >= timeout)
			{
				if (shouldDestroyInsteadOfSelfDestruct)
				{
					Destroy(gameObject);
				}
				else
				{
					GetComponent<SelfDestructBehaviour>().SelfDestruct();
				}
			}
		}
	}
}
