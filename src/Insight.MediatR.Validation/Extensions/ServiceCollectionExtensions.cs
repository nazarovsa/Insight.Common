using System.Reflection;
using FluentValidation;
using Insight.DependencyInjection.Extensions;
using Insight.MediatR.Validation.Decorators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.MediatR.Validation.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRequestValidationDecorator(this IServiceCollection services,
			params Assembly[] assemblies)
		{
			services.Decorate(typeof(IRequestHandler<,>), typeof(RequestValidationDecorator<,>));
			services.AsClosedTypesOf(assemblies, typeof(IValidator<>));

			return services;
		}
	}
}