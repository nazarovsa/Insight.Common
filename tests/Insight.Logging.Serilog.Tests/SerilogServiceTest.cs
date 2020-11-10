using System.Linq;
using Insight.Logging.Serilog.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Insight.Logging.Serilog.Tests
{
	public class SerilogServiceTest
	{
		[Fact]
		public void Should_register_open_generic_serilog_service()
		{
			var services = GetServiceCollection();
			var provider = services.BuildServiceProvider();

			var logService1 = provider.GetService<ILogService<DummyTypeOne>>();
			Assert.NotNull(logService1);
			Assert.True(logService1.GetType().IsGenericType);
			Assert.Equal(logService1.GetType().GenericTypeArguments.First(), typeof(DummyTypeOne));
			
			var logService2 = provider.GetService<ILogService<DummyTypeTwo>>();
			Assert.NotNull(logService2);
			Assert.True(logService2.GetType().IsGenericType);
			Assert.Equal(logService2.GetType().GenericTypeArguments.First(), typeof(DummyTypeTwo));
		}


		private IServiceCollection GetServiceCollection()
		{
			var services = new ServiceCollection();
			services.AddGenericSerilogService();

			return services;
		}

		internal sealed class DummyTypeOne
		{
		}
		
		internal sealed class DummyTypeTwo
		{
		}
	}
}