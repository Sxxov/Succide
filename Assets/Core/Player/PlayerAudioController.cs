using UnityEngine;

namespace Succide.Core.Player
{
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(PlayerMovementController))]
	[RequireComponent(typeof(PlayerFireController))]
	public class PlayerAudioController : MonoBehaviour
	{
		[SerializeField]
		private AudioClip[]? impactClips;

		[SerializeField]
		private AudioClip[]? jumpClips;

		[SerializeField]
		private AudioClip[]? walkClips;

		[SerializeField]
		private AudioClip[]? clickClips;

		[SerializeField]
		private AudioClip[]? touchClips;

		private AudioSource audioSource = null!;
		private PlayerMovementController movementController = null!;
		private PlayerFireController fireController = null!;

		private float horizontalAxis => movementController!.horizontalAxis;
		private float verticalAxis => movementController!.verticalAxis;

		private float timeSincePrevWalk = 0;
		private bool hasDashed = false;

		void Awake()
		{
			audioSource = GetComponent<AudioSource>()!;
			movementController = GetComponent<PlayerMovementController>()!;
			fireController = GetComponent<PlayerFireController>()!;

			movementController.isOnGround.OnChanged += OnIsOnGroundChanged;
			movementController.isOnWall.OnChanged += OnIsOnWallChanged;
			movementController.OnJump += OnJump;
			fireController.isFiring.OnChanged += OnIsFiringChanged;
		}

		void OnDestroy()
		{
			movementController.isOnGround.OnChanged -= OnIsOnGroundChanged;
			movementController.isOnWall.OnChanged -= OnIsOnWallChanged;
			movementController.OnJump -= OnJump;
			fireController.isFiring.OnChanged -= OnIsFiringChanged;
		}

		void Update()
		{
			if (horizontalAxis != 0 && movementController.isOnGround.value)
			{
				if (timeSincePrevWalk > 0.2f)
				{
					audioSource.PlayOneShot(
						walkClips![Random.Range(0, walkClips!.Length)]
					);
					timeSincePrevWalk = 0;
				}
				else
				{
					timeSincePrevWalk += Time.deltaTime;
				}
			}

			if (verticalAxis < 0)
			{
				hasDashed = true;
			}
		}

		private void OnIsOnGroundChanged(bool v)
		{
			if (v)
			{
				if (hasDashed)
				{
					// landing
					audioSource.PlayOneShot(
						impactClips![Random.Range(0, impactClips!.Length)]
					);
					hasDashed = false;
				}
				else
				{
					audioSource.PlayOneShot(
						touchClips![Random.Range(0, touchClips!.Length)]
					);
				}
			}
		}

		private void OnIsOnWallChanged(bool v)
		{
			if (v)
			{
				audioSource.PlayOneShot(
					touchClips![Random.Range(0, touchClips!.Length)]
				);
			}
		}

		private void OnJump()
		{
			audioSource.PlayOneShot(
				jumpClips![Random.Range(0, jumpClips!.Length)]
			);
			hasDashed = false;
		}

		private void OnIsFiringChanged(bool v)
		{
			if (v)
			{
				// firing
				audioSource.PlayOneShot(
					clickClips![Random.Range(0, clickClips!.Length)]
				);
			}
		}
	}
}
