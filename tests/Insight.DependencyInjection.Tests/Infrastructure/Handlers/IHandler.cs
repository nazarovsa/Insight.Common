using System.Threading;
using System.Threading.Tasks;
using Insight.DependencyInjection.Tests.Infrastructure.Requests;

namespace Insight.DependencyInjection.Tests.Infrastructure.Handlers
{
	public interface IHandler<in TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
		public Task<TResult> Handle(TRequest request, CancellationToken cancellationToken = default);
	}
}