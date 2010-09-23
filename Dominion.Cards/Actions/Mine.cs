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
                var activity = new SelectCardToActionActivity(context, "Select a treasure card to mine");
                activity.Restrictions.Add(RestrictionType.TreasureCard);
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
                var activity = new GainACardUpToActivity(log, player, upToCost);
                activity.Restrictions.Add(RestrictionType.TreasureCard);

                _activities.Add(activity);
            }
        }
    }
}
