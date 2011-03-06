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

        [When(@"(.*) tells the host to move to the buy step")]
        public void WhenPlayer1TellTheHostToMoveToTheBuyStep(string playerName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var message = new MoveToBuyStepMessage(client.PlayerId);
            _gameHost.AcceptMessage(message);
        }

        [When(@"(.*) tells the host to reveal (.*)")]
        [When(@"(.*) tells the host to pass (.*)")]
        [When(@"(.*) tells the host to put (.*) on top")]
        [When(@"(.*) tells the host to select (.*)")]
        public void WhenPlayerTellsTheHostToSelect(string playerName, string cardName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);
            var cardId = gameState.Hand.First(p => p.Name == cardName).Id;
            var message = new SelectCardsMessage(client.PlayerId, new[] { cardId });
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

            gameState.PendingActivity.Type.ShouldEqual("SelectFixedNumberOfCards");
            gameState.PendingActivity.Message.ShouldEqual(string.Format("Select {0} card(s) to discard.", discardCount));
            gameState.PendingActivity.Properties["NumberOfCardsToSelect"].ShouldEqual(discardCount);
        }

        [Then(@"(.*)'s view includes a (.*) in hand with types (.*) and (.*)")]
        public void ThenPlayerViewIncludesCardWithTypes(string playerName, string cardName, string type1, string type2)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            var card = gameState.Hand.First(x => x.Name == cardName);
            card.Types.ShouldContain(type1);
            card.Types.ShouldContain(type2);
        }
        
        [Then(@"(.*)'s view includes a (.*) in hand with the type (.*)")]
        public void ThenPlayerViewIncludesCardWithType(string playerName, string cardName, string type)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            var card = gameState.Hand.First(x => x.Name == cardName);
            card.Types.Single().ShouldEqual(type);            
        }

        [Then(@"(.*)'s view includes a (.*) in the bank that can be bought")]
        [Then(@"(.*)'s view includes a (.*) in the bank that can be gained")]        
        public void ThenPlayerViewIncludesACardInTheBankThatCanBeBought(string playerName, string cardName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            var card = gameState.Bank.First(x => x.Name == cardName);
            card.CanBuy.ShouldBeTrue();
        }

        [Then(@"(.*)'s view includes nothing in the bank that can be bought")]
        public void ThenPlayersViewIncludesNothingInTheBankThatCanBeBought(string playerName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.Bank.Any(b => b.CanBuy).ShouldBeFalse();            
        }

        [Then(@"(.*)'s view includes nothing in hand that can be played")]
        public void ThenPlayersViewIncludesNothingInHandThatCanBePlayed(string playerName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.Hand.Any(b => b.CanPlay).ShouldBeFalse();         
        }

        [Then(@"(.*)'s view includes a (.*) in hand that can be played")]
        public void ThenPlayerViewIncludesACardInHandThatCanBePlayed(string playerName, string cardName)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.Hand.Single(c => c.Name == cardName)
                .CanPlay.ShouldBeTrue();            
        }

        [Then(@"(.*)'s view of the play area should start with this sequence of cards: (.*)")]
        public void ThenPlayerViewOfThePlayAreaShouldStartWithThisSequenceOfCards(string playerName, string sequence)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);
            
            string playAreaCardNames = string.Join(", ", gameState.InPlay.Select(c => c.Name).ToArray());
            playAreaCardNames.ShouldStartWith(sequence);
        }

        [Then(@"(.*)'s view includes (.*) in the revealed zone")]
        public void ThenPlayerViewIncludesInTheRevealedZone(string playerName, string cards)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.Revealed.GetCardNames().ShouldEqual(cards);            
        }

        [Then(@"(.*)'s current activity should have a type of (.*)")]
        public void ThenPlayersCurrentActivityHasATypeOf(string playerName, string activityType)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.PendingActivity.Type.ShouldEqual(activityType);
        }

        [Then(@"(.*)'s current activity should have a hint of (.*)")]
        public void ThenPlayersCurrentActivityHasAHintOf(string playerName, string hint)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.PendingActivity.Hint.ShouldEqual(hint);
        }


        [Then(@"(.*)'s current activity should have a source of (.*)")]
        public void ThenPlayersCurrentActivityHasASourceOf(string playerName, string source)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.PendingActivity.Source.ShouldEqual(source);
        }

        [Then(@"(.*)'s current activity should have a type restriction of (.*)")]
        public void ThenPlayersCurrentActivityHasATypeRestrictionOf(string playerName, string type)
        {
            var client = _clients.Single(c => c.PlayerName == playerName);
            var gameState = _gameHost.GetGameState(client);

            gameState.PendingActivity.GetTypeRestrictionProperty().ShouldEqual(type);
        }
    }
}