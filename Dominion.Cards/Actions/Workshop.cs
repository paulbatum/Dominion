using System;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Workshop : ActionCard
    {
        public Workshop() : base(3)
        {
        }

        protected override void Play(TurnContext context)
        {
            context.AddEffect(new WorkshopEffect(context));
        }

        public class WorkshopEffect : CardEffectBase
        {
            public WorkshopEffect(TurnContext context)
            {
                _activities.Add(new GainACardUpToActivity(context.Game.Log, context.ActivePlayer, 4));
            }
        }
    }
}