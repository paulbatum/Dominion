using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Festival : Card, IActionCard
    {
        public Festival() : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 2;
            context.Buys += 1;
            context.MoneyToSpend += 2;
        }
    }
}
