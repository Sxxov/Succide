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
		private const float firesPerSecond = 10;
		private const float fireMinCooldown = 1 / firesPerSecond;

		public event Action? OnFire;
		public event Action? OnFiring;
		public event Action? OnCease;

		[HideInInspector]
		public Store<bool> isFiring = new();

		private PlayerInput input = null!;
		private Ticker? ticker;
		private float prevTickerStartTime = float.MinValue;

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

				var currTime = Time.time;

				if (currTime - prevTickerStartTime >= fireMinCooldown)
				{
					prevTickerStartTime = currTime;
					OnFire?.Invoke();

					if (ticker is null)
					{
						ticker = new(firesPerSecond);
						ticker.OnTick += OnFire;
						StartCoroutine(ticker);
					}
				}
			}
			else
			{
				OnCease?.Invoke();

				if (ticker is not null)
				{
					ticker.OnTick -= OnFire;
					StopCoroutine(ticker);
					ticker = null;
				}
			}
		}

		public void FireOnce()
		{
			var wasFiring = isFiring.value;

			if (!wasFiring)
			{
				isFiring.value = true;
			}

			OnFire?.Invoke();

			if (!wasFiring)
			{
				isFiring.value = false;
			}
		}

		public void FireContinuously()
		{
			isFiring.value = true;
		}

		public void Cease()
		{
			isFiring.value = false;
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
			FireContinuously();
		}

		private void OnFireInputCanceled(InputAction.CallbackContext ctx)
		{
			Cease();
		}
	}
}
