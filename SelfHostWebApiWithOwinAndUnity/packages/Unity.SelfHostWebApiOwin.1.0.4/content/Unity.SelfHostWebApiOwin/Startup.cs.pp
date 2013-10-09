using Owin;
using System.Web.Http;
using System.Collections.Generic;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using Microsoft.Practices.Unity;

namespace Unity.SelfHostWebApiOwin
{
    public class Startup
    {
        private static readonly IUnityContainer _container = UnityHelpers.GetConfiguredContainer();

        // Your startup logic
        public static void StartServer()
        {
            string baseAddress = "http://localhost:8081/";
            var startup = _container.Resolve<Startup>();
            IDisposable webApplication = WebApp.Start(baseAddress, startup.Configuration);

            try
            {
                Console.WriteLine("Started...");

                Console.ReadKey();
            }
            finally
            {
                webApplication.Dispose();
            }


        }
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

			// Add Unity DependencyResolver
            config.DependencyResolver = new UnityDependencyResolver(SelfHostWebApiOwin.UnityHelpers.GetConfiguredContainer());

			// Add Unity filters provider
			//var providers = config.Services.GetFilterProviders().ToList();
            //config.Services.Add(typeof (IFilterProvider), new UnityFilterAttributeFilterProvider(SelfHostWebApiOwin.UnityHelpers.GetConfiguredContainer(), providers));

            //var defaultprovider = providers.First(i => i is ActionDescriptorFilterProvider);
            //config.Services.Remove(typeof(IFilterProvider), defaultprovider);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}
