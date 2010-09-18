using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Rules.CardTypes
{
    public interface IScoreCard
    {
        int Value { get; set; }

        int Score(CardZone allCards);
    }
}
