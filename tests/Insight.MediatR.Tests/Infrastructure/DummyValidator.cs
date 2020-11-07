using FluentValidation;

namespace Insight.MediatR.Tests.Infrastructure
{
	public sealed class DummyValidator : AbstractValidator<DummyCommand>
	{
		public DummyValidator()
		{
			RuleFor(c => c.DummyProperty)
				.NotEmpty();
		}
	}
}