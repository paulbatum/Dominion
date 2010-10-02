using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class FishingVillage : Card, IActionCard
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

        public class FishingVillageEffect : ILongLivedCardEffect
        {
            public FishingVillageEffect(FishingVillage card)
            {
                IsFinished = false;
                SourceCard = card;
            }

            public ICard SourceCard { get; private set; }

            public void OnTurnStarting(TurnContext context)
            {
                context.RemainingActions += 1;
                context.AvailableSpend += 1;
                IsFinished = true;
                context.Game.Log.LogMessage("FishingVillage adds one action and one spend");
            }

            public bool IsFinished { get; private set; }
        }
    }
}
