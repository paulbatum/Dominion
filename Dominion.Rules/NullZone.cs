using System;

namespace Dominion.Rules
{
    public class NullZone : CardZone
    {
        protected override void AddCard(Card card)
        {
            // NO OP
        }

        protected override void RemoveCard(Card card)
        {
            // NO OP
        }

        public override int CardCount
        {
            get
            {
                throw new NotSupportedException("A null zone cannot be considered to have a card count");
            }
        }
    }
}