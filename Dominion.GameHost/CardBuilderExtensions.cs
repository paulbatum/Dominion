using System.Collections.Generic;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public static class CardBuilderExtensions
    {
        public static CardPile WithNewCards<T>(this CardPile targetPile, int count) where T : Card, new()
        {
            count.Times(() => new T().MoveTo(targetPile));
            return targetPile;
        }

        public static IEnumerable<Card> NewCards<T>(this int count) where T : Card, new()
        {
            return count.Items<Card>(() => new T());
        }
    }
}