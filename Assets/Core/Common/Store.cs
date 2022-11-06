using System;

namespace Succide.Core.Common
{
	public class Store<T>
	{
		public event Action<T?>? OnChanged;
		private T? value_;

		public T? value
		{
			get => value_;
			set
			{
				if (!value_?.Equals(value) ?? false)
				{
					value_ = value;
					OnChanged?.Invoke(value);
				}
			}
		}

		public Store(T? value = default)
		{
			value_ = value;
		}
	}
}
