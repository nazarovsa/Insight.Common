using System.Threading.Tasks;

namespace Insight.Testing.Tests.Infrastructure
{
	internal sealed class Service : IService
	{
		public Task Execute()
		{
			return Task.CompletedTask;
		}
	}
}