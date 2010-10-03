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
            context.AddEffect(new FamiliarAttack());
        }

        private class FamiliarAttack : AttackEffect
        {
            public override void Attack(Player player, TurnContext context)
            {
                var gainUtil = new GainUtility(context, player);
                gainUtil.Gain<Curse>();
            }
        }
    }
}