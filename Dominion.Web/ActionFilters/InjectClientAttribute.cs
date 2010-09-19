using System;
using System.Linq;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Web.Controllers;
using Dominion.Web.Bootstrap;
using Autofac;

namespace Dominion.Web.ActionFilters
{
    public class InjectClientAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Guid playerId = Guid.Parse((string) filterContext.RouteData.Values["id"]);
            var multiHost = AutofacConfig.Container.Resolve<MultiGameHost>();
            var controller = (IHasGameClient)filterContext.Controller;
            controller.Client = multiHost.FindClient(playerId);
        }
    }
}