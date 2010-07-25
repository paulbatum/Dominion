using System;

namespace Dominion.Rules.CardTypes
{
    public abstract class VictoryCard : Card
    {
        protected VictoryCard(int value, int cost) : base(cost)
        {
            Value = value;
        }

        public int Value { get; protected set; }

        public override int Score(CardZone allCards)
        {
            return Value;
        }
    }
}