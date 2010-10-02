using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules.Activities
{
    public static class Activities
    {
        public static ISelectCardsActivity DiscardCards(TurnContext context, Player player, int numberToDiscard)
        {
            return new SelectCardsActivity(
                context.Game.Log, player, string.Format("Select {0} card(s) to discard", numberToDiscard),
                SelectionSpecifications.SelectExactlyXCards(numberToDiscard))
            {
                AfterCardsSelected = cards => context.DiscardCards(player, cards)
            };
        }

        public static ISelectCardsActivity PutCardFromHandOnTopOfDeck(IGameLog log, Player player, string message)
        {
            return new SelectCardsActivity
                (log, player, message, SelectionSpecifications.SelectExactlyXCards(1))
            {
                AfterCardsSelected = cards => player.Deck.MoveToTop(cards.Single())
            };
        }

        public static IEnumerable<ISelectCardsActivity> PutMultipleCardsFromHandOnTopOfDeck(IGameLog log, Player player, int count)
        {
            return count.Items(i => PutCardFromHandOnTopOfDeck(log, player, 
                                                               string.Format("Select the {0} (of {1}) card to put on top of the deck.", i.ToOrderString(), count)));
        }

        public static SelectPileActivity GainACardCostingUpToX(IGameLog log, Player player, CardCost cost)
        {
            return GainACardCostingUpToX(log, player, cost, player.Discards);
        }

        public static SelectPileActivity GainACardCostingUpToX(IGameLog log, Player player, CardCost cost, CardZone destination)
        {
            return new SelectPileActivity(log, player, string.Format("Select a card to gain of cost {0} or less", cost),
                                          SelectionSpecifications.SelectPileCostingUpToX(cost))
            {
                AfterPileSelected = pile =>
                {
                    var card = pile.TopCard;
                    card.MoveTo(destination);
                    log.LogGain(player, card);
                }
            };
        }

        public static IActivity GainOpponentsCardChoice(TurnContext context, Card card, Player cardOwner)
        {
            var activity = new ChoiceActivity(context, context.ActivePlayer,
                string.Format("Gain {0}'s {1}?", cardOwner.Name, card), Choice.Yes, Choice.No);

            activity.ActOnChoice = choice =>
            {
                if (choice == Choice.Yes)
                {
                    card.MoveTo(context.ActivePlayer.Discards);
                    context.Game.Log.LogGain(context.ActivePlayer, card);
                }
            };

            return activity;
        }

        public static IActivity SelectARevealedCardToPutOnTopOfDeck(IGameLog log, Player player, RevealZone revealZone, string message)
        {
            return new SelectFromRevealedCardsActivity(log, player, revealZone, message,
                                                       SelectionSpecifications.SelectExactlyXCards(1))
            {
                AfterCardsSelected = cards => player.Deck.MoveToTop(cards.Single())
            };
        }

        public static IEnumerable<IActivity> SelectMultipleRevealedCardsToPutOnTopOfDeck(IGameLog log, Player player, RevealZone revealZone)
        {
            var count = revealZone.Count();
            return count.Items(
                (i) => SelectARevealedCardToPutOnTopOfDeck(log, player, revealZone,
                    string.Format("Select the {0} (of {1}) card to put on top of the deck.", i.ToOrderString(), count))
            );               
        }

        public static IActivity ChooseYesOrNo(IGameLog log, Player player, string message, Action ifYes)
        {
            return ChooseYesOrNo(log, player, message, ifYes, null);
        }

        public static IActivity ChooseYesOrNo(IGameLog log, Player player, string message, Action ifYes, Action ifNo)
        {
            var choiceActivity = new ChoiceActivity(log, player,
                  message,
                  Choice.Yes, Choice.No);

            choiceActivity.ActOnChoice = c =>
            {
                if (c == Choice.Yes)
                {
                    if (ifYes != null)
                        ifYes();
                }
                else
                {
                    if(ifNo != null)
                        ifNo();
                }
            };

            return choiceActivity;
        }
    }
}