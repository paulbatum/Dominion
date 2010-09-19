using System;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Moat : Card, IActionCard, IReactionCard
    {
        public Moat() : base(2)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(2);
        }

    }
}