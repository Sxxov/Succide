using Succide.Core.Routing;
using Succide.Core.Things.Interacting;
using UnityEngine;

namespace Succide.Core.Things.Branch
{
	[RequireComponent(typeof(InteractableComptroller))]
	public class BranchComptroller : MonoBehaviour
	{
		[SerializeField]
		private RouteKinds levelKind;

		void Awake()
		{
			GetComponent<InteractableComptroller>()!.OnInteract += async () =>
				await RouteComptroller.instance!.Goto(levelKind);
		}
	}
}
