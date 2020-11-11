using Insight.MediatR.Logging.Decorators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.MediatR.Logging.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRequestLogDecorator(this IServiceCollection services)
		{
			services.Decorate(typeof(IRequestHandler<,>), typeof(RequestLogDecorator<,>));

			return services;
		}

		public static IServiceCollection AddRequestFailedResultLogDecorator(this IServiceCollection services)
		{
			services.Decorate(typeof(IRequestHandler<,>), typeof(RequestFailedResultLogDecorator<,>));

			return services;
		}
	}
}