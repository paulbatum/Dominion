using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Dominion.GameHost;
using Dominion.Rules;
using Dominion.Rules.CardTypes;
using Dominion.Web.ActionFilters;
using Dominion.Web.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Dominion.Web.Controllers
{
    [InjectClient]
    public class GameController : Controller, IHasGameClient
    {        
        public IGameClient Client { get; set; }

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
        public ActionResult GameData(string playerId)
        {
            var model = Client.GetGameState();
            return new GameViewModelResult(model, this);
        }

        [HttpPost]
        public ActionResult PlayCard(Guid id)
        {
            var message = new PlayCardMessage(playerId: Client.PlayerId, cardId: id);
            Client.AcceptMessage(message);            
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult BuyCard(Guid id)
        {
            var message = new BuyCardMessage(playerId: Client.PlayerId, pileId: id);
            Client.AcceptMessage(message);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DoBuys()
        {
            var message = new MoveToBuyStepMessage(playerId: Client.PlayerId);
            Client.AcceptMessage(message);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult EndTurn()
        {
            var message = new EndTurnMessage(playerId: Client.PlayerId);
            Client.AcceptMessage(message);
            return new EmptyResult();            
        }

        [HttpPost]
        public ActionResult SelectCards(Guid[] ids)
        {
            var message = new SelectCardsFromHandMessage(Client.PlayerId, ids);
            Client.AcceptMessage(message);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult MakeChoice(string choice)
        {
            var message = new ChoiceMessage(Client.PlayerId, choice);
            Client.AcceptMessage(message);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult SelectPile(Guid id)
        {
            var message = new ChooseAPileMessage(Client.PlayerId, id);
            Client.AcceptMessage(message);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Chat(string message)
        {
            Client.SendChatMessage(message);
            return new EmptyResult();
        }

        
    }

    public class GameViewModelResult : JsonNetResult
    {
        public GameViewModelResult(GameViewModel model, Controller controller)
        {
            model.Log = controller.Server.HtmlEncode(model.Log)
                .Replace(Environment.NewLine, "<br/>"); // there has to be a better way to do this, why am I so dumb
            Data = model;
            SerializerSettings = new JsonSerializerSettings { Converters = { new ImageConverter(controller.Url) } };
        }
    }
    
    public class ImageConverter : KeyValuePairConverter
    {
        private readonly UrlHelper _url;

        public ImageConverter(UrlHelper url)
        {
            _url = url;
        }

        public override bool CanConvert(Type objectType)
        {
            return
                new[]
                    {
                        typeof (CardViewModel), 
                        typeof (CardPileViewModel), 
                        typeof (DeckViewModel),
                        typeof (DiscardPileViewModel)                        
                    }
                .Contains(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string cardName = string.Empty;

            if (value is CardViewModel)
                cardName = ((CardViewModel)value).Name;
            else if (value is CardPileViewModel)
            {
                var pile = ((CardPileViewModel) value);
                cardName = (!pile.IsLimited) || pile.Count > 0 ? pile.Name : "empty";
            }
            else if (value is DeckViewModel)
                cardName = ((DeckViewModel)value).IsEmpty ? "empty" : "deck";
            else if (value is DiscardPileViewModel)
                cardName = ((DiscardPileViewModel)value).IsEmpty ? "empty" : ((DiscardPileViewModel)value).TopCardName;

            JObject o = JObject.FromObject(value);
            o["ImageUrl"] = _url.Content(string.Format("~/Content/Images/Cards/{0}.jpg", cardName));
            writer.WriteRawValue(o.ToString());
        }
    }
    
}
