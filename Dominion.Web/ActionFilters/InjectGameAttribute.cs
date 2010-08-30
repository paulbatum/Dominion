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

            string gameKey = filterContext.RouteData.Values["id"].ToString();
            Guid playerId = (Guid) filterContext.RequestContext.HttpContext.Session["playerId"];

            var multiHost = AutofacConfig.Container.Resolve<MultiGameHost>();

            //gameController.Key = gameKey;
            gameController.Host = multiHost.FindGame(gameKey);
            gameController.Client = multiHost.FindClient(playerId);
        }
    }
}