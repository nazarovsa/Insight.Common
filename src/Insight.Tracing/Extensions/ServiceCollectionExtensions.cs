using Microsoft.Extensions.DependencyInjection;

namespace Insight.Tracing.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddTraceContext(this IServiceCollection services)
		{
			services.AddScoped<ITraceContext, TraceContext>();

			return services;
		}
	}
}