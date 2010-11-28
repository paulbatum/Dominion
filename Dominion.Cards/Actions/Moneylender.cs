using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;
using Dominion.Cards.Treasure;

namespace Dominion.Cards.Actions
{
    public class Moneylender : Card, IActionCard
    {
        public Moneylender()
            : base(4)
        { }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new MoneylenderEffect());
        }

        private class MoneylenderEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if (context.ActivePlayer.Hand.OfType<Copper>().Any())
                {
                    var copperCard = context.ActivePlayer.Hand.OfType<Copper>().FirstOrDefault();
                    context.Trash(context.ActivePlayer, copperCard);
                    context.AvailableSpend += 3;
                };
            }
        }
    }
}
