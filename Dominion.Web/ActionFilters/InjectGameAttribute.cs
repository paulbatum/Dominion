using System;
using System.Linq;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Web.Controllers;
using Dominion.Web.Bootstrap;
using Autofac;

namespace Dominion.Web.ActionFilters
{
    public class InjectGameAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var gameController = (GameController)filterContext.Controller;

            var gameKey = filterContext.RouteData.Values["id"].ToString();

            var multiHost = AutofacConfig.Container.Resolve<MultiGameHost>();
            
            gameController.CurrentGame = multiHost.FindGame(gameKey).CurrentGame;
        }
    }
}