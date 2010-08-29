using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameHost;
using TechTalk.SpecFlow;

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
            _gameHost.AcceptMessage(new BeginGameMessage());

            _clients = new List<GameClient>();
            _notifications = new Dictionary<GameClient, int>();

            foreach(var player in gameBinding.Game.Players)
            {
                var client = new GameClient {PlayerName = player.Name};
                _gameHost.RegisterGameClient(client, player);
                _clients.Add(client);
                _notifications[client] = 0;
                client.GameStateUpdates.Subscribe( _ => _notifications[client] += 1 );
            }

            

        }

        [When(@"(.*) tells the host to buy (.*)")]
        public void WhenPlayer1TellsTheHostToBuy(string playerName, string cardName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);
            var pileId = gameState.Bank.Single(p => p.Name == cardName).Id;
            var message = new BuyCardMessage {PileId = pileId};
            _gameHost.AcceptMessage(message);
        }

        [Then(@"All players should recieve a game state update")]
        public void ThenAllPlayersShouldRecieveAGameStateUpdate()
        {
            _notifications.Values
                .All(x => x == 1)
                .ShouldBeTrue();
        }
    }
}