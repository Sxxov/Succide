using System;
using System.Linq;
using UnityEngine;

namespace Succide.Core.Routing
{
	public class RouteProvider
	{
		private static readonly Route[] routes = new Route[]
		{
			new Route(
				kind: RouteKinds.Intro,
				sceneName: "IntroScene",
				shouldShowMeasurables: false
			),
			new Route(
				kind: RouteKinds.Branch,
				sceneName: "BranchScene",
				shouldShowMeasurables: true
			),
			new Route(
				kind: RouteKinds.Work,
				sceneName: "WorkScene",
				shouldShowMeasurables: true
			),
			new Route(
				kind: RouteKinds.Fun,
				sceneName: "FunScene",
				shouldShowMeasurables: true
			),
			new Route(
				kind: RouteKinds.Sleep,
				sceneName: "SleepScene",
				shouldShowMeasurables: true
			),
			new Route(
				kind: RouteKinds.Pharmacy,
				sceneName: "PharmacyScene",
				shouldShowMeasurables: true
			),
			new Route(
				kind: RouteKinds.Death,
				sceneName: "DeathScene",
				shouldShowMeasurables: false
			),
		};

		public static Route Get(RouteKinds kind) =>
			(Route?)routes.First((v) => v.kind == kind)
			?? throw new NotSupportedException();
	}
}
