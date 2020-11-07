using System;

namespace Insight.MediatR.Decorators
{
	public interface IHandlerDecorator
	{
		Type GetHandlerType();
	}
}