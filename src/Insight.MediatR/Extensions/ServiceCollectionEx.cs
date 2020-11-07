using System;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.MediatR.Extensions
{
	public static class ServiceCollectionEx
	{
		public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
		{
			if (assemblies == null)
				throw new ArgumentNullException(nameof(assemblies));

			if (!assemblies.Any())
				throw new ArgumentException("At least one assembly should be specified");

			return services.AddMediatR(assemblies);
		}
	}
}