using System.Threading;
using System.Threading.Tasks;
using Insight.DependencyInjection.Attributes;
using Insight.DependencyInjection.Tests.Infrastructure.Requests;

namespace Insight.DependencyInjection.Tests.Infrastructure.Handlers
{
	[DefaultImplementation(typeof(IHandler<GetRequest, string>))]
	public sealed class DefaultGetHandler : IHandler<GetRequest, string>
	{
		public Task<string> Handle(GetRequest request, CancellationToken cancellationToken = default)
		{
			return Task.FromResult("Get");
		}
	}
}