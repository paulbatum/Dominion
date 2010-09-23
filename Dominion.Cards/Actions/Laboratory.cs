using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Laboratory : Card, IActionCard
    {
        public Laboratory() : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.DrawCards(2);
        }
    }
}
