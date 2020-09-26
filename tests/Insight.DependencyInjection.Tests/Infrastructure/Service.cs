using System.Threading.Tasks;
using Insight.DependencyInjection.Attributes;

namespace Insight.DependencyInjection.Tests.Infrastructure
{
	[DefaultImplementation(typeof(IService))]
	internal sealed class Service : IService
	{
		public Task Up()
		{
			return Task.CompletedTask;
		}
	}
}