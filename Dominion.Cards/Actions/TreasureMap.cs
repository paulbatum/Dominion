using System;
using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class TreasureMap : Card, IActionCard
    {
        public TreasureMap() : base(4)
        {}

        public void Play(TurnContext context)
        {
            context.Trash(context.ActivePlayer, this);
            var otherMap = context.ActivePlayer.Hand.OfType<TreasureMap>().FirstOrDefault();
            if(otherMap != null)
            {
                context.Trash(context.ActivePlayer, otherMap);
                var gainUtil = new GainUtility(context, context.ActivePlayer);
                var deck = context.ActivePlayer.Deck;
                gainUtil.Gain<Gold>(deck.MoveToTop);
                gainUtil.Gain<Gold>(deck.MoveToTop);
                gainUtil.Gain<Gold>(deck.MoveToTop);
                gainUtil.Gain<Gold>(deck.MoveToTop);
            }
        }
    }
}