using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Transversal.Entidades;
using Transversal.Interfaces;
using Transversal.DependencyResolver;
using CatProductos.Controllers;
using Microsoft.Practices.Unity;

namespace CatProductos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            var container = new UnityContainer();

            ComponentLoader.LoadContainer(container, ".\\bin", "*.dll");

            config.DependencyResolver = new UnityResolver(container);

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "api/{controller}/{id}/{id2}",
                        defaults: new { id = RouteParameter.Optional, id2 = RouteParameter.Optional }
                    );
        }
    }
}
