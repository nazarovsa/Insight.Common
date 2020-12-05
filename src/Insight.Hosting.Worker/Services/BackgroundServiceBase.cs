using System;
using System.Threading;
using System.Threading.Tasks;
using Insight.Hosting.Worker.Options;
using Insight.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Insight.Hosting.Worker.Services
{
	public abstract class BackgroundServiceBase : BackgroundService
	{
		private readonly BackgroundServiceOptions _options;
		protected ILogService<BackgroundServiceBase> LogService { get; }

		protected BackgroundServiceBase(ILogService<BackgroundServiceBase> logService,
			IOptionsSnapshot<BackgroundServiceOptions> options)
		{
			LogService = logService ?? throw new ArgumentNullException(nameof(logService));
			_options = options?.Value ?? throw new ArgumentNullException(nameof(options));
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					await Task.WhenAll(UnitOfWork(stoppingToken),
						Task.Delay(_options.Interval, stoppingToken));
				}
				catch (Exception ex)
				{
					LogService.Error(ex, "Error at background service");

					await Task.Delay(_options.ExceptionDelay, stoppingToken);
				}
			}
		}

		protected abstract Task UnitOfWork(CancellationToken cancellationToken = default);
	}
}