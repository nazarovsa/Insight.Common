using System;
using Insight.Testing.Tests.Infrastructure;
using Xunit;

namespace Insight.Testing.Tests
{
	public class ServiceBagTests
	{
		private readonly ServiceBag _serviceBag;

		public ServiceBagTests()
		{
			_serviceBag = new ServiceBag();
		}


		[Fact]
		public void Should_add_new_service_as_self()
		{
			_serviceBag.AddService<Service>();
			var service = _serviceBag.GetService<Service>();
			Assert.NotNull(service);
		}

		[Fact]
		public void Should_add_new_service_as_interface()
		{
			_serviceBag.AddService<IService, Service>();
			var service = _serviceBag.GetService<IService>();
			Assert.NotNull(service);
		}

		[Fact]
		public void Should_add_instantiated_service_as_self()
		{
			_serviceBag.AddService(new Service());
			var service = _serviceBag.GetService<Service>();
			Assert.NotNull(service);
		}

		[Fact]
		public void Should_add_instantiated_service_as_interface()
		{
			_serviceBag.AddService<IService, Service>(new Service());

			var service = _serviceBag.GetService<IService>();
			Assert.NotNull(service);
		}

		[Fact]
		public void Should_throw_IOE_if_T2_does_not_implements_T1()
		{
			Assert.Throws<InvalidOperationException>(() => _serviceBag.AddService<IService, object>(new object()));
		}
	}
}