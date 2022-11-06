using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(HealthfulBehaviour))]
	public class DamageableController : MonoBehaviour
	{
		private Animator anim = null!;
		private HealthfulBehaviour healthful = null!;

		void Awake()
		{
			anim = GetComponent<Animator>();
			healthful = GetComponent<HealthfulBehaviour>();
			healthful.OnHealthChanged += OnHealthChanged;
		}

		void OnDestroy()
		{
			healthful.OnHealthChanged -= OnHealthChanged;
		}

		void OnHealthChanged((int curr, int prev, int diff) v)
		{
			if (v.diff < 0 && gameObject.activeInHierarchy)
			{
				anim!.CrossFade("Damage", 0.1f);
			}
		}
	}
}
