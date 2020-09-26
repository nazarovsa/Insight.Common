namespace Insight.DependencyInjection.Tests.Infrastructure.Requests
{
	public interface IRequest
	{
	}

	public interface IRequest<TResult> : IRequest
	{
	}
}