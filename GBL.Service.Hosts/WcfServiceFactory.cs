using Backbone.Logging;
using GBL.Services;
using GBL.Services.Api.Contracts;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Wcf;

namespace GBL.Service.Hosts
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            // container.LoadConfiguration();
            container.RegisterType<ILogger, DebugLogger>();
            container.RegisterType<IGitService, GitService>();
        }
    }
}