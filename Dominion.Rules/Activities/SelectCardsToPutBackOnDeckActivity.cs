using System.Collections.Generic;

namespace Dominion.Rules.Activities
{
    public class SelectCardsToPutBackOnDeckActivity : SelectCardsFromHandActivity
    {
        public SelectCardsToPutBackOnDeckActivity(Player player, TurnContext context, int count)
            : base(context.Game.Log, player, string.Format("Select {0} cards to put on top of your deck.", count), ActivityType.SelectFixedNumberOfCards, 2)
        {
        }

        public override void Execute(IEnumerable<Card> cards)
        {
            //foreach (var card in cards)
            //    this.Player.Deck.MoveToTop(card);
        }
    }
}