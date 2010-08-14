using System;
using System.Collections;
using System.Collections.Generic;

namespace Dominion.Rules
{
    public class Hand : CardZone, IEnumerable<Card>
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

    public class PlayArea : CardZone, IEnumerable<Card>
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