using System;
using Succide.Core.Common;
using Succide.Core.Player;
using UnityEngine;

namespace Succide.Core.Things.Interacting
{
	[RequireComponent(typeof(IntersectableBehaviour))]
	public class InteractableComptroller : MonoBehaviour
	{
		public event Action? OnInteract;
		public event Action? OnOuteract;
		public readonly Store<bool> isInteracting = new();
		private IntersectableBehaviour intersectableBehaviour = null!;
		private PlayerFireController fireController = null!;

		void Awake()
		{
			DeactivateHintChildren();

			isInteracting.OnChanged += OnIsInteractingChanged;

			intersectableBehaviour = GetComponent<IntersectableBehaviour>()!;
			intersectableBehaviour.OnIntersect += OnIntersect;
			intersectableBehaviour.OnOutersect += OnOutersect;

			fireController =
				PlayerController.player!.GetComponent<PlayerFireController>()!;
			fireController.OnFiring += OnFiring;
			fireController.OnCease += OnCease;
		}

		void OnDestroy()
		{
			isInteracting.OnChanged -= OnIsInteractingChanged;

			intersectableBehaviour.OnIntersect -= OnIntersect;
			intersectableBehaviour.OnOutersect -= OnOutersect;

			fireController.OnFiring -= OnFiring;
			fireController.OnCease -= OnCease;
		}

		private void OnIsInteractingChanged(bool v)
		{
			if (v)
			{
				OnInteract?.Invoke();
			}
			else
			{
				OnOuteract?.Invoke();
			}
		}

		private void OnIntersect() => ActivateHintChildren();

		private void OnOutersect() => DeactivateHintChildren();

		private void OnFiring()
		{
			// explicitly disable interaction when game is paused
			if (Time.timeScale == 0)
			{
				return;
			}

			isInteracting.value = intersectableBehaviour.isIntersecting.value;
		}

		private void OnCease()
		{
			isInteracting.value = false;
		}

		private void ActivateHintChildren()
		{
			foreach (
				var hint in transform.GetComponentsInChildren<InteractableHintController>(
					true
				)
			)
			{
				hint.gameObject.SetActive(!hint.isActiveStateInverted);
			}
		}

		private void DeactivateHintChildren()
		{
			foreach (
				var hint in transform.GetComponentsInChildren<InteractableHintController>(
					true
				)
			)
			{
				hint.gameObject.SetActive(hint.isActiveStateInverted);
			}
		}
	}
}
