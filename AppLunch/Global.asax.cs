﻿using AppLunch.Filters;
using Microsoft.ApplicationInsights.Extensibility;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppLunch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new ViewBagGlobalAttribute());

#if (RELEASE)
            TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["InstrumentationKey"];
#endif

        }
    }
}