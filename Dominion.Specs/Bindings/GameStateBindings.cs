using System;
using System.Linq;
using System.Collections.Generic;
using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.GameHost;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;
using NUnit.Framework;
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
        public void GivenPlayerHasAHandOfAll(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            
            player.Hand.MoveAll(new NullZone());

            var cards = 5.Items(() => CardFactory.CreateCard(cardName)).ToList();

            foreach (var card in cards)
                card.MoveTo(player.Hand);
        }

        [Given(@"(.*) has a hand of (.*), (.*), (.*), (.*), (.*)")]
        public void GivenPlayerHasAHandOfExactly(string playerName, string cardName1, string cardName2, string cardName3, string cardName4, string cardName5)
        {
            var player = _game.Players.Single(p => p.Name == playerName);

            player.Hand.MoveAll(new NullZone());

            var cards = new List<Card>();

            cards.Add(CardFactory.CreateCard(cardName1));
            cards.Add(CardFactory.CreateCard(cardName2));
            cards.Add(CardFactory.CreateCard(cardName3));
            cards.Add(CardFactory.CreateCard(cardName4));
            cards.Add(CardFactory.CreateCard(cardName5));

            foreach (var card in cards)
                card.MoveTo(player.Hand);
        }

        [Given(@"(.*) has a (.*) in the discard pile")]
        public void GivenPlayerHasACardInTheDiscardPile(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            CardFactory.CreateCard(cardName).MoveTo(player.Discards);
        }

        [Given(@"There is a (.*) in the trash pile")]
        public void GivenCardInTheTrashPile(string cardName)
        {
            CardFactory.CreateCard(cardName).MoveTo(_game.Trash);
        }

        [Given(@"(.*) has a (.*) in play")]
        public void GivenPlayerHasACardInPlay(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            CardFactory.CreateCard(cardName).MoveTo(player.PlayArea);
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

        [Given(@"There are (\d+) empty piles")]
        public void GivenThereAreEmptyPiles(int emptyPileCount)
        {
            var piles = _game.Bank.Piles.Where(x => x.IsLimited)
                .Take(emptyPileCount);

            foreach(var pile in piles)
                pile.MoveAll(new NullZone());
        }

        [Given(@"(.*) has a deck of (.*)")]
        public void GivenPlayerHasADeckOf(string playerName, string cardNames)
        {
            var player = _game.Players.Single(p => p.Name == playerName);

            var cards = cardNames.Split(',')
                .Select(s => s.Trim())
                //VS2008 says the type args can't be determined with just 
                //.Select(CardFactory.CreateCard)
                .Select(c => CardFactory.CreateCard(c))
                .ToList();

            while(player.Deck.TopCard != null)
                player.Deck.TopCard.MoveTo(new NullZone());

            foreach(Card c in cards)
                c.MoveTo(player.Deck);
        }



        // When
        //

        [When(@"(.*) plays a (.*)")]
        public void WhenPlaysA(string playerName, string cardName)
        {
            if (_game.ActivePlayer.Name != playerName)
                throw new InvalidOperationException(string.Format("{0} cannot play a {1} because it is currently {2}'s turn.", playerName, cardName, _game.ActivePlayer.Name));

            var card = _game.ActivePlayer.Hand
                .OfType<IActionCard>()
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

        [When(@"(.*) selects (\d+) (.*) to .*")]        
        public void WhenPlayerSelectsCards(string playerName, int numberOfCards, string selectedCard)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var cards = player.Hand.Where(c => c.Name == selectedCard).Take(numberOfCards);
            var activity = (ISelectCardsActivity) _game.GetPendingActivity(player);

            activity.SelectCards(cards);
        }

        [When(@"(.*) selects a (.*) to .*")]
        public void WhenPlayerSelectACard(string playerName, string selectedCard)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var cards = player.Hand.Where(c => c.Name == selectedCard).Take(1);
            var activity = (ISelectCardsActivity) _game.GetPendingActivity(player);

            activity.SelectCards(cards);
        }

        [When(@"(.*) chooses (.*)")]
        public void WhenPlayerChooses(string playerName, string choice)
        {            
            var player = _game.Players.Single(p => p.Name == playerName);
            var pendingActivity = _game.GetPendingActivity(player);
            var activity = (ChoiceActivity)pendingActivity;
            activity.MakeChoice(choice);
        }

        [When(@"(.*) gains a (.*)")]
        public void WhenPlayerGainsACard(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var pile = _game.Bank.Piles.First(p => p.Name == cardName);
            var activity = (GainACardActivity) _game.GetPendingActivity(player);

            activity.SelectPileToGainFrom(pile);
        }

        [When(@"(.*) attempts to gain a (.*)")]
        public void WhenPlayerAttemptsToGainACard(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var pile = _game.Bank.Piles.First(p => p.Name == cardName);
            var activity = (GainACardActivity) _game.GetPendingActivity(player);

            try
            {
                activity.SelectPileToGainFrom(pile);
            }
            catch (Exception)
            {
                //this may fail
            }
        }

        [When(@"(.*) reveals (.*)")]
        public void WhenPlayerRevealsCard(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = (SelectReactionActivity)_game.GetPendingActivity(player);
            var cards = player.Hand.Where(c => c.Name == cardName).Take(1);

            activity.SelectCards(cards);
        }

        [When(@"(.*) selects nothing to discard")]
        [When(@"(.*) is done with reactions")]
        public void WhenPlayerSelectsNothingToDiscard(string playerName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);

            var activity = (ISelectCardsActivity) _game.GetPendingActivity(player);
            activity.SelectCards(Enumerable.Empty<Card>());
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

        [Then(@"(.*) should be in the action step")]
        public void ThenPlayerShouldBeInTheActionStep(string playerName)
        {
            _game.ActivePlayer.Name.ShouldEqual(playerName);
            _game.CurrentTurn.InBuyStep.ShouldBeFalse();
        }

        [Then(@"The game should have ended")]
        public void ThenTheGameShouldHaveEnded()
        {
            _game.IsComplete.ShouldBeTrue();
        }


        [Then(@"The game should not have ended")]
        public void ThenTheGameShouldNotHaveEnded()
        {
            _game.IsComplete.ShouldBeFalse();
        }

        [Then(@"(.*) should have a (.*) on top of the discard pile")]
        public void PlayerShouldHaveCardOnTopOfDiscardPile(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);                        
            player.Discards.TopCard.Name.ShouldEqual(cardName);
        }

        [Then(@"There should be a (.*) on top of the trash pile")]
        public void PlayerShouldHaveCardOnTopOfTrashPile(string cardName)
        {
            _game.Trash.TopCard.Name.ShouldEqual(cardName);
        }
        
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

        [Then(@"(.*) must select (\d+) card[s]? to .*")]
        public void ThenPlayerMustSelectCards(string playerName, int numberOfCards)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = (SelectCardsFromHandActivity) _game.GetPendingActivity(player);

            activity.Count.ShouldEqual(numberOfCards);
        }

        [Then(@"All actions should be resolved")]
        public void ThenAllActionsShouldBeResolved()
        {
            _game.CurrentTurn.GetCurrentEffect().ShouldBeNull();
        }

        [Then(@"(.*) should have a deck of (\d+) card[s]?")]
        public void ThenPlayerShouldHaveADeckOf5Cards(string playerName, int cardCount)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            player.Deck.CardCount.ShouldEqual(cardCount);
        }

        
        [Then(@"(.*) must choose from (.*)")]
        public void ThenPlayerMustChooseFromOptions(string playerName, string options)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = _game.GetPendingActivity(player);

            activity.ShouldBeOfType<ChoiceActivity>();

            CollectionAssert.AreEquivalent(
                options.Split(',').Select(x => x.Trim()), 
                (IEnumerable<string>) activity.Properties["AllowedOptions"]);
        }

        [Then(@"(.*) must select (\d+) action card[s]?")]
        public void ThenPlayerMustSelectActionCard(string playerName, int cardCount)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectCardsActivity) _game.GetPendingActivity(player);

            activity.Properties["NumberOfCardsToSelect"].ShouldEqual(cardCount);
            activity.Properties["CardsMustBeOfType"].ShouldEqual(typeof (IActionCard).Name);
        }

        [Then(@"(.*) must select (\d+) treasure (\(only\) )?card[s]? to .*")]
        public void ThenPlayerMustSelectTreasureCard(string playerName, int cardCount, string only)
        {
            bool onlyTreasureIsAllowed = !string.IsNullOrEmpty(only);

            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = _game.GetPendingActivity(player) as SelectCardsFromHandActivity;

            activity.Count.ShouldEqual(cardCount);
            activity.Restrictions.ShouldContain(RestrictionType.TreasureCard);

            if (onlyTreasureIsAllowed)
                activity.Restrictions.Count.ShouldEqual(1);
        }

        [Then(@"(.*) must gain a card of cost (\d+) or less")]
        public void ThenPlayerMustGainACardOfCostOrLess(string playerName, int cardCost)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = _game.GetPendingActivity(player) as GainACardUpToActivity;

            activity.UpToCost.ShouldEqual(cardCost);
        }

        [Then(@"(.*) must wait")]
        public void ThenPlayerMustWait(string playerName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = _game.GetPendingActivity(player);

            activity.ShouldBeOfType<WaitingForPlayersActivity>();
        }

        [Then(@"(.*) may reveal a reaction")]
        public void ThenPlayerMayRevealAReaction(string playerName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = _game.GetPendingActivity(player);

            activity.ShouldBeOfType<SelectReactionActivity>();
        }

        [Then(@"(.*) may select up to (\d+) cards from their hand")]
        public void ThenPlayerMustSelectAnyNumberOfCardsFromTheirHand(string playerName, int cardCount)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectCardsActivity) _game.GetPendingActivity(player);

            activity.Properties["NumberOfCardsToSelect"].ShouldEqual(cardCount);
            activity.Type.ShouldEqual(ActivityType.SelectUpToNumberOfCards);
        }

        [Then(@"(.*) should have a hand of (.*)")]
        public void ThenPlayerShouldHaveAHandOfExactly(string playerName, string cardNames)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            player.Hand.ToString().ShouldEqual(cardNames);            
        }

        [Then(@"(.*) should have a deck of: (.*)")]
        public void ThenPlayerShouldHaveADeckOf(string playerName, string cardNames)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            var deckContents = string.Join(", ", player.Deck.Contents.Select(c => c.Name).ToArray());

            deckContents.ShouldEqual(cardNames);
        }




    }
}