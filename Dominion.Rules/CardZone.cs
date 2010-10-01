using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class CardZone
    {
        private Random _random;
        private List<ICard> _cards;

        public CardZone()
        {
            _cards = new List<ICard>();

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

        public void MoveWhere(CardZone targetZone, Func<ICard, bool> predicate)
        {
            var matchingCards = _cards.Where(predicate).ToList();
            foreach (Card c in matchingCards)
                c.MoveTo(targetZone);
        }

        public void MoveAll(CardZone targetZone)
        {
            var allCards = new List<ICard>(_cards);
            foreach (Card c in allCards)
                c.MoveTo(targetZone);
        }

        protected IList<ICard> Cards
        {
            get { return _cards; }
        }

        protected void RandomizeOrder()
        {
            _cards = _cards.OrderBy(_ => _random.NextDouble()).ToList();
        }

        protected void Sort(Comparison<ICard> comparison)
        {
            _cards.Sort(comparison);
        }

        protected virtual void AddCard(ICard card)
        {
            _cards.Add(card);
        }

        protected virtual void RemoveCard(ICard card)
        {
            _cards.Remove(card);
        }
    }
}