using System;
using System.Text.RegularExpressions;

namespace Dominion.Rules
{
    public class CardCost : IEquatable<CardCost>
    {
        public CardCost(int money)
            :this(money, 0)
        {
            
        }

        public CardCost(int money, int potions)
        {
            Money = money;
            Potions = potions;
        }

        public int Money { get; private set; }
        public int Potions { get; private set; }

        public override string ToString()
        {
            var result = Money.ToString();
            Potions.Times(() => result = result + "P");
            return result;
        }

        public static CardCost Potion
        {
            get { return new CardCost(0, 1); }
        }

        public static CardCost Parse(string stringCost)
        {
            var match = Regex.Match(stringCost, @"(\d+)(P*)");
            var money = int.Parse(match.Groups[1].Value);
            var potions = match.Groups.Count > 2 ? match.Groups[2].Length : 0;

            return new CardCost(money, potions);
        }

        public bool IsEnoughFor(ICard card)
        {
            return IsEnoughFor(card.Cost);
        }

        public bool IsEnoughFor(CardCost cost)
        {
            return this.Money >= cost.Money && this.Potions >= cost.Potions;
        }

        public static implicit operator CardCost (int money)
        {
            return new CardCost(money);
        }

        public static explicit operator CardCost(string cost)
        {
            return CardCost.Parse(cost);
        }

        public static CardCost  operator + (CardCost a, CardCost b)
        {
            return new CardCost(a.Money + b.Money, a.Potions + b.Potions);
        }

        public static CardCost operator -(CardCost a, CardCost b)
        {
            return new CardCost(a.Money - b.Money, a.Potions - b.Potions);
        }

        //public static bool operator <(CardCost a, CardCost b)
        //{
        //    if(a.Potions != b.Potions)
        //        throw new ArgumentException("Cannot compare two card costs that have differing potion values");

        //    return a.Money < b.Money;
        //}

        //public static bool operator >(CardCost a, CardCost b)
        //{
        //    if (a.Potions != b.Potions)
        //        throw new ArgumentException("Cannot compare two card costs that have differing potion values");

        //    return a.Money > b.Money;
        //}

        //public static bool operator <=(CardCost a, CardCost b)
        //{
        //    if (a.Potions != b.Potions)
        //        throw new ArgumentException("Cannot compare two card costs that have differing potion values");

        //    return a.Money <= b.Money;
        //}

        //public static bool operator >=(CardCost a, CardCost b)
        //{
        //    if (a.Potions != b.Potions)
        //        throw new ArgumentException("Cannot compare two card costs that have differing potion values");

        //    return a.Money >= b.Money;
        //}

        
        public static bool operator ==(CardCost a, CardCost b)
        {
            if (ReferenceEquals(a, null)) 
                return ReferenceEquals(b, null);

            return a.Equals(b);
        }

        public static bool operator !=(CardCost a, CardCost b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Money*397) ^ Potions;
            }
        }

        public bool Equals(CardCost other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Money == Money && other.Potions == Potions;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as CardCost);
        }

        
    }
}