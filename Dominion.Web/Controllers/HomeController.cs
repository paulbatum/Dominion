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
            var key = _host.CreateNewGame();

            return this.RedirectToAction("Play", "Game", new {id = key});
        }

    }
}
