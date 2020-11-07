using Insight.MediatR.Logging.Decorators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.MediatR.Logging.Extensions
{
	public static class ServiceCollectionEx
	{
		public static IServiceCollection AddRequestLogDecorator(this IServiceCollection services)
		{
			services.Decorate(typeof(IRequestHandler<,>), typeof(RequestLogDecorator<,>));

			return services;
		}
	}
}