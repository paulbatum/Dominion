using System;
using Dominion.Cards.Curses;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Familiar : Card, IActionCard
    {
        public Familiar() : base(new CardCost(3, 1))
        {}

        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 1;
            context.AddEffect(this, new FamiliarAttack());
        }

        private class FamiliarAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var gainUtil = new GainUtility(context, victim);
                gainUtil.Gain<Curse>();
            }
        }
    }
}