using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineTestApp.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("favicon.ico");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Robots.txt", "robots.txt", new { controller = "Seo", action = "Robots" });

            routes.MapRoute("Default", "{controller}/{action}", new { controller = "usermembership", action = "login" });

            routes.MapRoute("commonUrl", "{controller}/{action}/{id}", new { id = UrlParameter.Optional });
        }
    }
}
