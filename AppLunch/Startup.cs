using AppLunch.DataAccess;
using AppLunch.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AppLunch.Startup))]

namespace AppLunch
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() =>
                new AppContext());

            app.CreatePerOwinContext<UserStore<AppIdentityUser>>((opt, cont) =>
               new UserStore<AppIdentityUser>(cont.Get<AppContext>()));

            app.CreatePerOwinContext<RoleStore<IdentityRole>>((opt, cont) =>
                new RoleStore<IdentityRole>(cont.Get<AppContext>()));

            app.CreatePerOwinContext<UserManager<AppIdentityUser>>(
            (opt, cont) =>
            {
                var userManager = new UserManager<AppIdentityUser>(cont.Get<UserStore<AppIdentityUser>>());
                userManager.UserTokenProvider = new DataProtectorTokenProvider<AppIdentityUser>(opt.DataProtectionProvider.Create());
                userManager.EmailService = new EmailService();
                return userManager;
            });

            app.CreatePerOwinContext<RoleManager<IdentityRole>>((opt, cont) =>
            {
                return new RoleManager<IdentityRole>(cont.Get<RoleStore<IdentityRole>>());
            });

            app.CreatePerOwinContext<SignInManager<AppIdentityUser, string>>((opt, cont) =>
               new SignInManager<AppIdentityUser, string>(cont.Get<UserManager<AppIdentityUser>>(), cont.Authentication));

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}