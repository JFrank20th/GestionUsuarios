using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SistemaUsuarios.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            var cors = new EnableCorsAttribute("*", "*", "*"); // Permite cualquier origen
            config.EnableCors(cors);

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
