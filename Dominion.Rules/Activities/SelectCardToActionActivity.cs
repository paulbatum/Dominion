using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Rules.Activities
{
    public class SelectCardToActionActivity : SelectCardsFromHandActivity
    {
        public SelectCardToActionActivity(TurnContext context, string message)
            : base(context.Game.Log, context.ActivePlayer, message, ActivityType.SelectFixedNumberOfCards, 1)
        {
        }

        public Action<IEnumerable<Card>> AfterCardsSelected { get; set; }

        public override void Execute(IEnumerable<Card> cards)
        {
            AfterCardsSelected(cards);
        }
    }
}
