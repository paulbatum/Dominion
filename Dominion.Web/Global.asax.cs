using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dominion.Web.Bootstrap;

namespace Dominion.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                "PlayGame",
                "game/{id}/{action}",
                new {action = "Index", controller = "Game"}
                );

            routes.MapRoute(
                "Root", 
                "", 
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "NewGame",
                "home/newgame",
                new { controller = "Home", action = "NewGame" }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            AutofacConfig.Initialize();
        }
    }
}