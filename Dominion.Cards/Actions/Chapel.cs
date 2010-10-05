using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Chapel : Card, IActionCard
    {
        public Chapel()
            : base(2)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new ChapelEffect());
        }

        private class ChapelEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var activity = new SelectCardsActivity(
                    context,
                    "Select up to 4 cards to trash",
                    SelectionSpecifications.SelectUpToXCards(4));

                activity.AfterCardsSelected = cards =>
                {
                    foreach (var cardToTrash in cards)
                        context.Trash(activity.Player, cardToTrash);
                };

                _activities.Add(activity);
            }
        }
    }
}
