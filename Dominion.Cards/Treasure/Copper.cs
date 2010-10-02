using System;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Treasure
{
    public class Copper : Card, IMoneyCard
    {
        public Copper() : base(0)
        {}

        public CardCost Value { get { return 1; } }
    }
}