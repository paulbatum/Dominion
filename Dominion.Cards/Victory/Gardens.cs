using System;
using System.Linq;
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

    public class Duke : Card, IVictoryCard
    {
        public Duke() : base(5)
        {
        }

        public int Score(EnumerableCardZone allCards)
        {
            return allCards.OfType<Duchy>().Count();
        }
    }

}
