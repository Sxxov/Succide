using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using Succide.Core.Common;
using UnityEngine;
using Succide.Core.Player.Measuring;

namespace Succide.Core.Things.Branch
{
	[RequireComponent(typeof(Animator))]
	public class TimeSynchedAnimatorController : MonoBehaviour
	{
		[SerializeField]
		private float framesPerSecond = 60f;

		[SerializeField]
		private int animationClipIndex = 0;

		private Animator animator = null!;
		private PlayerMeasurable time = null!;

		void Awake()
		{
			animator = GetComponent<Animator>();
			animator.speed = 0;
			animator.Play(0, -1, 0);

			time = PlayerMeasurableProvider.Get(PlayerMeasurableKinds.Time);
			time.OnChanged += OnChanged;
			OnChanged(time.value);
		}

		void OnDestroy()
		{
			time.OnChanged -= OnChanged;
		}

		private void OnChanged(float v)
		{
			var clip = animator.runtimeAnimatorController.animationClips[
				animationClipIndex
			];
			var frameCount = clip.length * clip.frameRate;

			animator.Play(0, -1, v / (frameCount / framesPerSecond) % 1);
		}
	}
}
