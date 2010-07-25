using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class CardZone
    {
        private Random _random;
        private List<Card> _cards;

        public CardZone()
        {            
            _cards = new List<Card>();

            unchecked
            {
                int seed = this.GetHashCode() * Environment.TickCount;
                _random = new Random(seed);
            }
            
        }

        public virtual int CardCount
        {
            get { return this.Cards.Count(); }
        }

        public virtual void MoveCard(Card card, CardZone targetZone, CardZoneChanger changer)
        {
            RemoveCard(card);
            targetZone.AddCard(card);
            changer(targetZone);
        }

        public void MoveAll(CardZone targetZone)
        {
            IList<Card> allCards = new List<Card>(_cards);
            foreach (Card c in allCards)
                c.MoveTo(targetZone);
        }

        protected IEnumerable<Card> Cards
        {
            get { return _cards; }
        }

        protected void RandomizeOrder()
        {
            _cards = _cards.OrderBy(_ => _random.NextDouble()).ToList();
        }

        protected virtual void AddCard(Card card)
        {
            _cards.Add(card);
        }

        protected virtual void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }
    }
}