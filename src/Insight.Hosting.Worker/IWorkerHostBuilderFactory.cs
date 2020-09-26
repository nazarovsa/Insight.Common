using Microsoft.Extensions.Hosting;

namespace Insight.Hosting.Worker
{
	public interface IWorkerHostBuilderFactory<THostedService> : IHostBuilderFactory
		where THostedService : class, IHostedService
	{
	}
}