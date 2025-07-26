using System.Web.Http;
using PruebaTecnicaEmpleados.Api.Repository.Implementations;
using PruebaTecnicaEmpleados.Api.Repository.Interface;
using PruebaTecnicaEmpleados.Api.Services.Implementations;
using PruebaTecnicaEmpleados.Api.Services.Interface;
using Unity;
using Unity.WebApi;

namespace PruebaTecnicaEmpleados.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserService, UserService>();

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}