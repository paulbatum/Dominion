using System;
using System.Linq;
using Dominion.Cards.Curses;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Mountebank : Card, IActionCard, IAttackCard
    {
        public Mountebank() : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.MoneyToSpend += 2;
            context.AddEffect(new MountebankAttack());
        }

        public class MountebankAttack : AttackEffect
        {
            public override void Attack(Player player, TurnContext context)
            {
                var curseInHand = player.Hand.OfType<Curse>().FirstOrDefault();

                if(curseInHand == null)
                {
                    context.Game.Log.LogMessage("{0} did not have a Curse to discard.", player.Name);
                    var gainUtil = new GainUtility(context, player);                    
                    gainUtil.Gain<Copper>();                    
                    gainUtil.Gain<Curse>();                    
                }
                else
                {
                    curseInHand.MoveTo(player.Discards);
                    context.Game.Log.LogDiscard(player, curseInHand);
                }
            }
        }
    }

    
}