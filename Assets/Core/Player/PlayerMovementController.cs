using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

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

		private PlayerInput input = null!;
		private Rigidbody2D rigidBody = null!;
		private readonly List<GameObject> contactingGroundObjects = new();
		private readonly List<GameObject> contactingWallObjects = new();
		private bool isDoubleJumpable = false;
		private InputAction.CallbackContext? inputCtx;

		public bool isOnGround
		{
			get => contactingGroundObjects.Count > 0;
		}

		public bool isOnWall
		{
			get => contactingWallObjects.Count > 0;
		}

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

			if (isOnGround)
			{
				isDoubleJumpable = true;
			}

			if (canJump && !hasJumped)
			{
				if (canJump)
				{
					if (isOnGround)
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
					isOnGround
					&& rigidBody.velocity.x > 0
					&& horizontalAxis != Mathf.Sign(rigidBody.velocity.x)
						? // make switching direction when walking immediate
						-rigidBody.velocity.x
						: // otherwise, move according to physics
						horizontalAxis
							* (1 + Time.deltaTime - 1 / 60)
							* (
								isOnWall
									? walkAmplitudeWall
									: isOnGround
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
					+ (isOnGround ? jumpAmplitudeGround : jumpAmplitudeAir),
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
				if (!isOnGround && canJump && hasJumped)
				{
					hasJumped = false;
				}

				contactingGroundObjects.Add(collision.gameObject);
			}
			else
			{
				contactingWallObjects.Add(collision.gameObject);
			}
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			contactingGroundObjects.Remove(collision.gameObject);
			contactingWallObjects.Remove(collision.gameObject);
		}

		private void OnInputStarted(InputAction.CallbackContext ctx) =>
			inputCtx = ctx;

		private void OnInputCanceled(InputAction.CallbackContext ctx) =>
			inputCtx = null;

		private static float ZeroableSign(float value) =>
			value == 0 ? 0 : Mathf.Sign(value);
	}
}
