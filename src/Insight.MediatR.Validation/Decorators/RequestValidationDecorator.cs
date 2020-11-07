using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Insight.MediatR.Decorators;
using Insight.MediatR.Extensions;
using MediatR;

namespace Insight.MediatR.Validation.Decorators
{
	internal sealed class RequestValidationDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>,
		IHandlerDecorator
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		private readonly IRequestHandler<TRequest, TResponse> _inner;

		public RequestValidationDecorator(IEnumerable<IValidator<TRequest>> validators,
			IRequestHandler<TRequest, TResponse> inner)
		{
			_validators = validators ?? throw new ArgumentNullException(nameof(validators));
			_inner = inner ?? throw new ArgumentNullException(nameof(inner));
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			if (_validators != null)
			{
				foreach (var validator in _validators)
				{
					var result = await validator.ValidateAsync(request, cancellationToken);
					if (!result.IsValid)
						throw new ValidationException("Validation error", result.Errors);
				}
			}

			return await _inner.Handle(request, cancellationToken);
		}

		public Type GetHandlerType()
		{
			return _inner
				.GetHandlerType();
		}
	}
}