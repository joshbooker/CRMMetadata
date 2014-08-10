using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRMMetadata
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute("CRMController", "{controller}/{action}/{id}/{subpath}", new
            {
                controller = "CRM",
                action = "Index",
                id = UrlParameter.Optional,
                subpath = UrlParameter.Optional
            });

            //routes.MapRoute("CRMController", "{controller}/{action}/{id}/{tenantName}", new
            //{
            //    controller = "CRM",
            //    action = "Generate",
            //    id = UrlParameter.Optional,
            //    subpath = UrlParameter.Optional
            //});
        }
    }
}
