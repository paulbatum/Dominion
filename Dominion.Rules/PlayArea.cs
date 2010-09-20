using System;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public class PlayArea : EnumerableCardZone
    {
        public void SortForScoring()
        {
            foreach(var card in this.Cards                
                .OrderByDescending(c => c is IScoreCard ? ((IScoreCard)c).Score(this) : -100)
                .ThenBy(c => c.Name))
            {
                card.MoveTo(this);
            }
        }

    }
}