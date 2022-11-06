using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Succide.Core.Animation;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(SelfDestructBehaviour))]
	[RequireComponent(typeof(Aniwaitor))]
	public class SelfDestructOnAnimationEndBehaviour : MonoBehaviour
	{
		[SerializeField]
		private string? animationName;

		private Aniwaitor anim = null!;

		void Awake()
		{
			anim = GetComponent<Aniwaitor>();
			anim.OnAnimationEnd += OnAnimationEnd;
		}

		void OnDestroy()
		{
			anim.OnAnimationEnd -= OnAnimationEnd;
		}

		void OnAnimationEnd(string name)
		{
			if (name == animationName)
			{
				GetComponent<SelfDestructBehaviour>().SelfDestruct();
			}
		}
	}
}
