namespace Succide.Core.Routing
{
	public class Route
	{
		public RouteKinds kind;
		public string? sceneName;
		public bool shouldShowMeasurables;

		public Route(
			RouteKinds kind,
			string? sceneName,
			bool shouldShowMeasurables
		)
		{
			this.kind = kind;
			this.sceneName = sceneName;
			this.shouldShowMeasurables = shouldShowMeasurables;
		}
	}
}
