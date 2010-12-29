using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Rules.CardTypes
{
    public interface IScoreCard : ICard
    {
        int Score(EnumerableCardZone allCards);
    }
}
