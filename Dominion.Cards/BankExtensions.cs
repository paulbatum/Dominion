using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Cards.Curses;
using Dominion.Rules;

namespace Dominion.Cards
{
    public static class BankExtensions
    {
        public static CardPile Pile<T> (this CardBank bank)
        {
            return bank.Piles.Single(x => x.Name == typeof(T).Name);            
        }

        public static CardPile NonEmptyPile<T>(this CardBank bank)
        {
            var pile = bank.Pile<T>();
            return pile.IsEmpty ? null : pile;
        }


    }
}
