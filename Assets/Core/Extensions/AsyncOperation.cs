using System.Threading.Tasks;
using Succide.Core.Common;

namespace UnityEngine
{
	public static class AsyncOperationExtensions
	{
		public static Task ToTask(this AsyncOperation asyncOperation) =>
			Asyncify.Delegate<AsyncOperation>(
				(r) => asyncOperation.completed += r,
				(r) => asyncOperation.completed -= r
			);
	}
}
