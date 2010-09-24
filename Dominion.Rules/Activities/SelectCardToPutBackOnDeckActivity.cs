using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules.Activities
{
    public class SelectCardToPutBackOnDeckActivity : SelectCardsFromHandActivity
    {
        public SelectCardToPutBackOnDeckActivity(Player player, TurnContext context, string message)
            : base(context.Game.Log, player, message , ActivityType.SelectFixedNumberOfCards, 1)
        {
        }

        public override void Execute(IEnumerable<Card> cards)
        {
            var card = cards.Single();
            this.Player.Deck.MoveToTop(card);
        }
    }
}