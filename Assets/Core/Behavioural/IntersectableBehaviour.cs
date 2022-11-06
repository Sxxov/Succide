using System;
using Succide.Core.Common;
using Succide.Core.Player;
using UnityEngine;

namespace Succide.Core.Things
{
	[RequireComponent(typeof(Collider2D))]
	public class IntersectableBehaviour : MonoBehaviour
	{
		public event Action? OnIntersect;
		public event Action? OnOutersect;
		public readonly Store<bool> isIntersecting = new();

		void Awake()
		{
			isIntersecting.OnChanged += OnIsIntersectingChanged;
		}

		void OnDestroy()
		{
			isIntersecting.OnChanged -= OnIsIntersectingChanged;
		}

		private void OnIsIntersectingChanged(bool v)
		{
			if (v)
			{
				OnIntersect?.Invoke();
			}
			else
			{
				OnOutersect?.Invoke();
			}
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.gameObject == PlayerController.player)
			{
				isIntersecting.value = true;
			}
		}

		void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.gameObject == PlayerController.player)
			{
				isIntersecting.value = false;
			}
		}
	}
}
