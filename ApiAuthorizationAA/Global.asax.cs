
namespace ApiAuthorizationAA
{
    using App_Start;
    using Autofac.Integration.WebApi;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Initialize Application
        /// </summary>
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            var container = ConfigAutofac.Configure();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
