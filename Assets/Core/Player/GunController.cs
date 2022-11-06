using Succide.Core.Exceptions;
using UnityEngine;

namespace Succide.Core.Player
{
	class GunController : MonoBehaviour
	{
		private static readonly float bulletSpeedAmplitude = 20;

		[SerializeField]
		private GameObject? bullet;

		private PlayerFireController? playerFireController;

		void Awake()
		{
			playerFireController =
				this.ForceGetComponentInParent<PlayerFireController>();
			playerFireController.OnFire += OnFire;
		}

		void OnDestroy()
		{
			playerFireController!.OnFire -= OnFire;
		}

		void OnFire()
		{
			var bulletObject = Instantiate(
				bullet,
				transform.position,
				Quaternion.identity
			);

			if (bulletObject is not null)
			{
				var bulletRigidBody = bulletObject.GetComponent<Rigidbody2D>();

				bulletRigidBody.AddForce(
					new Vector2(
						bulletSpeedAmplitude * -transform.lossyScale.x,
						0
					),
					ForceMode2D.Impulse
				);
			}
		}
	}
}
