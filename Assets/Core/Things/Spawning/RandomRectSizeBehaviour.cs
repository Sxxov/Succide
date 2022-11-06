using UnityEngine;

namespace Succide.Core.Things.Spawning
{
	[RequireComponent(typeof(RectTransform))]
	public class RandomRectSizeBehaviour : MonoBehaviour
	{
		[SerializeField]
		private bool isUniform = true;

		[SerializeField]
		private float minSize = 0;

		[SerializeField]
		private float maxSize = 1;

		void Awake()
		{
			var sizeX = Random.Range(minSize, maxSize);
			var sizeY = isUniform ? sizeX : Random.Range(minSize, maxSize);

			(transform as RectTransform)!.sizeDelta = new Vector2(sizeX, sizeY);
		}
	}
}
