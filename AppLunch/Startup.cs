using AppLunch.DataAccess;
using AppLunch.Models;
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
           /*string identityConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            app.CreatePerOwinContext(() =>
               new IdentityDbContext(identityConnString));*/

            app.CreatePerOwinContext(() =>
                new AppLunchDbContext());

            app.CreatePerOwinContext<UserStore<AppLunchUser>>((opt, cont) =>
               new UserStore<AppLunchUser>(cont.Get<AppLunchDbContext>()));

            app.CreatePerOwinContext<RoleStore<IdentityRole>>((opt, cont) =>
                new RoleStore<IdentityRole>(cont.Get<AppLunchDbContext>()));

            app.CreatePerOwinContext<UserManager<AppLunchUser>>(
            (opt, cont) =>
            {
                var userManager = new UserManager<AppLunchUser>(cont.Get<UserStore<AppLunchUser>>());
                userManager.UserTokenProvider = new DataProtectorTokenProvider<AppLunchUser>(opt.DataProtectionProvider.Create());
                userManager.EmailService = new EmailService();
                return userManager;
            });

            app.CreatePerOwinContext<RoleManager<IdentityRole>>((opt, cont) =>
            {
                return new RoleManager<IdentityRole>(cont.Get<RoleStore<IdentityRole>>());
            });

            app.CreatePerOwinContext<SignInManager<AppLunchUser, string>>((opt, cont) =>
               new SignInManager<AppLunchUser, string>(cont.Get<UserManager<AppLunchUser>>(), cont.Authentication));

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}