using System;
using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Rules;
using System.Collections.Generic;
using Dominion.Cards.Victory;
using Dominion.Cards.Curses;

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

        public static IList<string> OptionalCardsForBank
        {
            get
            {
                var allCards = typeof(Copper).Assembly
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Card)));

                var cardsToExclude = new Type[]{
                    typeof(Copper),
                    typeof(Silver),
                    typeof(Gold),
                    typeof(Estate),
                    typeof(Duchy),
                    typeof(Province),
                    typeof(Curse),
                    typeof(Colony),
                    typeof(Platinum),
                    typeof(Potion),
                };

                return allCards
                    .Except(cardsToExclude)
                    .Select(c => c.Name)
                    .ToList();
            }
        }
    }
}