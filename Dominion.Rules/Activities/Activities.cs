using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules.Activities
{
    public static class Activities
    {
        public static ISelectCardsActivity DiscardCards(TurnContext context, Player player, int numberToDiscard, ICard source)
        {
            return new SelectCardsActivity(
                context.Game.Log, player, string.Format("Select {0} card(s) to discard.", numberToDiscard),
                SelectionSpecifications.SelectExactlyXCards(numberToDiscard), source)
            {
                AfterCardsSelected = cards => context.DiscardCards(player, cards),
                Hint = ActivityHint.DiscardCards
            };
        }

        public static ISelectCardsActivity PutCardFromHandOnTopOfDeck(TurnContext context, ICard source)
        {
            return PutCardFromHandOnTopOfDeck(context.Game.Log, context.ActivePlayer,
                                                "Select a card to put on top of the deck.", source);
        }

        public static ISelectCardsActivity PutCardFromHandOnTopOfDeck(IGameLog log, Player player, string message, ICard source)
        {
            return new SelectCardsActivity
                (log, player, message, SelectionSpecifications.SelectExactlyXCards(1), source)
            {
                AfterCardsSelected = cards =>
                {
                    player.Deck.MoveToTop(cards.Single());
                    log.LogMessage("{0} put a card on top of the deck.", player.Name);
                },
                Hint = ActivityHint.RedrawCards
            };
        }

        public static IEnumerable<ISelectCardsActivity> PutMultipleCardsFromHandOnTopOfDeck(IGameLog log, Player player, int count, ICard source)
        {
            return count.Items(i => PutCardFromHandOnTopOfDeck(log, player, 
                                                               string.Format("Select the {0} (of {1}) card to put on top of the deck.", i.ToOrderString(), count), source));
        }

        public static SelectPileActivity GainACardCostingUpToX(IGameLog log, Player player, CardCost cost, ICard source)
        {
            return GainACardCostingUpToX(log, player, cost, player.Discards, source);
        }

        public static SelectPileActivity GainACardCostingUpToX(IGameLog log, Player player, CardCost cost, CardZone destination, ICard source)
        {
            return new SelectPileActivity(log, player, string.Format("Select a card to gain of cost {0} or less.", cost),
                                          SelectionSpecifications.SelectPileCostingUpToX(cost), source)
            {
                AfterPileSelected = pile =>
                {
                    var card = pile.TopCard;
                    card.MoveTo(destination);
                    log.LogGain(player, card);
                },
                Hint = ActivityHint.GainCards
            };
        }

        public static SelectPileActivity GainACardCostingExactlyX(IGameLog log, Player player, CardCost cost, CardZone destination, ICard source)
        {
            return new SelectPileActivity(log, player, string.Format("Select a card to gain with a cost of exactly {0}.", cost),
                                          SelectionSpecifications.SelectPileCostingExactlyX(cost), source)
            {
                AfterPileSelected = pile =>
                {
                    var card = pile.TopCard;
                    card.MoveTo(destination);
                    log.LogGain(player, card);
                },
                Hint = ActivityHint.GainCards
            };
        }

        public static IActivity GainOpponentsCardChoice(TurnContext context, Card card, Player cardOwner, ICard source)
        {
            var activity = new ChoiceActivity(context, context.ActivePlayer,
                string.Format("Gain {0}'s {1}?", cardOwner.Name, card), source, Choice.Yes, Choice.No);

            activity.ActOnChoice = choice =>
            {
                if (choice == Choice.Yes)
                {
                    card.MoveTo(context.ActivePlayer.Discards);
                    context.Game.Log.LogGain(context.ActivePlayer, card);
                }
            };

            activity.Hint = ActivityHint.GainCards;

            return activity;
        }

        public static IActivity SelectARevealedCardToPutOnTopOfDeck(IGameLog log, Player player, RevealZone revealZone, string message, ICard source)
        {
            return new SelectFromRevealedCardsActivity(log, player, revealZone, message,
                                                       SelectionSpecifications.SelectExactlyXCards(1), source)
            {
                AfterCardsSelected = cards => player.Deck.MoveToTop(cards.Single()),
                Hint = ActivityHint.RedrawCards
            };
        }

        public static IEnumerable<IActivity> SelectMultipleRevealedCardsToPutOnTopOfDeck(IGameLog log, Player player, RevealZone revealZone, ICard source)
        {
            var count = revealZone.Count();
            return count.Items(
                (i) => SelectARevealedCardToPutOnTopOfDeck(log, player, revealZone,
                    string.Format("Select the {0} (of {1}) card to put on top of the deck.", i.ToOrderString(), count), source)
            );               
        }

        public static IActivity ChooseYesOrNo(IGameLog log, Player player, string message, ICard source, Action ifYes)
        {
            return ChooseYesOrNo(log, player, message, source, ifYes, null);
        }

        public static IActivity ChooseYesOrNo(IGameLog log, Player player, string message, ICard source, Action ifYes, Action ifNo)
        {
            var choiceActivity = new ChoiceActivity(log, player,
                  message,
                  source,
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

        public static IActivity SelectUpToXCardsToTrash(TurnContext context, Player player, int count, ICard source)
        {
            var activity = new SelectCardsActivity(context.Game.Log, player,    
                string.Format("Select up to {0} card(s) to trash.", count),
                 SelectionSpecifications.SelectUpToXCards(count), source);

            activity.Hint = ActivityHint.TrashCards;
            activity.AfterCardsSelected = cards =>
            {
                foreach (var cardToTrash in cards)
                    context.Trash(activity.Player, cardToTrash);
            };

            return activity;
        }

        public static ISelectPileActivity SelectACardForOpponentToGain(TurnContext context, Player player, Player victim, CardCost cost, ICard source)
        {
            return new SelectPileActivity(context.Game.Log, player, string.Format("Select a card for {0} to gain of cost {1}.", victim.Name, cost),
                                          SelectionSpecifications.SelectPileCostingExactlyX(cost), source)
            {
                AfterPileSelected = pile =>
                {                    
                    var card = pile.TopCard;
                    card.MoveTo(victim.Discards);
                    context.Game.Log.LogGain(victim, card);
                },
                Hint = ActivityHint.OpponentGainCards
            };
        }

        public static IActivity DiscardCardsToDrawCards(TurnContext context, ICard source)
        {
            var activity = new SelectCardsActivity(
                   context,
                   "Select any number of cards to discard, you will draw 1 new card for each discard.",
                   SelectionSpecifications.SelectUpToXCards(context.ActivePlayer.Hand.CardCount), source);

            activity.AfterCardsSelected = cards =>
            {
                context.DiscardCards(activity.Player, cards);
                context.DrawCards(cards.Count());
            };

            return activity;
        }

        public static IActivity GainAnActionCardCostingUpToX(IGameLog log, Player player, int cost, ICard source, bool optional)
        {
            var activity = new SelectPileActivity(log, player, string.Format("Select an action card to gain of cost {0} or less.", cost),
                              SelectionSpecifications.SelectPileCostingUpToX(cost), source)
            {
                AfterPileSelected = pile =>
                {
                    var card = pile.TopCard;
                    card.MoveTo(player.Discards);
                    log.LogGain(player, card);
                },
                Hint = ActivityHint.GainCards
            };

            activity.Specification.CardTypeRestriction = typeof (IActionCard);
            activity.IsOptional = optional;

            return activity;
        }
    }
}