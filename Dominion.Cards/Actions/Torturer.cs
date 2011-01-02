using System;
using Dominion.Cards.Curses;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Torturer : Card, IActionCard, IAttackCard
    {
        public Torturer() : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(3);
            context.AddEffect(this, new TorturerAttack());
        }

        public class TorturerAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                Action discardCards = () => _activities.Add(Activities.DiscardCards(context, victim, 2, source));
                Action gainCurse = () => new GainUtility(context, victim).Gain<Curse>(victim.Hand);

                var decideActivity = Activities.ChooseYesOrNo(context.Game.Log, victim, "Discard two cards to Torturer?",
                                                              source, discardCards, gainCurse);

                _activities.Add(decideActivity);
            }

        }
    }
}