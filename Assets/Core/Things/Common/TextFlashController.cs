using UnityEngine;
using TMPro;
using System;

namespace Succide.Core.Things.Common
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextFlashController : MonoBehaviour
	{
		[SerializeField]
		private float frequency = 1;

		[SerializeField]
		private float amplitude = 1;

		[SerializeField]
		private bool shouldIgnoreTimeScale = false;

		private float sinIndex;
		private Color initialColour;

		private TextMeshProUGUI tmp = null!;

		void Start()
		{
			tmp = GetComponent<TextMeshProUGUI>();
			initialColour = tmp.color;
		}

		void Update()
		{
			var deltaTime = shouldIgnoreTimeScale
				? Time.unscaledDeltaTime
				: Time.deltaTime;

			sinIndex += deltaTime * frequency;
			sinIndex %= Mathf.PI * 2;

			tmp.color = new(
				tmp.color.r,
				tmp.color.g,
				tmp.color.b,
				initialColour.a
					* ((Mathf.Sin(sinIndex) / 2 + Mathf.PI / 2) * amplitude)
			);
		}
	}
}
