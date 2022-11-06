using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	public class HealthfulBehaviour : MonoBehaviour
	{
		public event Action<(int curr, int prev, int diff)>? OnHealthChanged;

		[SerializeField]
		private int health_ = 1;
		public int health
		{
			get => health_;
			set
			{
				var prev = health_;
				health_ = value;

				OnHealthChanged?.Invoke((value, prev, value - prev));
			}
		}
	}
}
