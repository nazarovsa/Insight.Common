using System.Threading;
using System.Threading.Tasks;
using Insight.DependencyInjection.Attributes;
using Insight.DependencyInjection.Tests.Infrastructure.Requests;

namespace Insight.DependencyInjection.Tests.Infrastructure.Handlers
{
	[DefaultImplementation(typeof(IHandler<PostRequest, string>))]
	public sealed class DefaultPostHandler : IHandler<PostRequest, string>
	{
		public Task<string> Handle(PostRequest request, CancellationToken cancellationToken = default)
		{
			return Task.FromResult("Post");
		}
	}
}