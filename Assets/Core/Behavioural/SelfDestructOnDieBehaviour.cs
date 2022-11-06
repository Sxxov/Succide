using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	[RequireComponent(typeof(HealthfulBehaviour))]
	public class SelfDestructOnDieBehaviour : MonoBehaviour
	{
		private HealthfulBehaviour healthful = null!;

		void Awake()
		{
			healthful = GetComponent<HealthfulBehaviour>();
			healthful!.OnHealthChanged += OnHealthChanged;
		}

		void OnDestroy()
		{
			healthful!.OnHealthChanged -= OnHealthChanged;
		}

		void OnHealthChanged((int damage, int prevHealth, int currHealth) v)
		{
			var (_, _, currHealth) = v;

			if (currHealth <= 0)
			{
				GetComponent<SelfDestructBehaviour>().SelfDestruct();
			}
		}
	}
}
