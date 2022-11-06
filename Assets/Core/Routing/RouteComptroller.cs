using System;
using System.Threading.Tasks;
using Succide.Core.Behavioural;
using Succide.Core.Common;
using Succide.Core.Player;
using Succide.Core.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Succide.Core.Routing
{
	public class RouteComptroller : SingletonBehaviour<RouteComptroller>
	{
		public event Action<Route>? OnChanged;
		public Route? currRoute { get; private set; }

		async void Awake()
		{
			await Goto(RouteKinds.Intro);
		}

		public async Task Goto(RouteKinds kind) =>
			await Goto(RouteProvider.Get(kind));

		public async Task Goto(Route route)
		{
			if (currRoute is not null)
			{
				await SceneManager
					.UnloadSceneAsync(currRoute.sceneName)
					.ToTask();
			}

			await SceneManager
				.LoadSceneAsync(route.sceneName, LoadSceneMode.Additive)
				.ToTask();

			foreach (
				var child in GetComponentsInChildren<MeasurableBehaviour>(true)
			)
			{
				child.gameObject.SetActive(route.shouldShowMeasurables);
			}

			var beacon = PlayerBeaconBehaviour.instance!;

			PlayerController.player!.transform.position = beacon
				.transform
				.position;

			PlayerController.player
				.GetComponent<PlayerMovementController>()!
				.Reset();

			foreach (Transform child in PlayerController.player.transform)
			{
				Destroy(child.gameObject);
			}

			foreach (Transform child in beacon.transform)
			{
				child.SetParent(PlayerController.player.transform);
			}

			currRoute = route;

			OnChanged?.Invoke(route);
		}

		public Task AwaitTo(RouteKinds kind) =>
			AwaitTo(RouteProvider.Get(kind));

		public Task AwaitTo(Route route) =>
			Asyncify.Delegate<Route>(
				(r) => OnChanged += r,
				(r) => OnChanged -= r,
				(currRoute) => currRoute == route
			);

		public Task AwaitAway(RouteKinds kind) =>
			AwaitAway(RouteProvider.Get(kind));

		public Task AwaitAway(Route route) =>
			Asyncify.Delegate<Route>(
				(r) => OnChanged += r,
				(r) => OnChanged -= r,
				(currRoute) => currRoute != route
			);
	}
}
