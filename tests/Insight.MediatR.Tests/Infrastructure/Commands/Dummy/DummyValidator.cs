using FluentValidation;

namespace Insight.MediatR.Tests.Infrastructure.Commands.Dummy
{
	internal sealed class DummyValidator : AbstractValidator<DummyCommand>
	{
		public DummyValidator()
		{
			RuleFor(c => c.DummyProperty)
				.NotEmpty();
		}
	}
}