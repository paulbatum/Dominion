using System.Collections;
using System.Collections.Generic;

namespace Dominion.Rules
{
    public abstract class EnumerableCardZone : CardZone, IEnumerable<Card>
    {
        public IEnumerator<Card> GetEnumerator()
        {
            return this.Cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}