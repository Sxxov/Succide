using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(RectTransform))]
	public class MaximiseRectTransformToParentBehaviour : MonoBehaviour
	{
		private RectTransform parentRectTransform = null!;

		void Awake()
		{
			parentRectTransform =
				this.ForceGetComponentInParent<RectTransform>();
		}

		void Update()
		{
			(transform as RectTransform)!.sizeDelta =
				parentRectTransform.sizeDelta;
		}
	}
}
