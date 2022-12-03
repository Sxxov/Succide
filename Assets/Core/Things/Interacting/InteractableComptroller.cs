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
		private bool canInteract;

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
			canInteract = !fireController.isFiring.value;
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
			if (!canInteract)
			{
				return;
			}

			if (intersectableBehaviour.isIntersecting.value)
			{
				isInteracting.value = true;
			}
			else
			{
				canInteract = false;
				isInteracting.value = false;
			}
		}

		private void OnCease()
		{
			canInteract = true;
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
