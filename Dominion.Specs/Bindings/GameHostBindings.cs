using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameHost;
using TechTalk.SpecFlow;
using System.Threading;

namespace Dominion.Specs.Bindings
{
    [Binding]
    public class GameHostBindings : BindingBase
    {
        private LockingGameHost _gameHost;
        private IList<GameClient> _clients;
        private IDictionary<GameClient, int> _notifications;

        [Given(@"A new hosted game with (\d+) players")]
        public void GivenANewHostedGameWithPlayers(int playerCount)
        {
            var gameBinding = Binding<GameStateBindings>();
            gameBinding.GivenANewGameWithPlayers(playerCount);

            _gameHost = new LockingGameHost(gameBinding.Game);           

            _clients = new List<GameClient>();
            _notifications = new Dictionary<GameClient, int>();

            foreach(var player in gameBinding.Game.Players)
            {
                var client = new GameClient(player.Id, player.Name);
                _gameHost.RegisterGameClient(client, player);
                _clients.Add(client);
                _notifications[client] = 0;
                client.GameStateUpdates.Subscribe( _ =>
                                                       {                                                           
                                                           _notifications[client] += 1;
                                                       });
            }

            

        }

        [When(@"The game begins")]
        public void WhenTheGameBegins()
        {
            _gameHost.AcceptMessage(new BeginGameMessage());
        }

        [When(@"(.*) tells the host to buy (.*)")]
        public void WhenTellsTheHostToBuy(string playerName, string cardName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);
            var pileId = gameState.Bank.Single(p => p.Name == cardName).Id;
            var message = new BuyCardMessage(client.PlayerId, pileId);
            _gameHost.AcceptMessage(message);
        }

        [When(@"(.*) tells the host to play (.*)")]
        public void WhenTellsTheHostToPlay(string playerName, string cardName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);
            var cardId = gameState.Hand.Single(p => p.Name == cardName).Id;
            var message = new PlayCardMessage(client.PlayerId, cardId);
            _gameHost.AcceptMessage(message);
        }

        [Then(@"All players should recieve (\d+) game state update[s]?")]
        public void ThenAllPlayersShouldRecieveAGameStateUpdate(int updateCount)
        {
            foreach (var value in _notifications.Values)
                value.ShouldEqual(updateCount);
        }

        [Then(@"The host should tell (.*) to discard (\d+) cards")]
        public void ThenTheHostShouldTellPlayerToDiscardCards(string playerName, int discardCount)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.PendingActivity.Type.ShouldEqual("DiscardCards");
            gameState.PendingActivity.Message.ShouldEqual(string.Format("Select {0} cards to discard", discardCount));
            gameState.PendingActivity.Properties["NumberOfCardsToDiscard"].ShouldEqual(discardCount);
        }

        
        // Should I use this binding in the automatic progression spec?

        //[Then(@"The host should indicate that (.*) is in the buy step")]
        //public void ThenPlayerShouldBeInTheBuyStep(string playerName)
        //{
        //    var client = _clients.Single(c => c.PlayerName == playerName);
        //    var gameState = _gameHost.GetGameState(client);
        //    gameState.Status.IsActive.ShouldBeTrue();
        //    gameState.Status.InBuyStep.ShouldBeTrue();
        //}
    }
}