using UnityEngine;

namespace Succide.Core.Things.Spawning
{
	[RequireComponent(typeof(RectTransform))]
	public class RandomRectDimensionsBehaviour : MonoBehaviour
	{
		[SerializeField]
		private bool isUniform = true;

		[SerializeField]
		private Vector2 minSize = Vector2.zero;

		[SerializeField]
		private Vector2 maxSize = Vector2.one;

		void Awake()
		{
			var randX = Random.Range(0, 1);
			var randY = isUniform ? randX : Random.Range(0, 1);

			(transform as RectTransform)!.sizeDelta = new Vector2(
				Mathf.Lerp(minSize.x, maxSize.x, randX),
				Mathf.Lerp(minSize.y, maxSize.y, randY)
			);
		}
	}
}
