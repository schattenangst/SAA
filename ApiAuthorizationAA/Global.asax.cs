
namespace ApiAuthorizationAA
{
    using App_Start;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Initialize Application
        /// </summary>
        protected void Application_Start()
        {            
            ConfigAutofac.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
