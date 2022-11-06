using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CircleCollider2D))]
	public class MaximiseCircleColliderToRectTransformBehaviour : MonoBehaviour
	{
		[SerializeField]
		private float offset;
		private CircleCollider2D circleCollider = null!;

		void Awake()
		{
			circleCollider = GetComponent<CircleCollider2D>();
		}

		void Update()
		{
			var rectTransform = (transform as RectTransform)!;
			var (sizeDelta, rect) = (
				rectTransform.sizeDelta,
				rectTransform.rect
			);

			if (sizeDelta.x != 0 && sizeDelta.y != 0)
			{
				circleCollider.radius =
					Mathf.Max(sizeDelta.x, sizeDelta.y) / 2 + offset;
			}
			else if (rect.width != 0 && rect.height != 0)
			{
				circleCollider.radius =
					Mathf.Max(rect.width, rect.height) / 2 + offset;
			}
		}
	}
}
