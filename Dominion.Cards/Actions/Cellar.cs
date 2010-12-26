using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Cellar : Card, IActionCard
    {
        public Cellar()
            : base(2)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.AddSingleActivity(Activities.DiscardCardsToDrawCards(context, this), this);
        }
    }
}
