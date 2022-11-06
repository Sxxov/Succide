using System;
using Succide.Core.Player;
using UnityEngine;

namespace Succide.Core.Behavioural
{
	public class CollectableBehaviour : MonoBehaviour
	{
		public event Action? OnCollected;
		public bool hasBeenCollected { get; private set; }

		public void Collect()
		{
			hasBeenCollected = true;
			OnCollected?.Invoke();
		}
	}
}
