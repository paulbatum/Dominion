using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Market : Card, IActionCard
    {
        public Market()
            : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 1;
            context.Buys += 1;
            context.AvailableSpend += 1;
        }
    }
}
