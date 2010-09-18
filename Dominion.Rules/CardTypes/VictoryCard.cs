using System;

namespace Dominion.Rules.CardTypes
{
    public abstract class VictoryCard : Card, IScoreCard
    {
        protected VictoryCard(int value, int cost) : base(cost)
        {
            Value = value;
        }

        public int Value { get; set; }

        public virtual int Score(CardZone allCards)
        {
            return Value;
        }
    }
}