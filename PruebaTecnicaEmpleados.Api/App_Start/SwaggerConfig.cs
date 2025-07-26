using System.Web.Http;
using WebActivatorEx; // Necesario para PreApplicationStartMethod
using PruebaTecnicaEmpleados.Api; // Asegúrate de que este using sea correcto para tu namespace
using Swashbuckle.Application;
using System; // Necesario para AppDomain
using System.IO; // Necesario para Path.Combine
using System.Reflection; // Puede ser útil, aunque no estrictamente necesario para este código

// La siguiente línea es crucial y le dice a WebActivatorEx que llame a SwaggerConfig.Register() al inicio.
//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PruebaTecnicaEmpleados.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            // Esto DEBE ir aquí, una sola vez. Se llama en GlobalConfiguration.Configuration.
            //GlobalConfiguration.Configuration.MapHttpAttributeRoutes();

            // var thisAssembly = typeof(SwaggerConfig).Assembly; // No es estrictamente necesario para esta configuración, puedes quitarlo si no lo usas.

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    // Configuración de la versión de la API y metadatos
                    c.SingleApiVersion("v1", "API de Usuarios - Prueba Técnica")
                        .Description("API para la gestión de empleados - prueba técnica.");

                    // Incluir los comentarios XML para la documentación de tus controladores y modelos
                    // Asegúrate de que el archivo XML se genere en las propiedades del proyecto (Build -> XML documentation file)
                    c.IncludeXmlComments(GetXmlCommentsPath());

                    // Si tienes más de un ensamblado con controladores que quieras documentar, puedes añadirlos así:
                    // c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "OtroEnsamblado.XML"));

                })
                .EnableSwaggerUi(c =>
                {
                    // Aquí puedes configurar la interfaz de usuario de Swagger
                    // Por ejemplo, si necesitas soporte para API Key:
                    // c.EnableApiKeySupport("apiKey", "header");
                });
        }

        // Método para obtener la ruta del archivo XML de documentación
        // Este método DEBE estar presente en SwaggerConfig.cs
        protected static string GetXmlCommentsPath()
        {
            // El nombre de tu ensamblado (proyecto) de la API
            var xmlFile = $"{typeof(SwaggerConfig).Assembly.GetName().Name}.XML"; // Esto resolverá a "PruebaTecnicaEmpleados.Api.XML"
            // La ruta donde se genera el archivo XML (normalmente en la carpeta bin del directorio base de la aplicación)
            var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", xmlFile);
            return xmlPath;
        }
    }
}