using System.Threading.Tasks;
using Insight.Tracing;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Insight.Logging.Serilog.Middlewares
{
	internal sealed class SerilogTraceIdMiddleware
	{
		private readonly RequestDelegate _next;

		public SerilogTraceIdMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ITraceContext traceContext)
		{
			using (LogContext.PushProperty("TraceId", traceContext.TraceId))
			{
				await _next.Invoke(context);
			}
		}
	}
}