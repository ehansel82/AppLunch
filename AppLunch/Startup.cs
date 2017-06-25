using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartup(typeof(AppLunch.Startup))]

namespace AppLunch
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string identityConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            app.CreatePerOwinContext(() => new IdentityDbContext(identityConnString));
            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) => new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));
            app.CreatePerOwinContext<UserManager<IdentityUser>>((opt, cont) => new UserManager<IdentityUser>(cont.Get<UserStore<IdentityUser>>()));
        }
    }
}
