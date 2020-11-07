using Microsoft.Extensions.DependencyInjection;

namespace Insight.Logging.Serilog.Extensions
{
	public static class ServiceCollectionEx
	{
		public static IServiceCollection AddSerilogService(this IServiceCollection services)
		{
			services.AddScoped<ILogService, SerilogService>();
			return services;
		}
	}
}