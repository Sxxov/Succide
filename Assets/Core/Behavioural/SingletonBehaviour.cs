using UnityEngine;

namespace Succide.Core.Behavioural
{
	public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T? instance_;
		public static T? instance
		{
			get =>
				instance_ || (instance_ = FindObjectOfType<T>())
					? instance_
					: null;
		}
	}
}
