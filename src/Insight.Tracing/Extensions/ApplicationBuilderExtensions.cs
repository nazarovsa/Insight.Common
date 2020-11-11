using System;
using Microsoft.AspNetCore.Builder;

namespace Insight.Tracing.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseTraceContextMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<TraceContextMiddleware>();

			return app;
		}

		public static IApplicationBuilder UseTraceContextMiddleware(this IApplicationBuilder app, string traceIdHeaderName)
		{
			if (string.IsNullOrWhiteSpace(traceIdHeaderName))
				throw new ArgumentNullException(nameof(traceIdHeaderName));

			app.UseMiddleware<TraceContextMiddleware>(traceIdHeaderName);

			return app;
		}
	}
}