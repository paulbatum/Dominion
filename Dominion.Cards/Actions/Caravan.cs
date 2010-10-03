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

        public class CaravanEffect : ILongLivedCardEffect
        {
            public CaravanEffect(Caravan card)
            {
                IsFinished = false;
                SourceCard = card;
            }

            public ICard SourceCard { get; private set; }

            public void OnTurnStarting(TurnContext context)
            {
                context.DrawCards(1);
                IsFinished = true;
                context.Game.Log.LogMessage("Caravan draws an extra card");
            }

            public bool IsFinished { get; private set; }
        }
    }
}
