using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Web.ViewModels;
using Microsoft.Web.Mvc;
using Newtonsoft.Json;

namespace Dominion.Web.Controllers
{
    [ControllerSessionState(ControllerSessionState.ReadOnly)]
    public class CometController : AsyncController
    {
        private readonly MultiGameHost _host;

        public CometController(MultiGameHost host)
        {
            _host = host;
        }

        [HttpGet]
        public void GameStateAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();
            var key = id.ToString();

            var playerId = (Guid) Session["playerId"];
            
            var client = _host.FindClient(playerId);
            
            client
                .GameStateUpdates
                .Timeout(TimeSpan.FromSeconds(15), Observable.Return(client.GetGameState()))
                .Take(1)
                .Subscribe(gvm =>
                {
                    AsyncManager.Parameters["gameState"] = gvm;
                    AsyncManager.OutstandingOperations.Decrement();
                });            
        }
        
        public ActionResult GameStateCompleted(GameViewModel gameState)
        {            
            return JsonNet(gameState);
        }

        private ActionResult JsonNet(GameViewModel model)
        {
            return new JsonNetResult
            {
                Data = model,
                SerializerSettings = new JsonSerializerSettings { Converters = { new ImageConverter(this.Url) } }                
            };
        }
      
    }
}
