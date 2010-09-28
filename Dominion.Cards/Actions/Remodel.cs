using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Remodel : Card, IActionCard
    {
        public Remodel()
            : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new RemodelEffect());
        }

        public class RemodelEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var remodelActivity = new SelectCardsFromHandActivity(context, "Select a card to remodel", 
                    SelectionSpecifications.SelectExactlyXCards(1));

                remodelActivity.AfterCardsSelected = cardList =>
                {
                    var player = context.ActivePlayer;
                    var cardToRemodel = cardList.Single();
                    context.Trash(player, cardToRemodel);

                    var gainActivity = Activities.GainACardCostingUpToX(context.Game.Log, player, cardToRemodel.Cost + 2);
                    _activities.Add(gainActivity);
                };

                _activities.Add(remodelActivity);
            }
        }
    }
}