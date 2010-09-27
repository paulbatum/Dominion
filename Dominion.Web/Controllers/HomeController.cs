using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Dominion.GameHost;
using System.Reflection;
using Dominion.Cards.Actions;
using Dominion.Rules;
using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.Cards.Curses;
using Dominion.Web.ViewModels;

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
            return RedirectToAction("NewGame");
        }        

        [HttpGet]
        public ActionResult NewGame()
        {
            var model = new NewGameViewModel();
            model.CardsToChooseFrom = CardFactory.OptionalCardsForBank.OrderBy(c => c).ToList();
            model.ChosenCards = model.CardsToChooseFrom.Take(10).ToList();

            return View(model);
        }
        
        [HttpPost]
        public ActionResult NewGame(NewGameViewModel model)
        {
            var namesArray = model.Names
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim());

            string gameKey = _host.CreateNewGame(namesArray, model.NumberOfPlayers, model.ChosenCards);
            return this.RedirectToAction(x => x.ViewPlayers(gameKey));
        }        

        [HttpGet]
        public ActionResult ViewPlayers(string gameKey)
        {
            var model = _host.GetGameData(gameKey);
            return View("ViewPlayers", model);
        }

        [HttpPost]
        public ActionResult JoinGame(Guid id)
        {            
            return RedirectToAction("Play", "Game", new { id });
        }
    }
}
