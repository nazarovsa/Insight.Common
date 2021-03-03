using System;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.DependencyInjection.Attributes
{
	public sealed class DefaultImplementationAttribute : Attribute
    {
        public Type Type { get; private set; }

        public ServiceLifetime Lifetime { get; private set; }

        public DefaultImplementationAttribute(Type type, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Type = type;
            Lifetime = lifetime;
        }
    }
}