using System;
using System.Linq;

namespace Insight.DependencyInjection
{
	public static class TypeExtensions
	{
		public static bool IsImplementsAnyGenericType(this Type type)
		{
			return type.GetInterfaces().Any(x => x.IsGenericType);
		}

		public static bool IsImplementsGenericType(this Type type, Type implementedType)
		{
			return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == implementedType);
		}
	}
}