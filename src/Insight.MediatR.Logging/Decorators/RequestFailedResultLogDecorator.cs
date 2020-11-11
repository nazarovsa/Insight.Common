using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Insight.Dto;
using Insight.Logging;
using Insight.MediatR.Decorators;
using Insight.MediatR.Extensions;
using MediatR;

namespace Insight.MediatR.Logging.Decorators
{
	internal sealed class RequestFailedResultLogDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>,
		IHandlerDecorator
		where TRequest : IRequest<TResponse>
	{
		private readonly ILogService _logService;
		private readonly IRequestHandler<TRequest, TResponse> _inner;

		public RequestFailedResultLogDecorator(ILogService logService, IRequestHandler<TRequest, TResponse> inner)
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
				var executionResult = await _inner.Handle(request, cancellationToken);
				if (executionResult is Result result && !result.Success)
					_logService.Trace(type, "Request {request} returned failed result: {message}", requestType.Name,
						result.Message);

				return executionResult;
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