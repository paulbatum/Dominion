using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Militia : Card, IActionCard
    {
        public Militia() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.MoneyToSpend += 2;
            context.AddEffect(new MilitiaAttack());
        }

        private class MilitiaAttack : AttackEffect
        {
            public override void Attack(Player player, TurnContext context)
            {
                var numberToDiscard = player.Hand.CardCount - 3;

                if (numberToDiscard > 0)
                    _activities.Add(Activities.DiscardCards(context, player, numberToDiscard));
                else
                {
                    context.Game.Log.LogMessage("{0} did not have to discard any cards.", player.Name);
                }
            }            
        }
    }
}