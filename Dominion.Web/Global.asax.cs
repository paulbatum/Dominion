using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dominion.Web.Bootstrap;
using MvcContrib.Routing;
using System.Threading;

namespace Dominion.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Comet",
                "game/{id}/gamestateloop",
                new { action = "GameState", controller = "Comet" }
                );

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
                "Home",
                "home/{Action}",
                new { controller = "Home", action = "Index" }
            );
           

        }

        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            //RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);

            AutofacConfig.Initialize();
        }
    }
}