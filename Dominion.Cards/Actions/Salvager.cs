using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    class Salvager : Card, IActionCard
    {
        public Salvager()
            : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.Buys += 1;
            context.AddEffect(new SalvagerEffect());
        }

        private class SalvagerEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var activity = new SelectCardsActivity(context, "Select a card to salvage",
                    SelectionSpecifications.SelectExactlyXCards(1));

                activity.AfterCardsSelected = cardList =>
                {
                    var cardToSalvage = cardList.Single();
                    context.AvailableSpend += cardToSalvage.Cost;
                    context.Trash(context.ActivePlayer, cardToSalvage);
                };

                _activities.Add(activity);
            }
        }
    }
}
