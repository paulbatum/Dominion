using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules.Activities
{
    public class DiscardCardsActivity : SelectCardsFromHandActivity
    {
        public DiscardCardsActivity(IGameLog log, Player player, int numberToDiscard)
            : base(log, player, string.Format("Select {0} cards to discard", numberToDiscard), ActivityType.SelectFixedNumberOfCards, numberToDiscard)
        { }

        public override void Execute(IEnumerable<Card> cards)
        {
            foreach (var card in cards.ToList())
            {
                Log.LogDiscard(Player, card);
                card.MoveTo(Player.Discards);
            }
        }
    }
}