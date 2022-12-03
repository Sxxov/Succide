using UnityEngine;

namespace Succide.Core.Things.Common
{
	public class BopController : MonoBehaviour
	{
		[SerializeField]
		private Vector2 frequency = new(0f, 0f);

		[SerializeField]
		private Vector2 amplitude = new(0f, 0f);

		private float sinIndexX;
		private float sinIndexY;
		private Vector3 initialPosition;

		void Start()
		{
			initialPosition = transform.position;
		}

		void Update()
		{
			sinIndexX += Time.deltaTime * frequency.x;
			sinIndexX %= Mathf.PI * 2;
			sinIndexY += Time.deltaTime * frequency.y;
			sinIndexY %= Mathf.PI * 2;

			transform.position = new(
				initialPosition.x + Mathf.Sin(sinIndexX) * amplitude.x,
				initialPosition.y + Mathf.Sin(sinIndexY) * amplitude.y,
				transform.position.z
			);
		}
	}
}
