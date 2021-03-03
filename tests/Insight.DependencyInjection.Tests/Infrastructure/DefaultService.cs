using System.Threading.Tasks;
using Insight.DependencyInjection.Attributes;

namespace Insight.DependencyInjection.Tests.Infrastructure
{
    [DefaultImplementation(typeof(IService))]
    internal sealed class DefaultService : IService
    {
        private readonly IServiceHelper _serviceHelper;

        public DefaultService(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        public async Task Up()
        {
            await _serviceHelper.Help();
        }
    }
}
