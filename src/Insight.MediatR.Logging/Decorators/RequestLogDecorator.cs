using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Insight.Logging;
using Insight.MediatR.Decorators;
using Insight.MediatR.Extensions;
using MediatR;

namespace Insight.MediatR.Logging.Decorators
{
	internal sealed class RequestLogDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>,
		IHandlerDecorator
		where TRequest : IRequest<TResponse>
	{
		private readonly ILogService _logService;
		private readonly IRequestHandler<TRequest, TResponse> _inner;

		public RequestLogDecorator(ILogService logService, IRequestHandler<TRequest, TResponse> inner)
		{
			_logService = logService ?? throw new ArgumentNullException(nameof(logService));
			_inner = inner ?? throw new ArgumentNullException(nameof(inner));
		}

		[DebuggerStepThrough]
		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var requestType = request
				.GetType();

			var type = _inner
				.GetHandlerType();

			try
			{
				_logService.Trace(type, "Executing request {request}:\r\n{@body}", requestType.Name, request);

				var result = await _inner.Handle(request, cancellationToken);
				if (result == null)
					_logService.Trace(type, "Request {request} returned null", requestType.Name);

				return result;
			}
			catch (Exception ex)
			{
				_logService.Error(type, ex, "Error at request execution {request}", requestType.Name);

				throw;
			}
		}

		public Type GetHandlerType()
		{
			return _inner
				.GetHandlerType();
		}
	}
}