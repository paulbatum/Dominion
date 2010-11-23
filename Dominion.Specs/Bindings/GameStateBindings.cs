using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Dominion.Cards;
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
            get
            {
                _game.CurrentTurn.ResolvePendingEffects();
                return _game;
            }
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

        [Given(@"A new game with (\d+) player[s]? and bank of (.*)")]
        public void GivenANewGameWithPlayersAndBank(int playerCount, string bankCardNames)
        {
            var cardNameList = bankCardNames.Split(new char[]{',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            var startingConfig = new ChosenStartingConfiguration(playerCount, cardNameList, false);
            var names = playerCount.Items(i => "Player" + i);
            _game = startingConfig.CreateGame(names);
        }

        [Given(@"(.*) has a (.*) in hand instead of a (.*)")]
        public void GivenPlayerHasACardInHandInsteadOfAnotherCard(string playerName, string cardName, string cardToReplace)
        {
            var player = Game.Players.Single(p => p.Name == playerName);

            var card = CardFactory.CreateCard(cardName);
            card.MoveTo(player.Hand);

            player.Hand
                .First(c => c.Name == cardToReplace)
                .MoveTo(new NullZone());
        }      

        [Given(@"(.*) has (\d+) (.*) in hand")]
        public void GivenPlayerHasAHandOfNumberCard(string playerName, int cardCount, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            
            player.Hand.MoveAll(new NullZone());

            var cards = cardCount.Items(() => CardFactory.CreateCard(cardName)).ToList();

            foreach (var card in cards)
                card.MoveTo(player.Hand);
        }

        [Given(@"(.*) has a hand of (.*)")]
        public void GivenPlayerHasAHandOfExactly(string playerName, string cardNames)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            player.Hand.MoveAll(new NullZone());

            var cards = cardNames.Split(',')
                .Select(s => s.Trim());

            foreach (var card in cards)
                CardFactory.CreateCard(card).MoveTo(player.Hand);
        }

        [Given(@"(.*) has a (.*) in the discard pile")]
        public void GivenPlayerHasACardInTheDiscardPile(string playerName, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            CardFactory.CreateCard(cardName).MoveTo(player.Discards);
        }

        [Given(@"There is a (.*) in the trash pile")]
        public void GivenCardInTheTrashPile(string cardName)
        {
            CardFactory.CreateCard(cardName).MoveTo(Game.Trash);
        }

        [Given(@"(.*) has a (.*) in play")]
        public void GivenPlayerHasACardInPlay(string playerName, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            CardFactory.CreateCard(cardName).MoveTo(player.PlayArea);
        }

        [Given(@"There is only (\d+) (.*) left")]
        public void GivenThereIsOnlyLeft(int cardCount, string cardName)
        {
            var pile = Game.Bank.Piles.Single(c => c.TopCard.Name == cardName);

            if (!pile.IsLimited)
                throw new InvalidOperationException("Cannot set the number of cards on an unlimited pile.");

            while (pile.CardCount > cardCount)
                pile.TopCard.MoveTo(new NullZone());
        }

        [Given(@"There are (\d+) empty piles")]
        public void GivenThereAreEmptyPiles(int emptyPileCount)
        {
            var piles = Game.Bank.Piles.Where(x => x.IsLimited)
                .Take(emptyPileCount);

            foreach(var pile in piles)
                pile.MoveAll(new NullZone());
        }

        [Given(@"(.*) has a deck of (.*)")]
        public void GivenPlayerHasADeckOf(string playerName, string cardNames)
        {
            var player = Game.Players.Single(p => p.Name == playerName);

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

        [Given(@"(.*) has an empty deck")]
        public void GivenPlayerHasAnEmptyDeck(string playerName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            
            player.Deck.MoveAll(new NullZone());
        }


        [Given(@"(.*) is available to buy")]
        public void GivenCardIsAvailableToBuy(string cardName)
        {
            Game.Bank.AddCardPile(new LimitedSupplyCardPile().WithNewCards(cardName, 10));
        }



        // When
        //

        [When(@"(.*) plays a (.*)")]
        public void WhenPlaysA(string playerName, string cardName)
        {
            if (Game.ActivePlayer.Name != playerName)
                throw new InvalidOperationException(string.Format("{0} cannot play a {1} because it is currently {2}'s turn.", playerName, cardName, Game.ActivePlayer.Name));

            var card = Game.ActivePlayer.Hand
                .OfType<IActionCard>()
                .First(c => c.Name == cardName);

            Game.CurrentTurn.Play(card);
        }

        [When(@"(.*) moves to the buy step")]
        public void WhenMovesToTheBuyStep(string playerName)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.CurrentTurn.MoveToBuyStep();
        }

        [When(@"(.*) buys a (.*)")]
        public void WhenBuysA(string playerName, string cardName)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);

            var pile = Game.Bank.Piles.Single(c => c.TopCard.Name == cardName);
            Game.CurrentTurn.Buy(pile);
        }

        [When(@"(.*) ends their turn")]
        public void WhenPlayerEndsTheirTurn(string playerName)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.EndTurn();
        }

        [When(@"(.*) selects (\d+) (.*) to .*")]        
        public void WhenPlayerSelectsCards(string playerName, int numberOfCards, string selectedCard)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var cards = player.Hand.Where(c => c.Name == selectedCard).Take(numberOfCards);
            var activity = (ISelectCardsActivity) Game.GetPendingActivity(player);

            activity.SelectCards(cards);
        }

        [When(@"(.*) selects a (.*) to .*")]
        public void WhenPlayerSelectACard(string playerName, string selectedCard)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var cards = player.Hand.Where(c => c.Name == selectedCard).Take(1);
            var activity = (ISelectCardsActivity) Game.GetPendingActivity(player);

            activity.SelectCards(cards);
        }

        [When(@"(.*) selects cards (.*) to .*")]
        public void WhenPlayerSelectsCards(string playerName, string selectedCardList)
        {
            var cardNames = selectedCardList.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var player = Game.Players.Single(p => p.Name == playerName);
            var cardList = new List<ICard>();
            foreach (var name in cardNames)
            {
                var card = player.Hand.Where(c => c.Name == name && !cardList.Contains(c))
                    .Take(1).Single();
                cardList.Add(card);
            }
            var activity = (ISelectCardsActivity)Game.GetPendingActivity(player);

            activity.SelectCards(cardList);
        }

        //[When(@"(.*) chooses (.*)")]
        //public void WhenPlayerChooses(string playerName, string choice)
        //{            
        //    var player = _game.Players.Single(p => p.Name == playerName);
        //    var pendingActivity = _game.GetPendingActivity(player);
        //    var activity = (ChoiceActivity)pendingActivity;
        //    activity.MakeChoice(choice);
        //}

        [When(@"(.*) chooses to .* \((.*)\)")]
        public void WhenPlayerChooses(string playerName, string choice)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var pendingActivity = Game.GetPendingActivity(player);
            var activity = (ChoiceActivity)pendingActivity;
            activity.MakeChoice(choice);
        }


        [When(@"(.*) gains a (.*)")]
        public void WhenPlayerGainsACard(string playerName, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var pile = Game.Bank.Piles.First(p => p.Name == cardName);
            var activity = (ISelectPileActivity) Game.GetPendingActivity(player);

            activity.SelectPile(pile);
        }

        

        [When(@"(.*) reveals (.*)")]
        public void WhenPlayerRevealsCard(string playerName, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (SelectReactionActivity)Game.GetPendingActivity(player);
            var cards = player.Hand.Where(c => c.Name == cardName).Take(1);

            activity.SelectCards(cards);
        }

        [When(@"(.*) selects nothing to discard")]
        [When(@"(.*) selects nothing to trash")]
        [When(@"(.*) is done with reactions")]
        public void WhenPlayerSelectsNothingToDiscard(string playerName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);

            var activity = (ISelectCardsActivity) Game.GetPendingActivity(player);
            activity.SelectCards(Enumerable.Empty<ICard>());
        }

        [When(@"(.*) selects (.*) from the revealed cards")]
        public void WhenPlayerSelectsFromTheRevealedCards(string playerName, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);

            var activity = (ISelectFromRevealedCardsActivity)Game.GetPendingActivity(player);
            var card = activity.RevealedCards.First(c => c.Name == cardName);
            activity.SelectCards(new[] { card });            
        }

       



        // Then
        // 

        [Then(@"The initial deck for each player should comprise of (\d+) Copper and (\d+) Estate")]
        public void ThenTheInitialDeckForEachPlayerShouldCompriseOfCopperAndEstate(int copperCount, int estateCount)
        {
            foreach (var player in Game.Players)
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
            Game.Bank.Piles.Single(x => x.TopCard.Name == cardName).CardCount.ShouldEqual(cardCount);
        }        

        [Then(@"(.*) should have (\d+) card[s]? in the discard pile")]
        public void ThenPlayerShouldHaveCardsInTheDiscardPile(string playerName, int cardCount)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            player.Discards.CardCount.ShouldEqual(cardCount);
        }

        [Then(@"(.*) should have (\d+) card[s]? in hand")]
        public void ThenPlayerShouldHaveCardsInHand(string playerName, int cardCount)
        {
            // HACK - Too lazy to figure out the regex to make this two separate bindings
            var players = new List<Player>();
            if (playerName == "Each player")
                players.AddRange(Game.Players);
            else
                players.Add(Game.Players.Single(p => p.Name == playerName));

            foreach(var p in players)
                p.Hand.CardCount.ShouldEqual(cardCount);
        }

        [Then(@"(.*) is the active player")]
        public void ThenPlayerIsTheActivePlayer(string playerName)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
        }

        [Then(@"(.*) should have (\d+) action[s]? remaining")]
        public void ThenPlayerShouldHaveActionsRemaining(string playerName, int actionCount)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.CurrentTurn.RemainingActions.ShouldEqual(actionCount);
        }

        [Then(@"(.*) should be in play")]
        public void ThenShouldBeInPlay(string cardName)
        {
            Game.ActivePlayer.PlayArea
                .ShouldContain(c => c.Name == cardName);
        }

        [Then(@"(.*) should have in play: (.*)")]
        public void ThenShouldBeInPlay(string playerName, string cardsInPlay)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.ActivePlayer.PlayArea.ToString().ShouldEqual(cardsInPlay);                
        }

        [Then(@"(.*) should have (\d+) buy[s]?")]
        public void ThenPlayerShouldHaveBuys(string playerName, int buyCount)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.CurrentTurn.Buys.ShouldEqual(buyCount);
        }

        [Then(@"(.*) should have (\d+) to spend")]
        public void ThenPlayerShouldHaveToSpend(string playerName, string spend)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            CardCost actualSpend = CardCost.Parse(spend);
            Game.CurrentTurn.AvailableSpend.ShouldEqual(actualSpend);
        }

        [Then(@"(.*) should have (\d+) remaining action[s]?")]
        public void ThenPlayerShouldHaveRemainingActions(string playerName, int remainingActions)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.CurrentTurn.RemainingActions.ShouldEqual(remainingActions);
        }

        [Then(@"(.*) should be in the buy step")]
        public void ThenPlayerShouldBeInTheBuyStep(string playerName)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.CurrentTurn.InBuyStep.ShouldBeTrue();
        }

        [Then(@"(.*) should be in the action step")]
        public void ThenPlayerShouldBeInTheActionStep(string playerName)
        {
            Game.ActivePlayer.Name.ShouldEqual(playerName);
            Game.CurrentTurn.InBuyStep.ShouldBeFalse();
        }

        [Then(@"The game should have ended")]
        public void ThenTheGameShouldHaveEnded()
        {
            Game.IsComplete.ShouldBeTrue();
        }


        [Then(@"The game should not have ended")]
        public void ThenTheGameShouldNotHaveEnded()
        {
            Game.IsComplete.ShouldBeFalse();
        }

        [Then(@"(.*) should have a (.*) on top of the discard pile")]
        public void PlayerShouldHaveCardOnTopOfDiscardPile(string playerName, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);                 
            player.Discards.TopCard.Name.ShouldEqual(cardName);
        }

        [Then(@"(.*) should not have a (.*) on top of the discard pile")]
        public void PlayerShouldNotHaveCardOnTopOfDiscardPile(string playerName, string cardName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            if(player.Discards.CardCount > 0)
                player.Discards.TopCard.Name.ShouldNotEqual(cardName);
        }
                
        [Then(@"(.*) should have a discard pile of (.*)")]
        public void PlayerShouldHaveADiscardPileOf(string playerName, string cards)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            player.Discards.ToString().ShouldEqual(cards);
        }

        [Then(@"There should be a (.*) on top of the trash pile")]
        public void ThenThereShouldBeACardOnTopOfTrashPile(string cardName)
        {
            Game.Trash.TopCard.Name.ShouldEqual(cardName);
        }

        [Then(@"The trash pile should be (.*)")]
        public void ThenTheTrashPileShouldBe(string trashPileContents)
        {
            Game.Trash.ToString().ShouldEqual(trashPileContents);
        }
        
        [Then(@"The game log should report that (.*)'s turn has begun")]
        public void ThenTheGameLogShouldReportThatTurnHasBegun(string playerName)
        {
            Game.Log.Contents.ShouldContain(playerName + "'s turn has begun.");
        }

        [Then(@"The game log should report that (.*) bought a (.*)")]
        public void ThenTheGameLogShouldReportThatBoughtA(string playerName, string cardName)
        {
            Game.Log.Contents.ShouldContain(playerName + " bought a " + cardName);
        }

        [Then(@"The game log should report that (.*) played a (.*)")]
        public void ThenTheGameLogShouldReportThatPlayedA(string playerName, string cardName)
        {
            Game.Log.Contents.ShouldContain(playerName + " played a " + cardName);
        }

        [Then(@"The game log should report the scores")]
        public void ThenTheGameLogShouldReportTheScores()
        {
            Game.Log.Contents.ShouldContain("SCORES");
        }

        [Then(@"(.*) should be the winner")]
        public void ThenPlayerShouldBeTheWinner(string playerName)
        {
            Game.Log.Contents.ShouldContain(playerName + " is the winner");
        }

        [Then(@"(.*) must select a card to .*")]
        public void ThenPlayerMustSelectACard(string playerName)
        {
            ThenPlayerMustSelectCards(playerName, 1);
        }

        [Then(@"(.*) must select (\d+) card[s]? to .*")]
        public void ThenPlayerMustSelectCards(string playerName, int numberOfCards)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectCardsActivity) Game.GetPendingActivity(player);

            activity.Type.ShouldEqual(ActivityType.SelectFixedNumberOfCards);
            activity.GetCountProperty().ShouldEqual(numberOfCards);
        }

        [Then(@"All actions should be resolved")]
        public void ThenAllActionsShouldBeResolved()
        {
            Game.CurrentTurn.GetCurrentEffect().ShouldBeNull();
        }

        [Then(@"(.*) should have a deck of (\d+) card[s]?")]
        public void ThenPlayerShouldHaveADeckOf5Cards(string playerName, int cardCount)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            player.Deck.CardCount.ShouldEqual(cardCount);
        }

        
        [Then(@"(.*) must choose from (.*)")]
        public void ThenPlayerMustChooseFromOptions(string playerName, string options)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = Game.GetPendingActivity(player);

            activity.ShouldBeOfType<ChoiceActivity>();

            CollectionAssert.AreEquivalent(
                options.Split(',').Select(x => x.Trim()), 
                (IEnumerable<string>) activity.Properties["AllowedOptions"]);
        }

        [Then(@"(.*) must choose whether to .* \((.*) or (.*)\)")]
        public void ThenPlayerMustChooseFromYesNo(string playerName, string option1, string option2)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = Game.GetPendingActivity(player);

            activity.ShouldBeOfType<ChoiceActivity>();
            CollectionAssert.AreEquivalent(
                new[] { option1, option2 },
                (IEnumerable) activity.Properties["AllowedOptions"]);
        }        

        [Then(@"(.*) must select (\d+) action card[s]?")]
        public void ThenPlayerMustSelectActionCard(string playerName, int cardCount)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectCardsActivity) Game.GetPendingActivity(player);

            activity.GetCountProperty().ShouldEqual(cardCount);
            activity.GetTypeRestrictionProperty().ShouldEqual(typeof (IActionCard).Name);
        }

        [Then(@"(.*) must select (\d+) treasure card[s]? to .*")]
        public void ThenPlayerMustSelectTreasureCard(string playerName, int cardCount)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectCardsActivity) Game.GetPendingActivity(player);

            activity.GetCountProperty().ShouldEqual(cardCount);
            activity.GetTypeRestrictionProperty().ShouldEqual(typeof(ITreasureCard).Name);
        }

        [Then(@"(.*) must gain a card of cost (.*) or less")]
        public void ThenPlayerMustGainACardOfCostOrLess(string playerName, string cost)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectPileActivity) Game.GetPendingActivity(player);
            CardCost cardCost = CardCost.Parse(cost);

            activity.GetCostProperty().ShouldEqual(cardCost);
        }

        [Then(@"(.*) must gain a card of exact cost (.*)")]
        public void ThenPlayerMustGainACardOfExactCost(string playerName, string costString)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var cost = CardCost.Parse(costString);
            var activity = (ISelectPileActivity) Game.GetPendingActivity(player);

            activity.GetCostProperty().ShouldEqual(cost);

            var matchingPile = new TestCardPile(cost);
            activity.Specification.IsMatch(matchingPile).ShouldBeTrue();

            var lowMismatchPile = new TestCardPile(cost - 1);
            activity.Specification.IsMatch(lowMismatchPile).ShouldBeFalse();
        }

        [Then(@"(.*) must gain a treasure card of cost (.*) or less")]
        public void ThenPlayerMustGainATreasureCardOfCostOrLess(string playerName, string cost)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectPileActivity)Game.GetPendingActivity(player);
            CardCost cardCost = CardCost.Parse(cost);

            activity.GetCostProperty().ShouldEqual(cardCost);
            activity.GetTypeRestrictionProperty().ShouldEqual(typeof (ITreasureCard).Name);
        }

        [Then(@"(.*) must wait")]
        public void ThenPlayerMustWait(string playerName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = Game.GetPendingActivity(player);

            activity.ShouldBeOfType<WaitingForPlayersActivity>();
        }

        [Then(@"(.*) may reveal a reaction")]
        public void ThenPlayerMayRevealAReaction(string playerName)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = Game.GetPendingActivity(player);

            activity.ShouldBeOfType<SelectReactionActivity>();
        }

        [Then(@"(.*) may select up to (\d+) card[s]? from their hand")]
        [Then(@"(.*) may select up to (\d+) card[s]? from their hand to .*")]
        public void ThenPlayerMustSelectAnyNumberOfCardsFromTheirHand(string playerName, int cardCount)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectCardsActivity) Game.GetPendingActivity(player);

            activity.GetCountProperty().ShouldEqual(cardCount);
            activity.Type.ShouldEqual(ActivityType.SelectUpToNumberOfCards);
        }

        [Then(@"(.*) should have a hand of (.*)")]
        public void ThenPlayerShouldHaveAHandOfExactly(string playerName, string cardNames)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            player.Hand.ToString().ShouldEqual(cardNames);            
        }

        [Then(@"(.*) should have a deck of: (.*)")]
        public void ThenPlayerShouldHaveADeckOf(string playerName, string cardNames)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var deckContents = string.Join(", ", player.Deck.Contents.Select(c => c.Name).ToArray());

            deckContents.ShouldEqual(cardNames);
        }

        [Then(@"(.*) must select a revealed card from: (.*)")]
        public void ThenPlayerMustSelectARevealedCardFrom(string playerName, string cards)
        {
            var player = Game.Players.Single(p => p.Name == playerName);
            var activity = (ISelectFromRevealedCardsActivity) Game.GetPendingActivity(player);
            activity.RevealedCards.ToString().ShouldEqual(cards);
            activity.Type.ShouldEqual(ActivityType.SelectFromRevealed);                        
            activity.GetCountProperty().ShouldEqual(1);
        }

        [Then(@"(.*) should have (\d+) card[s]? in deck")]
        public void ThenPlayer1ShouldHaveXCardsInDeck(string playerName, int cardCount)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            player.Deck.CardCount.ShouldEqual(cardCount);
        }

        [Then(@"(.*) should have a (.*) on top of the deck")]
        public void ThenPlayerShouldHaveACardOnTopOfTheDeck(string playerName, string cardName)
        {
            var player = _game.Players.Single(p => p.Name == playerName);
            player.Deck.TopCard.Name.ShouldEqual(cardName);
        }



    }
}