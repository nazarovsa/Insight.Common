using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Insight.Logging.Serilog.Extensions;
using Insight.MediatR.Extensions;
using Insight.MediatR.Logging.Decorators;
using Insight.MediatR.Logging.Extensions;
using Insight.MediatR.Tests.Infrastructure.Commands.Dummy;
using Insight.MediatR.Tests.Infrastructure.Commands.DummyWithFailedResult;
using Insight.MediatR.Validation.Decorators;
using Insight.MediatR.Validation.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Xunit;

namespace Insight.MediatR.Tests
{
	public sealed class DecoratorsTests
	{
		[Fact]
		public void Should_decorate_request_with_log_decorator()
		{
			var services = GetServiceCollection();
			services.AddRequestLogDecorator();
			services.AddSerilogService();

			var provider = services.BuildServiceProvider();
			var handler = provider.GetService<IRequestHandler<DummyCommand, bool>>();
			Assert.NotNull(handler);
			Assert.Equal(typeof(RequestLogDecorator<DummyCommand, bool>), handler.GetType());
		}

		[Fact]
		public void Should_decorate_request_with_failed_result_log_decorator()
		{
			var services = GetServiceCollection();
			services.AddRequestFailedResultLogDecorator();
			services.AddSerilogService();

			var provider = services.BuildServiceProvider();
			var handler = provider.GetService<IRequestHandler<DummyCommand, bool>>();
			Assert.NotNull(handler);
			Assert.Equal(typeof(RequestFailedResultLogDecorator<DummyCommand, bool>), handler.GetType());
		}

		[Fact]
		public void Should_decorate_request_with_validation_decorator()
		{
			var services = GetServiceCollection();
			services.AddRequestValidationDecorator();

			var provider = services.BuildServiceProvider();
			var handler = provider.GetService<IRequestHandler<DummyCommand, bool>>();
			Assert.NotNull(handler);
			Assert.Equal(typeof(RequestValidationDecorator<DummyCommand, bool>), handler.GetType());
		}

		[Fact]
		public void Should_throw_validation_exception()
		{
			var services = GetServiceCollection();
			services.AddRequestValidationDecorator();

			var provider = services.BuildServiceProvider();
			var handler = provider.GetService<IRequestHandler<DummyCommand, bool>>();
			Assert.NotNull(handler);

			Assert.ThrowsAsync<ValidationException>(async () =>
				await handler.Handle(new DummyCommand(), CancellationToken.None));
		}

		private IServiceCollection GetServiceCollection()
		{
			var services = new ServiceCollection();
			services.AddMediator(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}