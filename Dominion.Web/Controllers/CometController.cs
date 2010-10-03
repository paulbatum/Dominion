using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominion.GameHost;
using Dominion.Web.ActionFilters;
using Dominion.Web.ViewModels;
using Microsoft.Web.Mvc;
using Newtonsoft.Json;

namespace Dominion.Web.Controllers
{
    [InjectClient]
    public class CometController : AsyncController, IHasGameClient
    {
        public IGameClient Client { get; set; }

        [HttpGet]
        public void GameStateAsync(long? mostRecentVersion)
        {
            AsyncManager.OutstandingOperations.Increment();

            var currentGameState = Client.GetGameState();
            if (mostRecentVersion == null || mostRecentVersion < currentGameState.Version)
            {
                AsyncManager.Parameters["gameState"] = currentGameState;
                AsyncManager.OutstandingOperations.Decrement();
            }
            
            Client
                .GameStateUpdates
                .Timeout(TimeSpan.FromSeconds(15), Observable.Return(Client.GetGameState()))
                .Take(1)
                .Subscribe(gvm =>
                {
                    AsyncManager.Parameters["gameState"] = gvm;
                    AsyncManager.OutstandingOperations.Decrement();
                });            
        }
        
        public ActionResult GameStateCompleted(GameViewModel gameState)
        {
            return new GameViewModelResult(gameState, this);
        }



        [HttpGet]
        public void ChatAsync()
        {
            AsyncManager.OutstandingOperations.Increment();

            Client
                .ChatMessages
                .Timeout(TimeSpan.FromSeconds(15), Observable.Return(""))
                .Take(1)
                .Subscribe(message =>
                {
                    AsyncManager.Parameters["message"] = message;
                    AsyncManager.OutstandingOperations.Decrement();
                });
        }

        public ActionResult ChatCompleted(string message)
        {            
            return new JsonNetResult { Data = new {message} };
        }
      
    }

    public interface IHasGameClient
    {
        IGameClient Client { get; set; }
    }
}
