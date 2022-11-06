using System;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	public class SelfDestructBehaviour : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("In seconds")]
		private float delay = 0;
		public event Action? OnSelfDestruct;

		public void SelfDestruct()
		{
			OnSelfDestruct?.Invoke();
			Destroy(gameObject, delay);
		}
	}
}
