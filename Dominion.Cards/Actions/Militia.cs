using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Militia : Card, IActionCard, IAttackCard
    {
        public Militia() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.AvailableSpend += 2;
            context.AddEffect(this, new MilitiaAttack());
        }

        private class MilitiaAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var numberToDiscard = victim.Hand.CardCount - 3;

                if (numberToDiscard > 0)
                    _activities.Add(Activities.DiscardCards(context, victim, numberToDiscard, source));
                else
                {
                    context.Game.Log.LogMessage("{0} did not have to discard any cards.", victim.Name);
                }
            }            
        }
    }

    public class Cutpurse : Card, IActionCard, IAttackCard
    {
        public Cutpurse() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.AvailableSpend += 2;
            context.AddEffect(this, new CutpurseAttack());
        }

        public class CutpurseAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var copper = victim.Hand.FirstOrDefault(c => c is Copper);

                if (copper != null)
                    context.DiscardCard(victim, copper);
                else
                    context.Game.Log.LogRevealHand(victim);
            }
        }
    }
}