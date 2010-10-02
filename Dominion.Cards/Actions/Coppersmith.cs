using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;
using Dominion.Cards.Treasure;

namespace Dominion.Cards.Actions
{
    public class Coppersmith : Card, IActionCard
    {
        public Coppersmith()
            : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddPassiveCardEffect(new CoppersmithPassiveEffect());
        }

        private class CoppersmithPassiveEffect : PassiveCardEffectBase
        {
            public override CardCost ModifyValue(CardCost currentValue, ITreasureCard card)
            {
                if (card is Copper)
                    return currentValue + 1;

                return currentValue;
            }
        }
    }
}
