using System.Linq;

namespace Dominion.Rules
{
    public class LimitedSupplyCardPile : CardPile
    {
        public override bool IsEmpty
        {
            get { return this.CardCount == 0; }
        }

        public override ICard TopCard
        {
            get { return Cards.FirstOrDefault(); }
        }

        public override bool IsLimited
        {
            get { return true; }
        }
    }
}