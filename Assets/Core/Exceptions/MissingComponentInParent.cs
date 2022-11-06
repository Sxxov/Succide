using UnityEngine;

namespace Succide.Core.Exceptions
{
	public class MissingComponentInParentException<T>
		: MissingComponentException<T> where T : Component
	{
		public MissingComponentInParentException(string? message = null)
			: base(message) { }
	}
}
