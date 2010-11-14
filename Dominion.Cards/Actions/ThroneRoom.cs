using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class ThroneRoom : Card, IActionCard
    {
        public ThroneRoom() : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(new ThroneRoomEffect());
        }

        private class ThroneRoomEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context)
            {
                var player = context.ActivePlayer;
                var log = context.Game.Log;

                if (player.Hand.OfType<IActionCard>().Any())
                {
                    var activity = new SelectCardsActivity(
                        log, player,
                        "Select an action to play twice",
                        SelectionSpecifications.SelectExactlyXCards(1));

                    activity.Specification.CardTypeRestriction = typeof (IActionCard);
                    activity.AfterCardsSelected = cards =>
                    {
                        var actionCard = cards.OfType<IActionCard>().Single();
                        log.LogMessage("{0} selected {1} to be played twice.", player.Name, actionCard.Name);

                        actionCard.MoveTo(context.ActivePlayer.PlayArea);
                        context.AddEffect(new PlayCardEffect(actionCard));
                        context.AddEffect(new PlayCardEffect(actionCard));                        
                    };

                    _activities.Add(activity);
                }
                    
            }

            private class PlayCardEffect : CardEffectBase
            {
                private readonly IActionCard _card;

                public PlayCardEffect(IActionCard card)
                {
                    _card = card;
                }

                public override void Resolve(TurnContext context)
                {
                    _card.Play(context);
                }
            }
            
        }
    }
}
