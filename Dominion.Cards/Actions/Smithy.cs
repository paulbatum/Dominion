using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Smithy : Card, IActionCard
    {
        public Smithy() : base(4) { }

        public void Play(TurnContext context)
        {
            context.DrawCards(3);
        }
    }
}
