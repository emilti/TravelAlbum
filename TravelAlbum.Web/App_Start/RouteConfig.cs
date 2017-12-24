using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TravelAlbum.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
            name: "CultureDefault",
            url: "{lang}/{controller}/{action}/{id}",
            defaults: new { controller = "Images", action = "SearchImages", id = UrlParameter.Optional },
            constraints: new { lang = "bg|en"}
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Images", action = "SearchImages", id = UrlParameter.Optional }
            );
        }
    }
}
