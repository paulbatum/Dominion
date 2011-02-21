using System;

namespace Dominion.Rules
{
    public class UnlimitedSupplyCardPile : CardPile
    {
        private readonly Func<ICard> _cardCreator;

        public UnlimitedSupplyCardPile(Func<ICard> cardCreator)
        {
            _cardCreator = cardCreator;
        }

        public override bool IsEmpty
        {
            get { return false; }
        }

        public override int CardCount
        {
            get
            {
                throw new NotSupportedException("A null zone cannot be considered to have a card count");                
            }
        }

        public override ICard TopCard
        {
            get { return _cardCreator(); }
        }

        public override bool IsLimited
        {
            get { return false; }
        }

        protected override void AddCard(ICard card)
        {
            // NO OP
        }

        protected override void RemoveCard(ICard card)
        {
            // NO OP
        }
        
    }
}