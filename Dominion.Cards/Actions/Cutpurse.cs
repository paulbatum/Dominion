using System;
using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
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