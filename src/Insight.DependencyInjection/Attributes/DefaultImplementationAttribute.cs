using System;

namespace Insight.DependencyInjection.Attributes
{
	public sealed class DefaultImplementationAttribute : Attribute
    {
        public Type Type { get; private set; }

        public DefaultImplementationAttribute(Type type)
        {
            Type = type;
        }
    }
}