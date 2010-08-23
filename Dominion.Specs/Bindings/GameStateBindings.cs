using System.Linq;
using System.Collections.Generic;
using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.GameHost;
using Dominion.Rules;
using TechTalk.SpecFlow;

namespace Dominion.Specs.Bindings
{
    [Binding]
    public class GameStateBindings : BindingBase
    {
        private Game _game;

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






        [Given(@"A game is in progress")]
        public void GivenAGameIsInProgress()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have 4 cards in hand")]
        public void GivenIHave4CardsInHand()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"My deck is not empty")]
        public void GivenMyDeckIsNotEmpty()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I draw a card")]
        public void WhenIDrawACard()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I should have (.*) cards in hand")]
        public void ThenIShouldHaveCardsInHand(int numberOfCards)
        {
            ScenarioContext.Current.Pending();
        }
    }
}