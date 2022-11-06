using UnityEngine;

namespace Succide.Core.Exceptions
{
	public class MissingComponentException<T> : MissingComponentException
		where T : Component
	{
		public MissingComponentException(string? message = null)
			: base(
				$"<{typeof(T).Name}>{(message is null ? "" : ": ")}{message}"
			) { }
	}
}
