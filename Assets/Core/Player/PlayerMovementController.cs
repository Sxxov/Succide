using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Succide.Core.Common;

namespace Succide.Core.Player
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class PlayerMovementController : MonoBehaviour
	{
		private const float walkAmplitudeMax = 7f;
		private const float walkAmplitudeWall = 1f;
		private const float walkAmplitudeAir = 2f;
		private const float walkAmplitudeGround = 2f;
		private const float jumpAmplitudeMax = 12f;
		private const float jumpAmplitudeAir = 12f;
		private const float jumpAmplitudeGround = 12f;
		private const float crouchAmplitude = 2f;

		public event Action? OnJump;
		public event Action? OnLand;

		private PlayerInput input = null!;
		private Rigidbody2D rigidBody = null!;
		private readonly List<GameObject> contactingGroundObjects = new();
		private readonly List<GameObject> contactingWallObjects = new();
		private bool isDoubleJumpable = false;
		private InputAction.CallbackContext? inputCtx;

		public Store<bool> isOnGround = new(true);

		public Store<bool> isOnWall = new(false);
		public Store<bool> isJumping = new(false);

		public float horizontalAxis =>
			ZeroableSign(inputCtx?.ReadValue<Vector2>().x ?? 0);

		public float verticalAxis =>
			ZeroableSign(inputCtx?.ReadValue<Vector2>().y ?? 0);

		private bool canJump = false;
		private bool hasJumped = false;

		void Awake()
		{
			rigidBody = GetComponent<Rigidbody2D>();
			input = GetComponent<PlayerInput>();
			input.actions["Move"].started += OnInputStarted;
			input.actions["Move"].canceled += OnInputCanceled;
		}

		void OnDestroy()
		{
			input.actions["Move"].started -= OnInputStarted;
			input.actions["Move"].canceled -= OnInputCanceled;
		}

		void FixedUpdate()
		{
			if (horizontalAxis != 0)
			{
				Walk();
			}

			if (verticalAxis > 0)
			{
				canJump = true;
			}
			else
			{
				canJump = false;
				hasJumped = false;
			}

			if (isOnGround.value)
			{
				isDoubleJumpable = true;
			}

			if (canJump && !hasJumped)
			{
				if (canJump)
				{
					if (isOnGround.value)
					{
						Jump();
					}
					else if (isDoubleJumpable)
					{
						Jump();

						isDoubleJumpable = false;
					}

					hasJumped = true;
				}
			}

			if (verticalAxis < 0)
			{
				Crouch();
			}
		}

		public void Reset()
		{
			rigidBody.velocity = Vector2.zero;
			contactingGroundObjects.Clear();
			contactingWallObjects.Clear();
			canJump = false;
			hasJumped = false;
			isDoubleJumpable = false;
			isOnGround.value = true;
			isOnWall.value = false;
			isJumping.value = false;
		}

		private void Walk()
		{
			transform.localScale = new Vector3(
				-horizontalAxis * Mathf.Abs(transform.localScale.x),
				transform.localScale.y,
				transform.localScale.z
			);

			rigidBody.velocity = new Vector2(
				Mathf.Clamp(
					isOnGround.value
					&& rigidBody.velocity.x > 0
					&& horizontalAxis != Mathf.Sign(rigidBody.velocity.x)
						? // make switching direction when walking immediate
						-rigidBody.velocity.x
						: // otherwise, move according to physics
						horizontalAxis
							* (1 + Time.deltaTime - 1 / 60)
							* (
								isOnWall.value
									? walkAmplitudeWall
									: isOnGround.value
										? walkAmplitudeGround
										: walkAmplitudeAir
							)
							+ rigidBody.velocity.x,
					-walkAmplitudeMax,
					walkAmplitudeMax
				),
				rigidBody.velocity.y
			);
		}

		private void Jump()
		{
			var clamped = Mathf.Clamp(
				rigidBody.velocity.y
					+ (
						isOnGround.value
							? jumpAmplitudeGround
							: jumpAmplitudeAir
					),
				-jumpAmplitudeMax,
				jumpAmplitudeMax
			);

			rigidBody!.velocity = new Vector2(
				rigidBody.velocity.x,
				clamped == 0
					? 0
					: clamped > 0
						? Math.Max(clamped, rigidBody.velocity.y)
						: Math.Min(clamped, rigidBody.velocity.y)
			);

			isJumping.value = true;
			OnJump?.Invoke();
		}

		private void Crouch()
		{
			rigidBody!.velocity -= new Vector2(0, crouchAmplitude);
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			if (
				Array.Exists(
					collision.contacts,
					(contact) => contact.normal.y > 0
				)
			)
			{
				if (!isOnGround.value && canJump && hasJumped)
				{
					hasJumped = false;
				}

				contactingGroundObjects.Add(collision.gameObject);
				isOnGround.value = true;

				if (isJumping.value)
				{
					isJumping.value = false;
					OnLand?.Invoke();
				}
			}
			else
			{
				contactingWallObjects.Add(collision.gameObject);
				isOnWall.value = true;
			}
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			contactingGroundObjects.Remove(collision.gameObject);

			if (contactingGroundObjects.Count <= 0)
			{
				isOnGround.value = false;
			}

			contactingWallObjects.Remove(collision.gameObject);

			if (contactingWallObjects.Count <= 0)
			{
				isOnWall.value = false;
			}
		}

		private void OnInputStarted(InputAction.CallbackContext ctx) =>
			inputCtx = ctx;

		private void OnInputCanceled(InputAction.CallbackContext ctx) =>
			inputCtx = null;

		private static float ZeroableSign(float value) =>
			value == 0 ? 0 : Mathf.Sign(value);
	}
}
