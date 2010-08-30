using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Dominion.GameHost;

namespace Dominion.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MultiGameHost _host;

        public HomeController(MultiGameHost host)
        {
            _host = host;
        }

        public ActionResult Index()
        {
            return View();
        }        
        
        [HttpPost]
        public ActionResult NewGame()
        {
            string gameKey = _host.CreateNewGame();
            return this.RedirectToAction(x => x.ViewPlayers(gameKey));
        }        

        [HttpGet]
        public ActionResult ViewPlayers(string gameKey)
        {
            var model = _host.GetGameData(gameKey);
            return View("ViewPlayers", model);
        }

        [HttpPost]
        public ActionResult JoinGame(string id, Guid playerId)
        {
            Session["playerId"] = playerId;
            return RedirectToAction("Play", "Game", new { id });
        }


    }
}
