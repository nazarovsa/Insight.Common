using MediatR;

namespace Insight.MediatR.Tests.Infrastructure.Commands.Dummy
{
	internal sealed class DummyCommand : IRequest<bool>
	{
		public string DummyProperty { get; set; }
	}
}