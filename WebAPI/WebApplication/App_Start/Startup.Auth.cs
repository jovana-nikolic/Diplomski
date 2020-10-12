using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Owin;

[assembly: OwinStartup(typeof(WebApplication.Startup))]

namespace WebApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                //just to show bearer
                AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                AllowedAudiences = new[] { "test" },
                IssuerSecurityKeyProviders = new[] {new SymmetricKeyIssuerSecurityKeyProvider("test", "YXNkMTIz")}
            });
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
