using System.Threading.Tasks;
using Insight.DependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Insight.DependencyInjection.Tests.Infrastructure
{
    [DefaultImplementation(typeof(IServiceHelper), ServiceLifetime.Singleton)]
    internal sealed class DefaultServiceHelper : IServiceHelper
    {
        public async Task Help()
        {
            await Task.Delay(2000);
        }
    }
}