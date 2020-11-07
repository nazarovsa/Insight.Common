using MediatR;

namespace Insight.MediatR.Tests.Infrastructure
{
	public sealed class DummyCommand : IRequest<bool>
	{
		public string DummyProperty { get; set; }
	}
}