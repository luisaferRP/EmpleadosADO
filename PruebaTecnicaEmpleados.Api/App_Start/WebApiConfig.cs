using System.Web.Http;
using System.Web.Http.Routing; // Necesario para IHttpRoute

namespace PruebaTecnicaEmpleados.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            UnityConfig.RegisterComponents();
            // Habilitar el uso de atributos de ruta (como [RoutePrefix] y [Route])
            config.MapHttpAttributeRoutes();

            // Rutas de Web API - Ruta por defecto (puedes ajustar según tus necesidades)
            // Esta ruta genérica es útil si no usas atributos de ruta en todos los controladores,
            // pero si usas atributos en todo, podrías incluso removerla.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Opcional: Configurar para devolver siempre JSON si lo prefieres
            // config.Formatters.Remove(config.Formatters.XmlFormatter);
            // config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // Habilitar Swagger (¡Asegúrate que SwaggerConfig.cs exista y esté configurado!)
            SwaggerConfig.Register(); // <-- ¡Esta línea es crucial para iniciar Swagger!
        }
    }
}