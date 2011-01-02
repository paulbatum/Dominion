using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;
using Dominion.Cards.Curses;

namespace Dominion.Cards.Actions
{
    public class Witch : Card, IActionCard, IAttackCard
    {
        public Witch()
           : base(5)
        {

        }

        public void Play(TurnContext context)
        {
            context.DrawCards(2);
            context.AddEffect(this, new WitchAttack());            
        }

        private class WitchAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var gainUtil = new GainUtility(context, victim);                
                gainUtil.Gain<Curse>();       
            }
        }
    }
}
