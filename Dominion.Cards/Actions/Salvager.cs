using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Salvager : Card, IActionCard
    {
        public Salvager()
            : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.Buys += 1;
            context.AddEffect(this, new SalvagerEffect());
        }

        private class SalvagerEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if (context.ActivePlayer.Hand.CardCount > 0)
                {
                    var activity = new SelectCardsActivity(context, "Select a card to trash",
                        SelectionSpecifications.SelectExactlyXCards(1), source);

                    activity.AfterCardsSelected = cardList =>
                    {
                        var cardToSalvage = cardList.Single();
                        context.AvailableSpend += cardToSalvage.Cost.Money;
                        context.Trash(context.ActivePlayer, cardToSalvage);
                    };
                    _activities.Add(activity);
                }
            }
        }
    }
}
