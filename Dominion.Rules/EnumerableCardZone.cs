using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public abstract class EnumerableCardZone : CardZone, IEnumerable<ICard>
    {
        public IEnumerator<ICard> GetEnumerator()
        {
            return this.Cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(", ", this.Select(c => c.Name).ToArray());
        }
    }
}