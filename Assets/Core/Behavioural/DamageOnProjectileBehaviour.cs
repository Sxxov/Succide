using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(Collision2D))]
	[RequireComponent(typeof(HealthfulBehaviour))]
	public class DamageOnProjectileBehaviour : MonoBehaviour
	{
		private HealthfulBehaviour healthful = null!;

		void Awake()
		{
			healthful = GetComponent<HealthfulBehaviour>();
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			var projectile =
				collision.gameObject.GetComponent<ProjectileBehaviour>();

			if (projectile)
			{
				healthful.health -= projectile.damage;
			}
		}
	}
}
