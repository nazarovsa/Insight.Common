using System;
using System.Linq;

namespace Insight.DependencyInjection.Extensions
{
	public static class TypeExtensions
	{
		public static bool IsImplementsAnyGenericType(this Type type)
			=> type
				.GetInterfaces()
				.Any(x => x.IsGenericType);

		public static bool IsImplementsGenericType(this Type type, Type implementedType)
			=> type
				.GetInterfaces()
				.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == implementedType);
	}
}