using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Rules;
using Dominion.Rules.CardTypes;
using Dominion.Web.ViewModels;

namespace Dominion.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameHost _gameHost;

        public GameController(IGameHost gameHost)
        {
            _gameHost = gameHost;
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
            var model = new GameViewModel(_gameHost);
            return Json(model, JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        public ActionResult PlayCard(Guid id)
        {            
            var model = new PlayerActionResultViewModel();

            try
            {
                var card = _gameHost.CurrentGame.ActivePlayer.Hand                    
                    .Single(c => c.Id == id);

                if(_gameHost.CurrentTurn.CanPlay(card))
                {
                    _gameHost.CurrentTurn.Play((ActionCard)card);
                    model.Success = true;
                    model.GameState = new GameViewModel(_gameHost);
                }
                else
                {
                    model.Success = false;
                    model.ErrorMessage = "Not an action card";
                }
            }
            catch (Exception e)
            {
                model.Success = false;
                model.ErrorMessage = e.ToString();
            }

            return Json(model);
        }

        public ActionResult BuyCard(Guid id)
        {
            var model = new PlayerActionResultViewModel();

            try
            {
                var pile = _gameHost.CurrentGame.Bank.Piles.Single(p => p.Id == id);

                if (_gameHost.CurrentTurn.CanBuy(pile))
                {
                    _gameHost.CurrentTurn.Buy(pile);
                    model.Success = true;
                    model.GameState = new GameViewModel(_gameHost);
                }
                else
                {
                    model.Success = false;
                    model.ErrorMessage = "Cannot buy .";
                }
            }
            catch (Exception e)
            {
                model.Success = false;
                model.ErrorMessage = e.ToString();
            }

            return Json(model);
        }

        [HttpPost]
        public ActionResult DoBuys()
        {
            var model = new PlayerActionResultViewModel();
            _gameHost.CurrentTurn.MoveToBuyStep();

            model.Success = true;
            model.GameState = new GameViewModel(_gameHost);

            return Json(model);
        }

        [HttpPost]
        public ActionResult EndTurn()
        {
            var model = new PlayerActionResultViewModel();

            if (_gameHost.NextTurn())
            {

                model.Success = true;
                model.GameState = new GameViewModel(_gameHost);
            }
            else
            {

                model.Success = false;
                model.ErrorMessage = "Game has ended. Refresh the page to start a new game.";
                _gameHost.BeginNewGame();
            }

            return Json(model);
        }
    }
}
