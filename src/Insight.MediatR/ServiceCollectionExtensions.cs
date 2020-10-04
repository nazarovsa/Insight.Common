using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.MediatR
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies) =>
			services.AddMediatR(assemblies);
	}
}