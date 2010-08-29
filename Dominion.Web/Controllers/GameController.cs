using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Rules;
using Dominion.Rules.CardTypes;
using Dominion.Web.ActionFilters;
using Dominion.Web.ViewModels;

namespace Dominion.Web.Controllers
{
    [InjectGame]
    public class GameController : Controller
    {
        public GameController(MultiGameHost host)
        {
            _host = host;
        }

        private readonly MultiGameHost _host;
        public Game CurrentGame { get; set; }
        public string Key { get; set; }

        private TurnContext CurrentTurn
        {
            get { return CurrentGame.CurrentTurn; }
        }

        public ActionResult Index()
        {
            return RedirectToAction("Play");
        }

        [HttpGet]
        public ActionResult Play()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GameData()
        {
            var model = new GameViewModel(CurrentGame, CurrentGame.ActivePlayer);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PlayCard(Guid id)
        {
            var model = new PlayerActionResultViewModel();
            
            var card = CurrentGame.ActivePlayer.Hand
                .Single(c => c.Id == id);

            CurrentTurn.Play((ActionCard)card);

            //model.GameState = new GameViewModel(CurrentGame, this.Url);
            _host.RaiseGameStateUpdated(Key);
            return Json(model);
        }

        public ActionResult BuyCard(Guid id)
        {
            var model = new PlayerActionResultViewModel();
            var pile = CurrentGame.Bank.Piles.Single(p => p.Id == id);

            CurrentTurn.Buy(pile);

            //model.GameState = new GameViewModel(CurrentGame, this.Url);
            _host.RaiseGameStateUpdated(Key);
            return Json(model);
        }

        [HttpPost]
        public ActionResult DoBuys()
        {
            var model = new PlayerActionResultViewModel();

            CurrentTurn.MoveToBuyStep();

            //model.GameState = new GameViewModel(CurrentGame, this.Url);
            _host.RaiseGameStateUpdated(Key);
            return Json(model);
        }

        [HttpPost]
        public ActionResult EndTurn()
        {
            var model = new PlayerActionResultViewModel();

            CurrentTurn.EndTurn();

            //model.GameState = new GameViewModel(CurrentGame, this.Url);
            _host.RaiseGameStateUpdated(Key);
            return Json(model);
        }
    }
}
