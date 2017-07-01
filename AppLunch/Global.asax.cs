using AppLunch.Filters;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppLunch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new UserNameAttribute());
        }
    }
}