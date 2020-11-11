using System;
using System.Threading.Tasks;
using Insight.DependencyInjection.Extensions;
using Insight.DependencyInjection.Tests.Infrastructure;
using Insight.DependencyInjection.Tests.Infrastructure.Handlers;
using Insight.DependencyInjection.Tests.Infrastructure.Requests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Insight.DependencyInjection.Tests
{
	public class ServiceCollectionExtensionsTests
	{
		[Fact]
		public void Should_register_default_implementation_of_interface()
		{
			var services = new ServiceCollection();

			services.RegisterDefaultImplementations(typeof(Service).Assembly);

			var provider = services.BuildServiceProvider();

			var service = provider.GetService<IService>();
			Assert.NotNull(service);
		}

		[Fact]
		public void Should_register_default_implementation_of_generic_interface()
		{
			var services = new ServiceCollection();

			services.RegisterDefaultImplementations(typeof(Service).Assembly, new[] {typeof(IHandler<,>)});

			var provider = services.BuildServiceProvider();

			var defaultPostHandler = provider.GetService<IHandler<PostRequest, string>>();
			var defaultGetHandler = provider.GetService<IHandler<GetRequest, string>>();
			Assert.NotNull(defaultPostHandler);
			Assert.NotNull(defaultGetHandler);
		}

		[Fact]
		public void Should_register_service_as_implemented_interface()
		{
			var services = new ServiceCollection();

			services.AsClosedTypesOf(new[] {typeof(Service).Assembly}, typeof(IService));

			var provider = services.BuildServiceProvider();

			var service = provider.GetService<IService>();
			Assert.NotNull(service);
		}

		[Fact]
		public async Task Should_register_service_as_implemented_generic_interface()
		{
			var services = new ServiceCollection();

			services.AsClosedTypesOf(new[] {typeof(Service).Assembly}, typeof(IHandler<,>));

			var provider = services.BuildServiceProvider();

			var handler = provider.GetService<IHandler<PostRequest, string>>();
			var result = await handler.Handle(new PostRequest());
			Assert.NotNull(handler);
			Assert.Equal("Post", result, StringComparer.InvariantCultureIgnoreCase);
		}
	}
}