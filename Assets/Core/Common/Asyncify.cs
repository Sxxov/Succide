using System;
using System.Threading.Tasks;

namespace Succide.Core.Common
{
	public static class Asyncify
	{
		/// <remarks>
		/// Usage:
		/// <code>
		/// 	await Asyncify.Delegate((r) => evt += r, (r) => evt -= r);
		/// </code>
		/// </remarks>
		public static async Task Delegate(
			Action<Action> attach,
			Action<Action> detach,
			Func<bool>? predicate = null
		)
		{
			var (taskCompletionSource, resolve) =
				TaskCompletionSourceAndResolve(predicate);

			attach(resolve);
			await taskCompletionSource.Task;
			detach(resolve);
		}

		public static async Task Delegate<T>(
			Action<Action<T>> attach,
			Action<Action<T>> detach,
			Func<T, bool>? predicate = null
		)
		{
			var (taskCompletionSource, resolve) =
				TaskCompletionSourceAndResolve<T>(predicate);

			attach(resolve);
			await taskCompletionSource.Task;
			detach(resolve);
		}

		private static (TaskCompletionSource<Action> taskCompletionSource, Action resolve) TaskCompletionSourceAndResolve(
			Func<bool>? predicate = null
		)
		{
			var taskCompletionSource = new TaskCompletionSource<Action>();
			var resolve = GenerateResolve(taskCompletionSource, predicate);

			return (taskCompletionSource, resolve);
		}

		private static (TaskCompletionSource<
			Action<T>
		> taskCompletionSource, Action<T> resolve) TaskCompletionSourceAndResolve<T>(
			Func<T, bool>? predicate = null
		)
		{
			var taskCompletionSource = new TaskCompletionSource<Action<T>>();
			var resolve = GenerateResolve(taskCompletionSource, predicate);

			return (taskCompletionSource, resolve);
		}

		private static Action<T> GenerateResolve<T>(
			TaskCompletionSource<Action<T>> taskCompletionSource,
			Func<T, bool>? predicate = null
		)
		{
			void Resolve(T value)
			{
				if (predicate is null || predicate(value))
				{
					taskCompletionSource.SetResult(Resolve);
				}
			}

			return Resolve;
		}

		private static Action GenerateResolve(
			TaskCompletionSource<Action> taskCompletionSource,
			Func<bool>? predicate = null
		)
		{
			void Resolve()
			{
				if (predicate is null || predicate())
				{
					taskCompletionSource.SetResult(Resolve);
				}
			}

			return Resolve;
		}
	}
}
