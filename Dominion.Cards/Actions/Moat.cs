using System;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Moat : ActionCard
    {
        public Moat() : base(2)
        {
        }

        protected override void Play(TurnContext context)
        {
            context.DrawCards(2);
        }
    }
}