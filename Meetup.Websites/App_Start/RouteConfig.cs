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
                name: "Event Seance Create",
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
                name: "User Actions",
                url: "Users/Profile/Edit",
                defaults: new
                {
                    controller = "Users",
                    action = "Edit"
                }
            );

            routes.MapRoute(
                name: "User List",
                url: "Users/{PageNumber}",
                defaults: new
                {
                    controller = "Users",
                    action = "Index",
                    PageNumber = "1"
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
