using Succide.Core.Exceptions;

namespace UnityEngine
{
	public static class MonoBehaviourExtensions
	{
		/// <exception cref="MissingComponentException{T}" />
		public static T ForceGetComponent<T>(this Component v)
			where T : Component
		{
			var component = v.GetComponent<T>();

			if (!component)
			{
				throw new MissingComponentException<T>();
			}

			return component;
		}

		/// <exception cref="MissingComponentInParentException{T}" />
		public static T ForceGetComponentInParent<T>(this Component v)
			where T : Component
		{
			var component = v.GetComponentInParent<T>();

			if (!component)
			{
				throw new MissingComponentInParentException<T>();
			}

			return component;
		}

		/// <exception cref="MissingComponentInChildrenException{T}" />
		public static T ForceGetComponentInChildren<T>(this Component v)
			where T : Component
		{
			var component = v.GetComponentInChildren<T>();

			if (!component)
			{
				throw new MissingComponentInChildrenException<T>();
			}

			return component;
		}
	}
}
