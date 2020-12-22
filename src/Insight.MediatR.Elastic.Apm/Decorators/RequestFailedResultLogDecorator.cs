using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Apm;
using Elastic.Apm.Api;
using Insight.Logging;
using Insight.MediatR.Decorators;
using Insight.MediatR.Extensions;
using MediatR;

namespace Insight.MediatR.Elastic.Apm.Decorators
{
    internal class RequestApmDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>,
        IHandlerDecorator
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogService _logService;
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public RequestApmDecorator(ILogService logService, IRequestHandler<TRequest, TResponse> inner)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var type = _inner
                .GetHandlerType();

            var transaction = Agent.Tracer.CurrentTransaction;
            if (transaction != null)
            {
                return await transaction
                    .CaptureSpan(type.GetContext(true), ApiConstants.TypeRequest,
                        _ => _inner
                            .Handle(request, cancellationToken), ApmConstants.SubtypeMediator, ApiConstants.ActionExec);
            }

            _logService
                .Warn(this, "APM transaction for {handler} does not exist", type.Name);

            return await _inner
                .Handle(request, cancellationToken);
        }

        public Type GetHandlerType()
        {
            return _inner
                .GetHandlerType();
        }
    }
}
