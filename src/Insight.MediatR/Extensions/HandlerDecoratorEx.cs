using System;
using Insight.MediatR.Decorators;

namespace Insight.MediatR.Extensions
{
	public static class HandlerDecoratorEx
	{
		public static Type GetHandlerType(this object target)
		{
			return target is IHandlerDecorator decorator
				? decorator.GetHandlerType()
				: target.GetType();
		}
	}
}