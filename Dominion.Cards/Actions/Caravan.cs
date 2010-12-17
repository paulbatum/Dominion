using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Caravan : Card, IActionCard
    {
        public Caravan()
            : base(4)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.DrawCards(1);

            context.AddLongLivedEffect(new CaravanEffect(this));
        }

        public class CaravanEffect : DurationEffect
        {
            public CaravanEffect(Caravan card) : base(card)
            {
            }

            public override void OnTurnStarting(TurnContext context)
            {
                base.OnTurnStarting(context);

                context.DrawCards(1);
                context.Game.Log.LogMessage("Caravan draws an extra card");
            }
        }
    }

    public class Tactician : Card, IDurationCard
    {
        public Tactician() : base(5)
        {}

        public void Play(TurnContext context)
        {
            if(context.ActivePlayer.Hand.CardCount > 0)
            {
                context.DiscardCards(context.ActivePlayer, context.ActivePlayer.Hand);
                context.AddLongLivedEffect(new TacticianEffect(this));
            }
            else
            {
                context.Game.Log.LogMessage("{0} did not have any cards to discard to the Tactician.", context.ActivePlayer.Name);
            }

        }

        public class TacticianEffect : DurationEffect
        {
            public TacticianEffect(Tactician sourceCard) : base(sourceCard)
            {}

            public override void OnTurnStarting(TurnContext context)
            {
                base.OnTurnStarting(context);

                context.DrawCards(5);
                context.RemainingActions += 1;
                context.Buys += 1;
                context.Game.Log.LogMessage("Tactician gives {0} +5 cards, +1 action and +1 buy.", context.ActivePlayer.Name);
            }
        }
    }
}
