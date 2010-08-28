using System.Linq;
using System.Collections.Generic;
using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.GameHost;
using Dominion.Rules;
using Dominion.Rules.CardTypes;
using TechTalk.SpecFlow;

namespace Dominion.Specs.Bindings
{
    [Binding]
    public class GameStateBindings : BindingBase
    {
        private Game _game;
        private Player _player;

        [Given(@"A new game with (\d+) players")]
        public void GivenANewGameWithPlayers(int playerCount)
        {
            var startingConfig = new SimpleStartingConfiguration(playerCount);
            var names = playerCount.Items(i => "Player" + i);
            _game = startingConfig.CreateGame(names);
        }

        //[Then(@"(.*) should have (\d+) cards in hand")]
        //public void ThenPlayerShouldHaveCardsInHand(string playerName, int cardsInHand)
        //{
        //    var player = _game.Players.Where(x => x.Name == playerName).Single();
        //    player.Hand.CardCount.ShouldEqual(cardsInHand);
        //}

        [Then(@"Each player should have (\d+) cards in hand")]
        public void ThenPlayerShouldHaveCardsInHand(int cardsInHand)
        {
            foreach (var player in _game.Players)
                player.Hand.CardCount.ShouldEqual(cardsInHand);
        }

        [Then(@"The initial deck for each player should comprise of (\d+) Copper and (\d+) Estate")]
        public void ThenTheInitialDeckForEachPlayerShouldCompriseOfCopperAndEstate(int copperCount, int estateCount)
        {
            foreach (var player in _game.Players)
            {
                var coppers = player.Deck.Contents.OfType<Copper>().Count() + player.Hand.OfType<Copper>().Count();
                var estates = player.Deck.Contents.OfType<Estate>().Count() + player.Hand.OfType<Estate>().Count();

                coppers.ShouldEqual(copperCount);
                estates.ShouldEqual(estateCount);
            }
        }

        [Then(@"There should be (\d+) (.*) available to buy")]
        public void ThenThereShouldBeAvailableToBuy(int cardCount, string cardName)
        {
            _game.Bank.Piles.Single(x => x.TopCard.Name == cardName).CardCount.ShouldEqual(cardCount);
        }

        [Given(@"It is my turn")]
        public void GivenItIsMyTurn()
        {
            _player = _game.CurrentTurn.ActivePlayer;
        }

        [When(@"I end my turn")]
        public void WhenIEndMyTurn()
        {
            _game.CurrentTurn.EndTurn();
        }

        [Then(@"I should have (\d+) cards in the discard pile")]
        public void ThenIShouldHave5CardsInTheDiscardPile(int cardCount)
        {
            _player.Discards.CardCount.ShouldEqual(cardCount);
        }

        [Then(@"I should have (\d+) cards in hand")]
        public void ThenIShouldHave5CardsInHand(int cardCount)
        {
            _player.Hand.CardCount.ShouldEqual(cardCount);
        }

        [Then(@"(.*) is the active player")]
        public void ThenIsTheActivePlayer(string playerName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
        }

        [When(@"(.*) ends their turn")]
        public void WhenPlayerEndsTheirTurn(string playerName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.EndTurn();
        }

        [Given(@"I have a (.*) in hand instead of a (.*)")]
        public void GivenIHaveAInHandInsteadOfA(string cardName, string cardToReplace)
        {
            var card = CardFactory.CreateCard(cardName);
            card.MoveTo(_player.Hand);

            _player.Hand
                .First(c => c.Name == cardToReplace)
                .MoveTo(new NullZone());
        }

        //[Given(@"(\d+) action[s]? remaining")]
        //public void GivenActionRemaining(int actionCount)
        //{
        //    _game.CurrentTurn.RemainingActions = actionCount;            
        //}

        [When(@"I play (.*)")]
        public void WhenIPlay(string cardName)
        {
            var card = _player.Hand
                .OfType<ActionCard>()
                .First(c => c.Name == cardName);

            _game.CurrentTurn.Play(card);
        }

        [Then(@"I should have (\d+) actions remaining")]
        public void ThenIShouldHaveActionsRemaining(int actionCount)
        {
            _game.CurrentTurn.RemainingActions.ShouldEqual(actionCount);
        }

        [Then(@"(.*) should be in play")]
        public void ThenShouldBeInPlay(string cardName)
        {
            _game.ActivePlayer.PlayArea
                .SingleOrDefault(c => c.Name == cardName)
                .ShouldNotBeNull();
        }
    }
}