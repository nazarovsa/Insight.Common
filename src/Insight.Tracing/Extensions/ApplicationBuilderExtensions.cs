using System;
using Microsoft.AspNetCore.Builder;

namespace Insight.Tracing.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder AddTraceContext(this IApplicationBuilder services)
		{
			services.UseMiddleware<TraceContextMiddleware>();

			return services;
		}

		public static IApplicationBuilder AddTraceContext(this IApplicationBuilder services, string traceIdHeaderName)
		{
			if (string.IsNullOrWhiteSpace(traceIdHeaderName))
				throw new ArgumentNullException(nameof(traceIdHeaderName));

			services.UseMiddleware<TraceContextMiddleware>(traceIdHeaderName);

			return services;
		}
	}
}