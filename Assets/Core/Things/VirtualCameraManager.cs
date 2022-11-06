using System;
using System.Collections.Generic;
using UnityEngine;

namespace Succide.Core.Things
{
	public class VirtualCameraManager : MonoBehaviour
	{
		private GameObject[]? cameras;
		public static VirtualCameraManager? instance;
		private int activeCamera_ = 0;
		public int activeCamera
		{
			get => activeCamera_;
			set
			{
				activeCamera_ = value;

				for (var i = 0; i < cameras!.Length; ++i)
				{
					cameras[i].SetActive(i == activeCamera_);
				}
			}
		}

		void Awake()
		{
			var cameraList = new List<GameObject>();
			foreach (Transform t in transform)
			{
				cameraList.Add(t.gameObject);
			}
			cameras = cameraList.ToArray();
			instance = this;
		}

		void OnDestroy()
		{
			instance = null;
		}
	}
}
