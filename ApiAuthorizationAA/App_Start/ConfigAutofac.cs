
namespace ApiAuthorizationAA.App_Start
{
    using Persistence.User;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Common.IPersistence.User;
    using Common.IService.Crypography;
    using Common.IService.User;
    using Service.Cryptography.SHA;
    using Service.User;
    using System.Reflection;
    using System.Web.Http;
    using Controllers;
    using Common.IPersistence;
    using Persistence;
    using Model;

    public class ConfigAutofac
    {
        public static void Configure()
        {
            
            // Get HttpConfiguration
            var config = GlobalConfiguration.Configuration;

            // Register Web API Controllers
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register other types
            builder.RegisterType<LoginController>().InstancePerRequest();
            builder.RegisterType<ApplicationDbContext>().As<IRepositoryContext>().InstancePerRequest();
            
            builder.RegisterType<PasswordHashProvider>().As<IEncryptShaServices>().InstancePerRequest();


            //// Service
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();

            ////// Persistence
            //builder.RegisterType<UserPersistence>().As<IUserPersistence>().InstancePerRequest();

            // 

            // Create and assign a dependency resolver for Web API to use
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}