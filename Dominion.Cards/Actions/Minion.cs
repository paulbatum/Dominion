using System;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Minion : Card, IActionCard, IAttackCard
    {
        public Minion() : base(5)
        {}

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.AddEffect(this, new MinionAttackEffect());
        }

        public class MinionAttackEffect : AttackEffect
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                Action discardAction = () =>
                {
                    context.DiscardCards(context.ActivePlayer, context.ActivePlayer.Hand);
                    context.DrawCards(4);
                    DistributeAttacks(context, source);
                };

                Action moneyAction = () => context.AvailableSpend += 2;

                var activity = Activities.ChooseYesOrNo(context.Game.Log, context.ActivePlayer, "Discard your hand?",
                                                        source, discardAction, moneyAction);
                _activities.Add(activity);
            }

            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                if (victim.Hand.CardCount > 4)
                {
                    context.DiscardCards(victim, victim.Hand);
                    victim.DrawCards(4);
                }
            }
        }
    }
}