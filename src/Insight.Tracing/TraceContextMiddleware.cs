using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Insight.Tracing
{
	public sealed class TraceContextMiddleware
	{
		private readonly string _traceIdHeaderName = TraceHeaders.TraceIdHeaderName;

		private readonly RequestDelegate _next;

		public TraceContextMiddleware(RequestDelegate next, string traceIdHeaderName = null)
		{
			if (!string.IsNullOrWhiteSpace(traceIdHeaderName))
				_traceIdHeaderName = traceIdHeaderName;

			_next = next;
		}

		public async Task Invoke(HttpContext context, ITraceContext traceContext)
		{
			context.Request.Headers.TryGetValue(_traceIdHeaderName, out var traceId);

			if (string.IsNullOrWhiteSpace(traceId) && !string.IsNullOrWhiteSpace(context.TraceIdentifier))
				traceId = context.TraceIdentifier;

			context.TraceIdentifier = traceId;
			traceContext.TraceId = traceId;

			context.Response.OnStarting(state =>
			{
				var httpContext = (HttpContext) state;
				if (!httpContext.Response.Headers.ContainsKey(_traceIdHeaderName))
					httpContext.Response.Headers.Add(_traceIdHeaderName, traceId);

				return Task.CompletedTask;
			}, context);

			await _next(context);
		}
	}
}