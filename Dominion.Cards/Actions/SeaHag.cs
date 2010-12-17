using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;
using Dominion.Cards.Curses;

namespace Dominion.Cards.Actions
{
    public class SeaHag : Card, IActionCard, IAttackCard
    {
        public SeaHag() : base(4) { }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new SeaHagAttack());
        }

        private class SeaHagAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                if (victim.Deck.CardCount + victim.Discards.CardCount > 0)
                    victim.Deck.MoveTop(1, victim.Discards);

                var gainUtil = new GainUtility(context, victim);
                gainUtil.Gain<Curse>(c => victim.Deck.MoveToTop(c));
            }
        }
    }
}
