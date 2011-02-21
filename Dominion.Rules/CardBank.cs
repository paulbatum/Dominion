using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Rules
{
    public class CardBank
    {
        private IList<CardPile> _piles;
        private IList<CardPile> _gameEndPiles;

        public CardBank()
        {
            _piles = new List<CardPile>();
            _gameEndPiles = new List<CardPile>();
        }

        public IEnumerable<CardPile> Piles
        {
            get { return _piles; }
        }

        public void AddCardPile(CardPile pile)
        {
            _piles.Add(pile);
        }

        public void AddCardPileWhichEndsTheGameWhenEmpty(CardPile pile)
        {
            AddCardPile(pile);
            _gameEndPiles.Add(pile);
        }

        public int EmptyPileCount
        {
            get
            {
                return _piles.Count(x => x.IsEmpty);
            }
        }

        public int EmptyGameEndingPilesCount
        {
            get { return _gameEndPiles.Count(x => x.IsEmpty); }
        }

        public CardPile Pile<T>()
        {
            return this.Piles.Single(x => x.Name == typeof(T).Name);
        }

        public CardPile NonEmptyPile<T>()
        {
            var pile = this.Pile<T>();
            return pile.IsEmpty ? null : pile;
        }
    }
}