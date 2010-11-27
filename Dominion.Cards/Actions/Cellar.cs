using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Cellar : Card, IActionCard
    {
        public Cellar()
            : base(2)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.AddEffect(this, new CellarEffect());
        }

        private class CellarEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                var activity = new SelectCardsActivity(
                    context,
                    "Select any number of cards to discard, you will draw 1 new card for each discard",
                    SelectionSpecifications.SelectUpToXCards(context.ActivePlayer.Hand.CardCount), source);

                activity.AfterCardsSelected = cards =>
                {
                    context.DiscardCards(activity.Player, cards);
                    context.DrawCards(cards.Count());
                };

                _activities.Add(activity);
            }
        }
    }
}
