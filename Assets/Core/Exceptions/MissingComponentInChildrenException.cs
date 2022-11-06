using UnityEngine;

namespace Succide.Core.Exceptions
{
	public class MissingComponentInChildrenException<T>
		: MissingComponentException<T> where T : Component
	{
		public MissingComponentInChildrenException(string? message = null)
			: base(message) { }
	}
}
