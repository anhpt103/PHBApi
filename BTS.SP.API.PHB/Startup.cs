using System;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.API.PHB.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.Practices.Unity;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Auth;
using Repository.Pattern.Ef6;
using BTS.SP.API.PHB.App_Start;

[assembly: OwinStartup(typeof(BTS.SP.API.PHB.Startup))]

namespace BTS.SP.API.PHB
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            ConfigureOAuth(app);

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
