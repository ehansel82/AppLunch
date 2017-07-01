using AppLunch.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(AppLunch.Startup))]

namespace AppLunch
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string identityConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            app.CreatePerOwinContext(() =>
               new IdentityDbContext(identityConnString));

            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) =>
               new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));

            app.CreatePerOwinContext<UserManager<IdentityUser>>(
            (opt, cont) => 
            {
                var userManager = new UserManager<IdentityUser>(cont.Get<UserStore<IdentityUser>>());
                userManager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(opt.DataProtectionProvider.Create());
                userManager.EmailService = new EmailService();
                return userManager;
            });

            app.CreatePerOwinContext<SignInManager<IdentityUser, string>>((opt, cont) =>
               new SignInManager<IdentityUser, string>(cont.Get<UserManager<IdentityUser>>(), cont.Authentication));

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}