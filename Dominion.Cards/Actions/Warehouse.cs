using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Warehouse : Card, IActionCard
    {
        public Warehouse() : base(3)
        {
            
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.DrawCards(3);
            context.AddEffect(new DiscardEffect());
            context.AddEffect(new DiscardEffect());
            context.AddEffect(new DiscardEffect());
        }

        private class DiscardEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var discardActivity = new SelectCardsActivity(context, "Select a card to discard",
                    SelectionSpecifications.SelectExactlyXCards(1));

                discardActivity.AfterCardsSelected = cardList =>
                {
                    var cardToDiscard = cardList.Single();
                    context.DiscardCard(context.ActivePlayer, cardToDiscard);
                };

                _activities.Add(discardActivity);
            }
        }
    }
}
