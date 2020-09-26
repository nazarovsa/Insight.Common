using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Insight.Hosting.Worker
{
	public class WorkerHostBuilderDefaultFactory<THostedService> : IWorkerHostBuilderFactory<THostedService>
		where THostedService : class, IHostedService
	{
		public IHostBuilder CreateHostBuilder(string[] args, string baseDirectory = null)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureServices((context, collection) =>
				{
					collection.AddHostedService<THostedService>();
				});
		}
	}
}