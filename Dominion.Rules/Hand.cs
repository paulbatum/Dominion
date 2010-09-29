using System;
using System.Linq;

namespace Dominion.Rules
{
    public class Hand : EnumerableCardZone
    {
        
    }

    public class RevealZone : EnumerableCardZone
    {
        public RevealZone(Player owner)
        {
            Owner = owner;
        }

        // The owner of the cards in the reveal zone
        public Player Owner { get; private set; }
    }
}