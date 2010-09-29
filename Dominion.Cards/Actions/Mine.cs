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
            context.AddEffect(new MineEffect());
        }

        public class MineEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var activity = new SelectCardsActivity(context, "Select a treasure card to mine", 
                    SelectionSpecifications.SelectExactlyXCards(1));

                activity.Specification.CardTypeRestriction = typeof(ITreasureCard);
                activity.AfterCardsSelected = cardList =>
                {
                    var cardToMine = cardList.Single();
                    context.Trash(context.ActivePlayer, cardToMine);
                    AddGainActivity(context.Game.Log, context.ActivePlayer, cardToMine.Cost + 3);
                };

                _activities.Add(activity);
            }

            public void AddGainActivity(IGameLog log, Player player, int upToCost)
            {
                var activity = Activities.GainACardCostingUpToX(log, player, upToCost, player.Hand);
                activity.Specification.CardTypeRestriction = typeof (ITreasureCard);
                _activities.Add(activity);
            }
        }
    }
}
