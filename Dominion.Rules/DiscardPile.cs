using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class DiscardPile : CardZone
    {
        public Card TopCard
        {
            get { return this.Cards.Last(); }
        }


    }
}