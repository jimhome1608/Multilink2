using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Multilink2 {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapRoute(
                name: "Default", // Route name
                url: "{controller}/{action}/{SalesMethod}", // URL with parameters
                defaults: new { controller = "Home", action = "vwInternetEnquiries", SalesMethod = "Sales" } // Parameter defaults
                //http://localhost:59861/Home/vwInternetEnquiries?SalesMethod=Sales
            );
        }
    }
}