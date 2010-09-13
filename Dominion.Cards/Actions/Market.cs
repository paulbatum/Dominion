using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Market : ActionCard
    {
        public Market()
            : base(5)
        {
        }

        protected override void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 1;
            context.Buys += 1;
            context.MoneyToSpend += 1;
        }
    }
}
