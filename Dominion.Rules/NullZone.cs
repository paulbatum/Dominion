using System;

namespace Dominion.Rules
{
    public class NullZone : CardZone
    {
        protected override void AddCard(ICard card)
        {
            // NO OP
        }

        protected override void RemoveCard(ICard card)
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