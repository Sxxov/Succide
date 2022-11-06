using UnityEngine;

namespace Succide.Core.Things.Common
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class ElasticityController : MonoBehaviour
	{
		private const float elasticityAmplitude = .1f;

		[SerializeField]
		[Range(1, float.PositiveInfinity)]
		private float elasticity = 1;

		private Vector3 initialPosition = new();
		private Rigidbody2D? rigidBody;

		void Awake()
		{
			initialPosition = transform.position;
			rigidBody = GetComponent<Rigidbody2D>();
		}

		void FixedUpdate()
		{
			// Compute a velocity that will take us to this clamped position instead.
			var neededVelocity =
				((Vector2)initialPosition - rigidBody!.position)
				/ Time.deltaTime;

			// You can also calculate this as the needed velocity change/acceleration,
			// and add it as a force instead if you prefer.
			rigidBody.velocity =
				neededVelocity
				/ new Vector2(
					1
						- 1 * elasticityAmplitude
						+ elasticity * elasticityAmplitude,
					1
						- 1 * elasticityAmplitude
						+ elasticity * elasticityAmplitude
				);
		}
	}
}
