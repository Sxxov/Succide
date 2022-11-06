using System;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	public class SelfDestructOnRandomTimeoutBehaviour : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("In seconds")]
		private float minTimeout;

		[SerializeField]
		[Tooltip("In seconds")]
		private float maxTimeout;

		[SerializeField]
		[Tooltip(
			"Instead of calling SelfDestruct, call Destroy on timeout to bypass any events that SelfDestruct would trigger"
		)]
		private bool shouldDestroyInsteadOfSelfDestruct;
		private long startTime = long.MinValue;
		private float timeout;

		void Awake()
		{
			timeout = UnityEngine.Random.Range(minTimeout, maxTimeout);
		}

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
