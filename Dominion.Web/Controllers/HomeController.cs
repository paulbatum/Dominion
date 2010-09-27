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
            return View();            
        }
        
        [HttpPost]
        public ActionResult NewGame(string names, int numberOfPlayers, string[] selectedCards)
        {
            var namesArray = names
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim());

            string gameKey = _host.CreateNewGame(namesArray, numberOfPlayers, selectedCards);
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

        public static IDictionary<string, Type> CardsForPurchase
        {
            get
            {
                var allCards = Assembly.GetAssembly(typeof(Festival))
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Card)));

                var cardsToExclude = new Type[]{
                    typeof(Copper),
                    typeof(Silver),
                    typeof(Gold),
                    typeof(Estate),
                    typeof(Duchy),
                    typeof(Province),
                    typeof(Curse),
                };

                return allCards
                    .Except(cardsToExclude)
                    .ToDictionary(c => c.Name, c => c);
            }
        }
    }
}
