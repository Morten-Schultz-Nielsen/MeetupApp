using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Meetup.Websites
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Event Seance Create Not Enough",
                url: "Events/Information/{id}/Seances/Create",
                defaults: new
                {
                    controller = "Events",
                    action = "CreateList"
                }
            );

            routes.MapRoute(
                name: "Event seances",
                url: "Events/Information/{id}/Seances",
                defaults: new
                {
                    controller = "Events",
                    action = "Seances"
                }
            );

            routes.MapRoute(
                name: "Event Information",
                url: "Events/Information/{id}",
                defaults: new
                {
                    controller = "Events",
                    action = "Page"
                }
            );

            routes.MapRoute(
                name: "Invite User",
                url: "Events/Invite/Person/{id}",
                defaults: new
                {
                    controller = "Events",
                    action = "Invite"
                }
            );

            routes.MapRoute(
                name: "Default ID",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }
            );
        }
    }
}
