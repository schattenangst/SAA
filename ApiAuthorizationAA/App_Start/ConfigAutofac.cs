
namespace ApiAuthorizationAA.App_Start
{
    using ApiAuthorizationAA.Persistence.EncryptConfiguration;
    using ApiAuthorizationAA.Persistence.SecureUser;
    using ApiAuthorizationAA.Service.EncryptConfiguration;
    using ApiAuthorizationAA.Service.SecureUser;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Model;
    using System.Reflection;
    using System.Web.Http;

    public static class ConfigAutofac
    {
        public static IContainer Configure()
        {
            // Get HttpConfiguration
            var config = GlobalConfiguration.Configuration;

            // Register Web API Controllers
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Application DbContext
            builder.RegisterType<ApplicationDbContext>().As<IRepositoryContext>().InstancePerRequest();

            // Service
            builder.RegisterType<ControlEncryptService>().As<IControlEncryptPersistence>().InstancePerRequest();
            builder.RegisterType<UserHashService>().As<IUserHashService>().InstancePerRequest();

            // Persistence
            builder.RegisterType<ControlEncryptPersistence>().As<IControlEncryptPersistence>().InstancePerRequest();
            builder.RegisterType<UserHashPersistence>().As<IUserHashPersistence>().InstancePerRequest();
            builder.RegisterType<UserHistoricHashPersistence>().As<IUserHistoricHashPersistence>().InstancePerRequest();

            //// Register all type by query
            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(ApiAuthorizationAA.Service)))
            //    .Where(t => t.Namespace.Contains("SecureUser"))
            //    .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            // Create and assign a dependency resolver for Web API to use
            //IContainer container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return builder.Build();
        }
    }
}