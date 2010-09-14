using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class DrawDeck : CardZone
    {
        private readonly DiscardPile _discards;

        public DrawDeck(IEnumerable<Card> startingCards, DiscardPile discards)
        {
            foreach (Card card in startingCards)
                card.MoveTo(this);

            _discards = discards;
        }

        public virtual void Shuffle()
        {
            RandomizeOrder();
        }

        public Card TopCard
        {
            get
            {
                if (CardCount == 0)
                {
                    _discards.MoveAll(this);
                    Shuffle();
                }

                return this.Cards.First();
            }
        }

        public void MoveCards(CardZone cardZone, int count)
        {
            count.Times(() => TopCard.MoveTo(cardZone));
        }

        public IEnumerable<Card> Contents
        {
            get { return this.Cards; }
        }

    }
}