using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Web.ViewModels;

namespace Dominion.Web.Controllers
{
    public class CometController : AsyncController
    {
        private readonly MultiGameHost _host;

        public CometController(MultiGameHost host)
        {
            _host = host;
        }

        [HttpGet, NoAsyncTimeout]
        public void GameStateAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();
            var key = id.ToString();
            
            _host.GameStateUpdated += gameKey =>
            {                
                if (key == gameKey)
                {
                    AsyncManager.Parameters["gameKey"] = gameKey;
                    AsyncManager.OutstandingOperations.Decrement();
                }
            };
        }
        
        public ActionResult GameStateCompleted(string gameKey)
        {
            var game = _host.FindGame(gameKey).CurrentGame;
            return Json(new GameViewModel(game, this.Url), JsonRequestBehavior.AllowGet);
        }

    }
}
