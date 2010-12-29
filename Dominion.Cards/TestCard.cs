using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;

namespace Dominion.Cards
{
    public class TestCard : Card
    {
        public TestCard(CardCost cost)
            : base(cost)
        {
        }
    }

    public class TestCardPile : CardPile
    {
        public TestCardPile(CardCost cost)
        {
            AddCard(new TestCard(cost));
        }

        public override bool IsEmpty
        {
            get { return !Cards.Any(); }
        }

        public override ICard TopCard
        {
            get { return Cards.First(); }
        }

        public override bool IsLimited
        {
            get { return true; }
        }
    }
}
