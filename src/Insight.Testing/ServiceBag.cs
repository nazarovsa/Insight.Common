using System;
using System.Collections.Generic;

namespace Insight.Testing
{
	public sealed class ServiceBag
	{
		private readonly Dictionary<Type, object> _services;

		public ServiceBag()
		{
			_services = new Dictionary<Type, object>();
		}

		public T GetService<T>()
		{
			if (!_services.TryGetValue(typeof(T), out var service))
				throw new KeyNotFoundException($"Service of type {typeof(T).FullName} not found");

			return (T) service;
		}

		public void AddService<T>(T service)
		{
			_services.Add(typeof(T), service);
		}

		public void AddService<T>() where T : new()
		{
			AddService(new T());
		}

		public void AddService<T1, T2>(T2 service)
		{
			if (!typeof(T1).IsAssignableFrom(typeof(T2)))
				throw new InvalidOperationException($"{typeof(T2).FullName} does not implements {typeof(T1).FullName}");

			_services.Add(typeof(T1), service);
		}

		public void AddService<T1, T2>() where T2 : new()
		{
			AddService<T1, T2>(new T2());
		}

		public void Configure<T>(Action<T> @delegate)
		{
			var service = GetService<T>();

			@delegate(service);
		}
	}
}