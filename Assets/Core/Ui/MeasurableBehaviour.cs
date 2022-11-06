using Succide.Core.Common;
using Succide.Core.Exceptions;
using Succide.Core.Player;
using Succide.Core.Player.Measuring;
using UnityEngine;

namespace Succide.Core.Ui
{
	public class MeasurableBehaviour : MonoBehaviour
	{
		public PlayerMeasurableKinds playerMeasurableKind;

		[HideInInspector]
		public Measurable? measurable;

		[HideInInspector]
		public Store<float> progress = new();

		void Awake()
		{
			measurable = PlayerMeasurableProvider.Get(playerMeasurableKind);
			measurable.OnChanged += OnChanged;
		}

		void OnDestroy()
		{
			measurable!.OnChanged -= OnChanged;
		}

		private void OnChanged(float v) =>
			progress.value = Mathf.InverseLerp(
				measurable!.min,
				measurable.max,
				v
			);
	}
}
