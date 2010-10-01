using System;
using System.Linq;
using Dominion.Cards.Curses;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.Activities;
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
                    GainCopperAndCurse(player, context);
                }
                else
                {
                    var activity = Activities.ChooseYesOrNo(context.Game.Log, player, "Discard a curse?",
                        () => context.DiscardCard(player, curseInHand),
                        () => GainCopperAndCurse(player, context));                  

                    _activities.Add(activity);
                    
                }
            }

            private void GainCopperAndCurse(Player player, TurnContext context)
            {
                var gainUtil = new GainUtility(context, player);                    
                gainUtil.Gain<Copper>();                    
                gainUtil.Gain<Curse>();
            }
        }
    }

    
}