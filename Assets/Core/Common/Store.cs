using System;

namespace Succide.Core.Common
{
	public class Store<T>
	{
		public event Action<T?>? OnChanging;
		public event Action<T?>? OnChanged;
		private T? value_;

		public T? value
		{
			get => value_;
			set
			{
				if (!value_?.Equals(value) ?? false)
				{
					OnChanging?.Invoke(value);
					value_ = value;
					OnChanged?.Invoke(value);
				}
			}
		}

		public Store(T? value = default)
		{
			value_ = value;
		}

		public void Reset(T? to = default)
		{
			value = to;
			OnChanged = null;
			OnChanging = null;
		}
	}
}
