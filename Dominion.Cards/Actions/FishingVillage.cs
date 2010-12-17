using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class FishingVillage : Card, IActionCard, IDurationCard
    {
        public FishingVillage()
            : base(3)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 2;
            context.AvailableSpend += 1;

            context.AddLongLivedEffect(new FishingVillageEffect(this));
        }

        public class FishingVillageEffect : DurationEffect
        {
            public FishingVillageEffect(FishingVillage sourceCard) : base(sourceCard)
            {}

            public override void OnTurnStarting(TurnContext context)
            {
                base.OnTurnStarting(context);

                context.RemainingActions += 1;
                context.AvailableSpend += 1;
                context.Game.Log.LogMessage("FishingVillage adds one action and one spend");
            }
        }
    }
}
