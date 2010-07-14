using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class CardZone
    {
        private List<Card> _cards;

        public CardZone()
        {
            _cards = new List<Card>();
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
            Random r = new Random();
            _cards.Sort((c1, c2) => r.Next());
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