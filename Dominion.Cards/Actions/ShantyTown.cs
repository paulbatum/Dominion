using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class ShantyTown : Card, IActionCard
    {
        public ShantyTown() : base(3)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 2;

            context.Game.Log.LogRevealHand(context.ActivePlayer);
            
            if (context.ActivePlayer.Hand.OfType<IActionCard>().Count() == 0) 
                context.DrawCards(2);
        }
    }
}
