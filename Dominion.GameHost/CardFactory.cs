using System;
using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class CardFactory
    {
        public static Card CreateCard(string cardName)
        {
            var cardType = typeof (Copper).Assembly
                .GetTypes()
                .Single(t => t.Name == cardName);

            return (Card) Activator.CreateInstance(cardType);
        }
    }
}