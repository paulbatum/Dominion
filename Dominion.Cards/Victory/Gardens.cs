using System;
using Dominion.Rules.CardTypes;
using Dominion.Rules;

namespace Dominion.Cards.Victory
{
    public class Gardens : Card, IVictoryCard
    {

        public Gardens()
            : base(4)
        {
        }

        public int Score(EnumerableCardZone allCards)
        {
            return (allCards.CardCount / 10);
        }


        public int Value
        {
            get { return 0; }
        }
    }
}
