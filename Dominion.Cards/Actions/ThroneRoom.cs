﻿using System;
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
            context.AddEffect(this, new ThroneRoomEffect());
        }

        private class ThroneRoomEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if (context.ActivePlayer.Hand.OfType<IActionCard>().Any())
                {
                    var activity = Activities.SelectActionToPlayMultipleTimes(context, context.ActivePlayer, context.Game.Log, source, 2);
                    _activities.Add(activity);
                }
                else
                {
                    context.Game.Log.LogMessage("{0} did not have any actions to use Throne Room on.", context.ActivePlayer.Name);
                }
            }
        }
    }
}
