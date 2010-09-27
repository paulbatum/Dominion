using System.Collections.Generic;
using Dominion.Rules;
using System;

namespace Dominion.GameHost
{
    public static class CardBuilderExtensions
    {
        public static CardPile WithNewCards<T>(this CardPile targetPile, int count) where T : Card, new()
        {
            count.Times(() => new T().MoveTo(targetPile));
            return targetPile;
        }

        public static CardPile WithNewCards(this CardPile targetPile, Type cardType, int count)
        {
            count.Times(() => {
                var newCard = (Card)Activator.CreateInstance(cardType);
                newCard.MoveTo(targetPile);
            });
            return targetPile;
        }

        public static IEnumerable<Card> NewCards<T>(this int count) where T : Card, new()
        {
            return count.Items<Card>(() => new T());
        }
    }
}