using System;

namespace Dominion.Rules.CardTypes
{
    public abstract class VictoryCard : Card
    {
        protected VictoryCard(int value)
        {
            Value = value;
        }

        public override bool CanPlay(TurnContext context)
        {
            return false;
        }

        public override void Play(TurnContext context)
        {
            throw new NotSupportedException();
        }

        public int Value { get; protected set; }

        public override int Score(DrawDeck deck)
        {
            return Value;
        }
    }
}