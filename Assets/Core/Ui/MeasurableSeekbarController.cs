using Succide.Core.Common;
using Succide.Core.Exceptions;
using Succide.Core.Player;
using Succide.Core.Player.Measuring;
using UnityEngine;

namespace Succide.Core.Ui
{
	public class MeasurableSeekbarController : MonoBehaviour
	{
		void Awake()
		{
			var rect = transform as RectTransform;
			var initialWidth = rect!.sizeDelta.x;

			this.ForceGetComponentInParent<MeasurableBehaviour>().progress!.OnChanged +=
				(v) =>
				{
					// rect might have been destroyed or deactivated
					if (rect)
					{
						rect.sizeDelta = new Vector2(
							Mathf.Lerp(0, initialWidth, v),
							rect.sizeDelta.y
						);
					}
				};
		}
	}
}
