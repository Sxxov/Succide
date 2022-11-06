using System;
using System.Threading.Tasks;
using Succide.Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Succide.Core.Player
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerFireController : MonoBehaviour
	{
		private const float fireCooldownTime = .1f;

		public event Action? OnFire;
		public event Action? OnFiring;
		public event Action? OnCease;

		[HideInInspector]
		public Store<bool> isFiring = new();

		private PlayerInput input = null!;
		private float prevFireTime = 0;

		void Awake()
		{
			input = GetComponent<PlayerInput>();
			input.actions["Fire"].started += OnFireInputStarted;
			input.actions["Fire"].canceled += OnFireInputCanceled;

			isFiring.OnChanged += OnIsFiringChanged;
		}

		void OnDestroy()
		{
			input.actions["Fire"].started -= OnFireInputStarted;
			input.actions["Fire"].canceled -= OnFireInputCanceled;

			isFiring.OnChanged -= OnIsFiringChanged;
		}

		private void OnIsFiringChanged(bool v)
		{
			if (v)
			{
				OnFiring?.Invoke();
			}
			else
			{
				OnCease?.Invoke();
			}
		}

		void Update()
		{
			if (isFiring.value && Time.time - prevFireTime >= fireCooldownTime)
			{
				prevFireTime = Time.time;
				OnFire?.Invoke();
			}
		}

		public Task AwaitCease() =>
			isFiring.value
				? Task.CompletedTask
				: Asyncify.Delegate<bool>(
					(r) => isFiring.OnChanged += r,
					(r) => isFiring.OnChanged -= r,
					(v) => !v
				);

		private void OnFireInputStarted(InputAction.CallbackContext ctx)
		{
			isFiring.value = true;
		}

		private void OnFireInputCanceled(InputAction.CallbackContext ctx)
		{
			isFiring.value = false;
		}
	}
}
