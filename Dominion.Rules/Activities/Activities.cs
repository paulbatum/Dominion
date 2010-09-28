using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules.Activities
{
    public static class Activities
    {
        public static ISelectCardsActivity DiscardCards(TurnContext context, Player player, int numberToDiscard)
        {
            return new SelectCardsFromHandActivity(
                context.Game.Log, player, string.Format("Select {0} card(s) to discard", numberToDiscard),
                SelectionSpecifications.SelectExactlyXCards(numberToDiscard))
            {
                AfterCardsSelected = cards => context.DiscardCards(player, cards)
            };
        }

        public static ISelectCardsActivity PutCardFromHandOnTopOfDeck(IGameLog log, Player player, string message)
        {
            return new SelectCardsFromHandActivity
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

        public static SelectPileActivity GainACardCostingUpToX(IGameLog log, Player player, int cost)
        {
            return GainACardCostingUpToX(log, player, cost, player.Discards);
        }

        public static SelectPileActivity GainACardCostingUpToX(IGameLog log, Player player, int cost, CardZone destination)
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
    }
}