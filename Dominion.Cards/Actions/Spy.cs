using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Spy : Card, IAttackCard, IActionCard
    {
        public Spy() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 1;
            context.AddEffect(this, new SpyAttack());
        }

        public class SpyAttack : AttackEffect
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                base.Resolve(context, source);

                if(context.ActivePlayer.Deck.TopCard != null)
                    _activities.Add(Activities.ChooseWhetherToMillTopCard(context, context.ActivePlayer, context.ActivePlayer, source));
            }

            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                if(victim.Deck.TopCard != null)
                    _activities.Add(Activities.ChooseWhetherToMillTopCard(context, context.ActivePlayer, victim, source));
            }
        }
    }
}