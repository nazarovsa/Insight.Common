using Microsoft.AspNetCore.Builder;

namespace Insight.Logging.Serilog.Middlewares.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseSerilogTraceIdMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<SerilogTraceIdMiddleware>();
			return app;
		}
	}
}