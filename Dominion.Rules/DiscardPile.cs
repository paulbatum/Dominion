using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class DiscardPile : EnumerableCardZone
    {
        public ICard TopCard
        {
            get { return this.Cards.Last(); }
        }


    }
}