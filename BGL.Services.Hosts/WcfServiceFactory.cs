using Airborne.Logging;
using BGL.Services;
using BGL.Services.Api.Contracts;
using BGL.Services.GitServices;
using Microsoft.Practices.Unity;
using RestSharp;
using Unity.Wcf;

namespace BGL.Services.Hosts
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            // container.LoadConfiguration();
            container.RegisterType<ILogger, DebugLogger>();
            container.RegisterType<IGitService, GitService>();
            container.RegisterType<IRestClient, RestClient>(new InjectionConstructor("https://api.github.com/users/"));
        }
    }    
}