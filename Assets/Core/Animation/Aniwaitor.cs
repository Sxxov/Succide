using System;
using System.Threading.Tasks;
using Succide.Core.Common;
using UnityEngine;

namespace Succide.Core.Animation
{
	[RequireComponent(typeof(Animator))]
	public class Aniwaitor : MonoBehaviour
	{
		public event Action<string>? OnAnimationStart;
		public event Action<string>? OnAnimationEnd;

		private Animator? animator;

		void Awake()
		{
			animator = GetComponent<Animator>();

			foreach (
				var clip in animator.runtimeAnimatorController.animationClips
			)
			{
				clip.AddEvent(
					new AnimationEvent
					{
						time = 0,
						functionName = "OnAnimatorAnimationStart",
						stringParameter = clip.name
					}
				);
				clip.AddEvent(
					new AnimationEvent
					{
						time = clip.length,
						functionName = "OnAnimatorAnimationEnd",
						stringParameter = clip.name
					}
				);
			}
		}

		internal void OnAnimatorAnimationStart(string name)
		{
			OnAnimationStart?.Invoke(name);
		}

		internal void OnAnimatorAnimationEnd(string name)
		{
			OnAnimationEnd?.Invoke(name);
		}

		public Task Await(string? animationName = null) =>
			Asyncify.Delegate<string>(
				(r) =>
				{
					OnAnimationEnd += r;
				},
				(r) =>
				{
					OnAnimationEnd -= r;
				},
				(name) => animationName is null || name == animationName
			);

		public Task Play(string animationName)
		{
			animator!.Play(animationName);

			return Await(animationName);
		}
	}
}
