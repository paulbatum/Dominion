using System;
using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Rabble : Card, IActionCard, IAttackCard
    {
        public Rabble()
            : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(3);
            context.AddEffect(this, new RabbleAttack());
        }

        private class RabbleAttack : AttackEffect
        {
            public override void Attack(Player victim, TurnContext context, ICard source)
            {
                var revealZone = new RevealZone(victim);
                victim.Deck.MoveTop(3, revealZone);

                revealZone.LogReveal(context.Game.Log);
                revealZone.MoveWhere(c => c is IActionCard || c is ITreasureCard, victim.Discards);

                foreach (var activity in Activities.SelectMultipleRevealedCardsToPutOnTopOfDeck(context.Game.Log, victim, revealZone, source))
                    _activities.Add(activity);
            }

        }
    }

    public class Apothecary : Card, IActionCard
    {
        public Apothecary() : base(CardCost.Parse("2P"))
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(1);
            context.RemainingActions += 1;

            context.AddEffect(this, new ApothecaryEffect());           
        }

        public class ApothecaryEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                var revealZone = new RevealZone(context.ActivePlayer);
                context.ActivePlayer.Deck.MoveTop(4, revealZone);

                revealZone.LogReveal(context.Game.Log);
                revealZone.MoveWhere(c => c is Copper || c is Potion, context.ActivePlayer.Hand);

                foreach (var activity in Activities.SelectMultipleRevealedCardsToPutOnTopOfDeck(context.Game.Log, context.ActivePlayer, revealZone, source))
                    _activities.Add(activity);
            }
        }
    }

    
}