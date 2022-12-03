using System.Collections.Generic;
using System.Threading.Tasks;
using Succide.Core.Behavioural;
using Succide.Core.Player;
using Succide.Core.Routing;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Succide.Core.Things.Death
{
	public class ResultController : MonoBehaviour
	{
		private PlayerFireController playerFire = null!;

		void Awake()
		{
			playerFire =
				PlayerController.player!.GetComponent<PlayerFireController>();
			playerFire.isFiring.OnChanged += OnIsFiringChanged;
		}

		void OnDestroy()
		{
			playerFire.isFiring.OnChanged -= OnIsFiringChanged;
		}

		private void OnIsFiringChanged(bool v)
		{
			if (v)
			{
				SceneManager.LoadScene(0);
			}
		}
	}
}
