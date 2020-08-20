using System.Web.Http;
using BTS.SP.API.PHB.App_Start;


namespace BTS.SP.API.PHB
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            UnityConfig.Register(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
             name: "actionApi",
             routeTemplate: "api/{controller}/{action}/{code}",
             defaults: new { code = RouteParameter.Optional }
            );
        }
    }
}
