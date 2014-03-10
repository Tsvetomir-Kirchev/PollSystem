using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PollSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Poll", action = "AllPolls", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Page",
                url: "{Poll}/{Vote}/{page}/{id}",
                defaults: new { controller = "Poll", action = "Vote" }
            );
        }
    }
}