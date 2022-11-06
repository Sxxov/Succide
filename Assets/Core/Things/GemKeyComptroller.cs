using Succide.Core.Player;
using UnityEngine;

namespace Succide.Core.Things
{
	[RequireComponent(typeof(PlayerMeasurableController))]
	public class GemKeyComptroller : MonoBehaviour
	{
		private PlayerMeasurableController measurableManager = null!;
		private bool isGemKeyCollectable_ = false;
		public bool isGemKeyCollectable
		{
			get => isGemKeyCollectable_;
			set
			{
				isGemKeyCollectable_ = value;

				foreach (Transform child in transform)
				{
					child.gameObject.SetActive(isGemKeyCollectable_);
				}
			}
		}

		void Awake()
		{
			measurableManager =
				PlayerController.player!.GetComponent<PlayerMeasurableController>()!;
			isGemKeyCollectable = false;
		}

		void Update()
		{
			if (!isGemKeyCollectable && measurableManager!.mood.value >= 3)
			{
				isGemKeyCollectable = true;
			}
		}
	}
}
