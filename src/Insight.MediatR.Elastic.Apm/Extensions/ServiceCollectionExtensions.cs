using MediatR;
using Insight.MediatR.Elastic.Apm.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.MediatR.Elastic.Apm.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRequestApmDecorator(this IServiceCollection services)
		{
			services.Decorate(typeof(IRequestHandler<,>), typeof(RequestApmDecorator<,>));

			return services;
		}
	}
}