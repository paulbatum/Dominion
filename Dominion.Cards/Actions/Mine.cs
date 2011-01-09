using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Mine : Card, IActionCard
    {
        public Mine() : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new MineEffect());
        }

        public class MineEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if (context.ActivePlayer.Hand.OfType<ITreasureCard>().Any())
                {
                    var activity = new SelectCardsActivity(context, "Select a treasure card to mine",
                                                           SelectionSpecifications.SelectExactlyXCards(1), source);

                    activity.Specification.CardTypeRestriction = typeof (ITreasureCard);
                    activity.AfterCardsSelected = cardList =>
                    {
                        var cardToMine = cardList.Single();
                        context.Trash(context.ActivePlayer, cardToMine);
                        AddGainActivity(context.Game.Log, context.ActivePlayer, cardToMine.Cost + 3, source);
                    };

                    _activities.Add(activity);
                }
                else
                {
                    context.Game.Log.LogMessage("No treasure cards to trash.");
                }
            }

            public void AddGainActivity(IGameLog log, Player player, CardCost upToCost, ICard source)
            {
                var activity = Activities.GainACardCostingUpToX(log, player, upToCost, player.Hand, source);
                activity.Specification.CardTypeRestriction = typeof (ITreasureCard);
                _activities.Add(activity);
            }
        }
    }
}
