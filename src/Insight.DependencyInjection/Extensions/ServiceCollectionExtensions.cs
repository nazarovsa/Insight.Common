using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Insight.DependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AsClosedTypesOf(this IServiceCollection services,
            Assembly assembly,
            Type implementedType,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            return services.AsClosedTypesOf(new[] { assembly }, implementedType, lifetime);
        }

        public static IServiceCollection AsClosedTypesOf(this IServiceCollection services,
            Assembly[] assemblies,
            Type implementedType,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var types = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract);

            if (implementedType.IsGenericType)
                foreach (var type in types.Where(x => x.IsImplementsGenericType(implementedType)))
                {
                    var @interface = type.GetInterfaces()
                        .Where(x => x.IsGenericType)
                        .FirstOrDefault(x => x.GetGenericTypeDefinition() == implementedType);

                    if (@interface == null)
                        throw new ArgumentException(nameof(@interface));

                    var genericType = implementedType.MakeGenericType(@interface.GetGenericArguments());
                    services.Add(new ServiceDescriptor(genericType, type, lifetime));
                }
            else
                foreach (var type in types.Where(implementedType.IsAssignableFrom))
                    services.Add(new ServiceDescriptor(implementedType, type, lifetime));

            return services;
        }

        public static IServiceCollection RegisterDefaultImplementations(this IServiceCollection services,
            Assembly assembly,
            Type @interface,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            return services.RegisterDefaultImplementations(assembly, new[] { @interface }, lifetime);
        }


        public static IServiceCollection RegisterDefaultImplementations(this IServiceCollection services,
            Assembly assembly,
            Type[] interfaces,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            return services.RegisterDefaultImplementations(new[] { assembly }, interfaces, lifetime);
        }

        public static IServiceCollection RegisterDefaultImplementations(this IServiceCollection services,
            Assembly[] assemblies,
            Type[] interfaces,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            if (interfaces == null || !interfaces.Any())
                throw new ArgumentException("At least one interface should be provided");

            var types = GetTypesWithDefaultImplementationAttribute(assemblies, interfaces);

            foreach (var type in types)
            {
                var implementedType = type.GetCustomAttribute<DefaultImplementationAttribute>()?.Type;
                services.Add(new ServiceDescriptor(implementedType, type, lifetime));
            }

            return services;
        }

        public static IServiceCollection RegisterDefaultImplementations(this IServiceCollection services,
            Assembly assembly)
        {
            return services.RegisterDefaultImplementations(new[] { assembly });
        }

        public static IServiceCollection RegisterDefaultImplementations(this IServiceCollection services,
            Assembly[] assemblies)
        {
            var types = GetTypesWithDefaultImplementationAttribute(assemblies);

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<DefaultImplementationAttribute>();
                // ReSharper disable once PossibleNullReferenceException : all types have attribute
                services.Add(new ServiceDescriptor(attribute.Type, type, attribute.Lifetime));
            }

            return services;
        }

        private static IReadOnlyCollection<Type> GetTypesWithDefaultImplementationAttribute(Assembly[] assemblies,
            params Type[] implementedTypes)
        {
            var types = assemblies
                .SelectMany(x => x.GetTypes()
                    .Where(t =>
                    {
                        var type = t.GetCustomAttribute<DefaultImplementationAttribute>()?.Type;
                        return t.IsGenericType
                            ? t.IsClass && !t.IsAbstract && type != null && t.GetGenericTypeDefinition() == type
                            : t.IsClass && !t.IsAbstract && type != null && type.IsAssignableFrom(t);
                    }))
                .ToArray();

            if (implementedTypes != null && implementedTypes.Any())
            {
                types = types.Where(x =>
                    {
                        foreach (var implementedType in implementedTypes)
                        {
                            return x.IsImplementsAnyGenericType()
                                ? x.IsImplementsGenericType(implementedType)
                                : implementedType.IsAssignableFrom(x);
                        }

                        return false;
                    })
                    .ToArray();
            }

            var multipleDefaultImplementations = types
                .GroupBy(x => x.GetCustomAttribute<DefaultImplementationAttribute>()?.Type, x => x)
                .Where(x => x.Count() > 1)
                .ToArray();

            if (multipleDefaultImplementations.Any())
            {
                var sb = new StringBuilder();
                foreach (var error in multipleDefaultImplementations)
                {
                    sb.Append(
                        $"Multiple default implementations found for {error.Key.FullName}: {string.Join(", ", error.Select(x => x.FullName))}");
                    sb.Append('\n');
                }

                throw new InvalidOperationException(sb.ToString());
            }

            return types;
        }
    }
}
