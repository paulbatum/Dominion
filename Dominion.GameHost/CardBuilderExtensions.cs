using System.Collections.Generic;
using Dominion.Rules;
using System;

namespace Dominion.GameHost
{
    public static class CardBuilderExtensions
    {
        public static CardPile WithNewCards<T>(this CardPile targetPile, int count) where T : ICard, new()
        {
            count.Times(() => new T().MoveTo(targetPile));
            return targetPile;
        }

        public static CardPile WithNewCards(this CardPile targetPile, string cardName, int count)
        {
            count.Times(() => CardFactory.CreateCard(cardName).MoveTo(targetPile));
            return targetPile;
        }

        public static IEnumerable<ICard> NewCards<T>(this int count) where T : Card, new()
        {
            return count.Items<ICard>(() => new T());
        }
    }
}