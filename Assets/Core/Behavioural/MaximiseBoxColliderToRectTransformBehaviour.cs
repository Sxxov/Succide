using UnityEngine;

namespace Succide.Core.Behavioural
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(BoxCollider2D))]
	public class MaximiseBoxColliderToRectTransformBehaviour : MonoBehaviour
	{
		[SerializeField]
		private Vector2 offset = Vector2.zero;
		private BoxCollider2D boxCollider = null!;

		void Awake()
		{
			boxCollider = GetComponent<BoxCollider2D>();
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
				boxCollider.size = sizeDelta + offset;
			}
			else if (rect.width != 0 && rect.height != 0)
			{
				boxCollider.size =
					new Vector2(rect.width, rect.height) + offset;
			}
		}
	}
}
