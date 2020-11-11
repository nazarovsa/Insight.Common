using Insight.Dto;
using MediatR;

namespace Insight.MediatR.Tests.Infrastructure.Commands.DummyWithFailedResult
{
	internal sealed class DummyWithFailedResultCommand : IRequest<Result>
	{
		public string DummyProperty { get; set; }
	}
}