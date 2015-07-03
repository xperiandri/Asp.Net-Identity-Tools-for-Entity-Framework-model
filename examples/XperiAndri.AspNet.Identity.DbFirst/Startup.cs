using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web;
using XperiAndri.AspNet.Identity.DbFirst;
using XperiAndri.AspNet.Identity.DbFirst.Security;
using XperiAndri.AspNet.Identity.EntityFramework.Identity;
using XperiAndri.AspNet.Identity.EntityFramework.Models;

[assembly: OwinStartup(typeof(Startup))]

namespace XperiAndri.AspNet.Identity.DbFirst
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Register UserManager in ApplicationDbContext in OWIN context
            app.CreatePerOwinContext<ApplicationDbContext>(() => new ApplicationDbContext());
            app.CreatePerOwinContext<UserManager<User, Guid>>(
                (IdentityFactoryOptions<UserManager<User, Guid>> options, IOwinContext context) =>
                    new UserManager<User, Guid>(new UserStore(context.Get<ApplicationDbContext>())));

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(
                    "self", () => HttpContext.Current.GetOwinContext().GetUserManager<UserManager<User, Guid>>()),
                AuthorizeEndpointPath = new PathString("/api/account/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            });
        }
    }
}