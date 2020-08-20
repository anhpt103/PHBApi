using System.Web.Http;
using Telerik.Reporting.Services.WebApi;

namespace BTS.SP.API.PHB
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ReportsControllerConfiguration.RegisterRoutes(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Set("Server", "STCServer");
        }
    }
}
