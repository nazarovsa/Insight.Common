using System.Threading;
using System.Threading.Tasks;
using Insight.Dto;
using MediatR;

namespace Insight.MediatR.Tests.Infrastructure.Commands.DummyWithFailedResult
{
	internal sealed class DummyWithFailedResultHandler : IRequestHandler<DummyWithFailedResultCommand, Result>
	{
		public Task<Result> Handle(DummyWithFailedResultCommand request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result.Fail("Error occured"));
		}
	}
}