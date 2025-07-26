using System.Web.Http;
using WebActivatorEx; // Necesario para PreApplicationStartMethod
using PruebaTecnicaEmpleados.Api; // Aseg�rate de que este using sea correcto para tu namespace
using Swashbuckle.Application;
using System; // Necesario para AppDomain
using System.IO; // Necesario para Path.Combine
using System.Reflection; // Puede ser �til, aunque no estrictamente necesario para este c�digo

// La siguiente l�nea es crucial y le dice a WebActivatorEx que llame a SwaggerConfig.Register() al inicio.
//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PruebaTecnicaEmpleados.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            // Esto DEBE ir aqu�, una sola vez. Se llama en GlobalConfiguration.Configuration.
            //GlobalConfiguration.Configuration.MapHttpAttributeRoutes();

            // var thisAssembly = typeof(SwaggerConfig).Assembly; // No es estrictamente necesario para esta configuraci�n, puedes quitarlo si no lo usas.

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    // Configuraci�n de la versi�n de la API y metadatos
                    c.SingleApiVersion("v1", "API de Usuarios - Prueba T�cnica")
                        .Description("API para la gesti�n de empleados - prueba t�cnica.");

                    // Incluir los comentarios XML para la documentaci�n de tus controladores y modelos
                    // Aseg�rate de que el archivo XML se genere en las propiedades del proyecto (Build -> XML documentation file)
                    c.IncludeXmlComments(GetXmlCommentsPath());

                    // Si tienes m�s de un ensamblado con controladores que quieras documentar, puedes a�adirlos as�:
                    // c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "OtroEnsamblado.XML"));

                })
                .EnableSwaggerUi(c =>
                {
                    // Aqu� puedes configurar la interfaz de usuario de Swagger
                    // Por ejemplo, si necesitas soporte para API Key:
                    // c.EnableApiKeySupport("apiKey", "header");
                });
        }

        // M�todo para obtener la ruta del archivo XML de documentaci�n
        // Este m�todo DEBE estar presente en SwaggerConfig.cs
        protected static string GetXmlCommentsPath()
        {
            // El nombre de tu ensamblado (proyecto) de la API
            var xmlFile = $"{typeof(SwaggerConfig).Assembly.GetName().Name}.XML"; // Esto resolver� a "PruebaTecnicaEmpleados.Api.XML"
            // La ruta donde se genera el archivo XML (normalmente en la carpeta bin del directorio base de la aplicaci�n)
            var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", xmlFile);
            return xmlPath;
        }
    }
}