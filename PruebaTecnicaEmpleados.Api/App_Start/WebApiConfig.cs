using System.Web.Http;
using System.Web.Http.Routing;

namespace PruebaTecnicaEmpleados.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
   
            UnityConfig.RegisterComponents();
      
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

       
            SwaggerConfig.Register(); // <-- ¡Esta línea es crucial para iniciar Swagger!
        }
    }
}