using System;

namespace Dominion.Rules.CardTypes
{
    public abstract class CurseCard : Card, IScoreCard
    {
        protected CurseCard(int cost, int value) : base(cost)
        {
            Value = value;
        }

        public int Value
        {
            get;
            set;
        }

        public int Score(CardZone allCards)
        {
            return Value;
        }
    }
}