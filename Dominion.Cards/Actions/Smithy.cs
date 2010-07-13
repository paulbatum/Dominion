using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Smithy : ActionCard
    {
        public override void Play(TurnContext context)
        {
            context.DrawCards(3);
        }
    }
}
