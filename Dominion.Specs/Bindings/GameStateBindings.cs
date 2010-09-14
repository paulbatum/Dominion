using System;
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

        public Game Game
        {
            get { return _game; }
        }

        // Given
        //

        [Given(@"A new game with (\d+) player[s]?")]
        public void GivenANewGameWithPlayers(int playerCount)
        {            
            var startingConfig = new SimpleStartingConfiguration(playerCount);
            var names = playerCount.Items(i => "Player" + i);
            _game = startingConfig.CreateGame(names);
        }


        [Given(@"(.*) has a (.*) in hand instead of a (.*)")]
        public void GivenPlayerHasACardInHandInsteadOfAnotherCard(string playerName, string cardName, string cardToReplace)
        {
            var player = _game.Players.Single(p => p.Name == playerName);

            var card = CardFactory.CreateCard(cardName);
            card.MoveTo(player.Hand);

            player.Hand
                .First(c => c.Name == cardToReplace)
                .MoveTo(new NullZone());
        }      

        [Given(@"(.*) has a hand of all (.*)")]
        public void GivenPlayerHasAHandOf(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            
            player.Hand.MoveAll(new NullZone());

            var cards = 5.Items(() => CardFactory.CreateCard(cardName)).ToList();

            foreach (var card in cards)
                card.MoveTo(player.Hand);
        }

        [Given(@"There is only (\d+) (.*) left")]
        public void GivenThereIsOnlyLeft(int cardCount, string cardName)
        {
            var pile = _game.Bank.Piles.Single(c => c.TopCard.Name == cardName);

            if (!pile.IsLimited)
                throw new InvalidOperationException("Cannot set the number of cards on an unlimited pile.");

            while (pile.CardCount > cardCount)
                pile.TopCard.MoveTo(new NullZone());
        }

        // When
        //

        [When(@"(.*) plays a (.*)")]
        public void WhenPlaysA(string playerName, string cardName)
        {
            if (_game.ActivePlayer.Name != playerName)
                throw new InvalidOperationException(string.Format("{0} cannot play a {1} because it is currently {2}'s turn.", playerName, cardName, _game.ActivePlayer.Name));

            var card = _game.ActivePlayer.Hand
                .OfType<ActionCard>()
                .First(c => c.Name == cardName);

            _game.CurrentTurn.Play(card);
        }

        [When(@"(.*) moves to the buy step")]
        public void WhenMovesToTheBuyStep(string playerName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.MoveToBuyStep();
        }

        [When(@"(.*) buys a (.*)")]
        public void WhenBuysA(string playerName, string cardName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);

            var pile = _game.Bank.Piles.Single(c => c.TopCard.Name == cardName);
            _game.CurrentTurn.Buy(pile);
        }

        [When(@"(.*) ends their turn")]
        public void WhenPlayerEndsTheirTurn(string playerName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.EndTurn();
        }

        // Then
        // 

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

        [Then(@"(.*) should have (\d+) cards in the discard pile")]
        public void ThenPlayerShouldHaveCardsInTheDiscardPile(string playerName, int cardCount)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            player.Discards.CardCount.ShouldEqual(cardCount);
        }

        [Then(@"(.*) should have (\d+) card[s]? in hand")]
        public void ThenPlayerShouldHaveCardsInHand(string playerName, int cardCount)
        {
            // HACK - Too lazy to figure out the regex to make this two separate bindings
            var players = new List<Player>();
            if (playerName == "Each player")
                players.AddRange(_game.Players);
            else
                players.Add(_game.Players.Single(p => p.Name == playerName));

            foreach(var p in players)
                p.Hand.CardCount.ShouldEqual(cardCount);
        }

        [Then(@"(.*) is the active player")]
        public void ThenPlayerIsTheActivePlayer(string playerName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
        }

        [Then(@"(.*) should have (\d+) actions remaining")]
        public void ThenPlayerShouldHaveActionsRemaining(string playerName, int actionCount)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.RemainingActions.ShouldEqual(actionCount);
        }

        [Then(@"(.*) should be in play")]
        public void ThenShouldBeInPlay(string cardName)
        {
            _game.ActivePlayer.PlayArea
                .ShouldContain(c => c.Name == cardName);
        }

        [Then(@"(.*) should have (\d+) buy[s]?")]
        public void ThenPlayerShouldHaveBuys(string playerName, int buyCount)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.Buys.ShouldEqual(buyCount);
        }

        [Then(@"(.*) should have (\d+) to spend")]
        public void ThenPlayerShouldHaveToSpend(string playerName, int moneyCount)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.MoneyToSpend.ShouldEqual(moneyCount);
        }

        [Then(@"(.*) should have (\d+) remaining action[s]?")]
        public void ThenPlayerShouldHaveRemainingActions(string playerName, int remainingActions)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.RemainingActions.ShouldEqual(remainingActions);
        }

        [Then(@"(.*) should be in the buy step")]
        public void ThenPlayerShouldBeInTheBuyStep(string playerName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.InBuyStep.ShouldBeTrue();
        }

        [Then(@"The game should have ended")]
        public void ThenTheGameShouldHaveEnded()
        {
            _game.IsComplete.ShouldBeTrue();
        }

        [Then(@"(.*) should have a (.*) on top of the discard pile")]
        public void PlayerShouldHaveCardOnTopOfDiscardPile(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);                        
            player.Discards.TopCard.Name.ShouldEqual(cardName);
        }

        #region GameLog
        [Then(@"The game log should report that (.*)'s turn has begun")]
        public void ThenTheGameLogShouldReportThatTurnHasBegun(string playerName)
        {
            _game.Log.Contents.ShouldContain(playerName + "'s turn has begun.");
        }

        [Then(@"The game log should report that (.*) bought a (.*)")]
        public void ThenTheGameLogShouldReportThatBoughtA(string playerName, string cardName)
        {
            _game.Log.Contents.ShouldContain(playerName + " bought a " + cardName);
        }

        [Then(@"The game log should report that (.*) played a (.*)")]
        public void ThenTheGameLogShouldReportThatPlayedA(string playerName, string cardName)
        {
            _game.Log.Contents.ShouldContain(playerName + " played a " + cardName);
        }

        [Then(@"The game log should report the scores")]
        public void ThenTheGameLogShouldReportTheScores()
        {
            _game.Log.Contents.ShouldContain("SCORES");
        }

        [Then(@"(.*) should be the winner")]
        public void ThenPlayerShouldBeTheWinner(string playerName)
        {
            _game.Log.Contents.ShouldContain(playerName + " is the winner");
        }

        #endregion
    }
}